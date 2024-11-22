using System;
using System.Collections.Generic;
using System.Linq;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityGameFramework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Hotfix")]
    public sealed class HotfixComponent : GameFrameworkComponent<HotfixComponent>
    {
        [SerializeField]
        private HotfixConfigurationAssetRef m_HotfixConfigAssetRef = null;

        [SerializeField]
        private string m_HotfixAssetHelperTypeName = "UnityGameFramework.Runtime.DefaultHotfixAssetHelper";

        [FormerlySerializedAs("m_CustomHotfixHelper")]
        [SerializeField]
        private HotfixAssetHelperBase m_CustomHotfixAssetHelper = null;

        [SerializeField]
        private bool m_EnableLoadCompleteEvent;

        [SerializeField]
        private bool m_EnableLoadFailureEvent;

        [SerializeField]
        private bool m_EnableLoadStepChangedEvent;

        [SerializeField]
        private bool m_EnableLoadAssemblyFailureEvent;

        private EventComponent m_EventComponent = null;
        private ResourceComponent m_ResourceComponent = null;
        private GameLogicComponent m_GameLogicComponent = null;

        private LoadAssetCallbacks m_LoadHotfixConfigCallbacks;
        private LoadBinaryCallbacks m_LoadAssemblyFileCallbacks;

        private HotfixLoadStep m_CurrentLoadStep = HotfixLoadStep.None;

        private List<AssemblyInfo> m_HotfixAssemblies;
        private byte[][] m_HotfixAssemblyBytes;
        private HotfixAppTypeRef m_HotfixAppType;

        /// <summary>
        /// 已加载的热更新程序集信息
        /// </summary>
        private static readonly Dictionary<string, AssemblyRuntimeInfo> s_LoadedAssemblies = new Dictionary<string, AssemblyRuntimeInfo>();

        private void Start()
        {
            if (m_HotfixConfigAssetRef == null || string.IsNullOrEmpty(m_HotfixConfigAssetRef.AssetPath))
            {
                Log.Fatal("Hotfix component HotfixConfig not configure.");
                return;
            }

            m_EventComponent = GameEntry.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }

            m_ResourceComponent = GameEntry.GetComponent<ResourceComponent>();
            if (m_ResourceComponent == null)
            {
                Log.Fatal("Resource component is invalid.");
                return;
            }
            
            m_GameLogicComponent = GameEntry.GetComponent<GameLogicComponent>();
            if (m_GameLogicComponent == null)
            {
                Log.Fatal("GameLogic component is invalid.");
                return;
            }

            var hotfixHelper = Helper.CreateHelper(m_HotfixAssetHelperTypeName, m_CustomHotfixAssetHelper);
            if (hotfixHelper == null)
            {
                Log.Error("Can not create hotfix helper.");
                return;
            }

            hotfixHelper.name = "Hotfix Helper";
            var helperTransform = hotfixHelper.transform;
            helperTransform.SetParent(this.transform);
            helperTransform.localScale = Vector3.one;

            m_LoadHotfixConfigCallbacks = new LoadAssetCallbacks(OnLoadHotfixConfigSuccess, OnLoadHotfixConfigFailure);
            m_LoadAssemblyFileCallbacks = new LoadBinaryCallbacks(OnLoadAssemblyFileSuccess, OnLoadAssemblyFileFailure);
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        public void LoadHotfix()
        {
            if (m_CurrentLoadStep != HotfixLoadStep.None)
            {
                Log.Fatal("Duplicate LoadHotfix.");
                return;
            }

            EnterLoadConfigStep();
        }

        #region [加载热更新配置文件]

        /// <summary>
        /// 进入加载配置阶段
        /// </summary>
        private void EnterLoadConfigStep()
        {
            SetCurrentStep(HotfixLoadStep.LoadConfig);
            m_HotfixConfigAssetRef.LoadAsset(m_ResourceComponent, m_LoadHotfixConfigCallbacks);
        }

        private void OnLoadHotfixConfigFailure(string assetName, LoadResourceStatus status, string errorMessage, object userdata)
        {
            SetCurrentStep(HotfixLoadStep.None);

            if (m_EnableLoadFailureEvent)
            {
                m_EventComponent.Fire(this, HotfixLoadFailureEventArgs.Create(HotfixLoadStep.LoadConfig, errorMessage));
                return;
            }

            Log.Fatal("Load hotfix config failure, asset name '{0}', status '{1}', error message '{2}'.", assetName, status, errorMessage);
        }

        private void OnLoadHotfixConfigSuccess(string assetName, object asset, float duration, object userdata)
        {
            var configuration = (HotfixConfiguration)asset;

            m_HotfixAppType = configuration.HotfixAppType;
            m_HotfixAssemblies = configuration.HotfixAssemblies.ToList();

            // 归还资源
            m_ResourceComponent.UnloadAsset(asset);

#if UNITY_EDITOR
            // 编辑器下直接触发到启动逻辑步骤
            EnterLaunchHotfixEntryStep();
#else
            // 进入加载程序集文件阶段
            EnterLoadAssemblyFileStep();
#endif
        }

        #endregion

        #region [加载热更新文件]

        /// <summary>
        /// 加载热更新文件
        /// </summary>
        private void EnterLoadAssemblyFileStep()
        {
            SetCurrentStep(HotfixLoadStep.LoadAssemblyFile);

            // 判断需要加载的热更新文件列表
            for (var index = 0; index < m_HotfixAssemblies.Count; index++)
            {
                var assemblyInfo = m_HotfixAssemblies[index];

                // 只要有一个程序集未加载或版本不对 在其之后的程序集都需要重新加载
                if (!s_LoadedAssemblies.TryGetValue(assemblyInfo.Name, out var runtimeInfo))
                    break;

                if (runtimeInfo.Version != assemblyInfo.Version)
                    break;

                // 程序集已加载 移除该列表项
                m_HotfixAssemblies.RemoveAt(index--);
            }

            if (m_HotfixAssemblies.Count == 0)
            {
                // 无需加载 直接跳转加载热更新逻辑入口的步骤
                EnterLaunchHotfixEntryStep();
                return;
            }

            // 初始化文件数据数组
            m_HotfixAssemblyBytes = new byte[m_HotfixAssemblies.Count][];
            for (var index = 0; index < m_HotfixAssemblies.Count; index++)
            {
                var assemblyInfo = m_HotfixAssemblies[index];
                var assetName = m_CustomHotfixAssetHelper.GetAssemblyAssetName(assemblyInfo);
                m_ResourceComponent.LoadBinary(assetName, m_LoadAssemblyFileCallbacks, assemblyInfo);
            }
        }

        /// <summary>
        /// 热更新程序集文件加载失败回调。
        /// </summary>
        private void OnLoadAssemblyFileFailure(string binaryAssetName, LoadResourceStatus status, string errorMessage, object userdata)
        {
            var assemblyInfo = (AssemblyInfo)userdata;
            if (m_HotfixAssemblies == null)
            {
                // 热更新程序集文件列表已经被清空，说明已经触发过加载失败，直接返回。
                return;
            }

            var index = m_HotfixAssemblies.IndexOf(assemblyInfo);
            if (index == -1)
            {
                // 热更新程序集文件列表中不存在指定的热更新程序集文件，说明已经触发过加载失败，直接返回。
                return;
            }

            // 清空数据
            m_HotfixAssemblies = null;
            m_HotfixAssemblyBytes = null;
            m_HotfixAppType = null;

            SetCurrentStep(HotfixLoadStep.None);

            if (m_EnableLoadFailureEvent)
            {
                m_EventComponent.Fire(this, HotfixLoadFailureEventArgs.Create(HotfixLoadStep.LoadAssemblyFile, errorMessage));
            }
        }

        /// <summary>
        /// 热更新程序集文件加载成功回调
        /// </summary>
        private void OnLoadAssemblyFileSuccess(string binaryAssetName, byte[] binaryBytes, float duration, object userdata)
        {
            var assemblyInfo = (AssemblyInfo)userdata;
            if (m_HotfixAssemblies == null)
            {
                // 热更新程序集文件列表已经被清空，说明已经触发过加载失败，直接返回。
                return;
            }

            var index = m_HotfixAssemblies.IndexOf(assemblyInfo);
            if (index == -1)
            {
                // 热更新程序集文件列表中不存在指定的热更新程序集文件，说明已经触发过加载失败，直接返回。
                return;
            }

            m_HotfixAssemblyBytes[index] = binaryBytes;

            if (!CheckAssemblyBytesComplete())
            {
                // 还未全部加载完成
                return;
            }

            // 全部加载完成，进入加载程序集步骤
            EnterLoadAssemblyStep();
        }

        /// <summary>
        /// 检查程序集文件加载是否完成
        /// </summary>
        private bool CheckAssemblyBytesComplete()
        {
            if (m_HotfixAssemblyBytes == null)
            {
                return false;
            }

            foreach (var bytes in m_HotfixAssemblyBytes)
            {
                if (bytes == null)
                    return false;
            }

            return true;
        }

        #endregion

        #region [加载程序集]

        /// <summary>
        /// 加载程序集
        /// </summary>
        private void EnterLoadAssemblyStep()
        {
            SetCurrentStep(HotfixLoadStep.LoadAssembly);
            
            // TODO: 加载程序集 并加入到Utility.Assembly中
        }

        #endregion

        #region [启动热更新逻辑]

        /// <summary>
        /// 启动热更新逻辑
        /// </summary>
        private void EnterLaunchHotfixEntryStep()
        {
            SetCurrentStep(HotfixLoadStep.LaunchHotfixEntry);

            var errorMessage = string.Empty;
            
            try
            {
                var appType = m_HotfixAppType.GetRuntimeType();
                
                // 创建热更新应用实例
                var hotfixApp = (HotfixAppBase)Activator.CreateInstance(appType);
                
                // 将热更新引用实例添加到逻辑模块进行驱动
                m_GameLogicComponent.AddGameLogic(hotfixApp);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            
            // 清空数据
            m_HotfixAssemblies = null;
            m_HotfixAssemblyBytes = null;
            m_HotfixAppType = null;

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // 加载失败
                SetCurrentStep(HotfixLoadStep.None);

                if (m_EnableLoadFailureEvent)
                {
                    m_EventComponent.Fire(this, HotfixLoadFailureEventArgs.Create(HotfixLoadStep.LaunchHotfixEntry, errorMessage));
                }
                else
                {
                    Log.Fatal("HotfixComponent: Launch hotfix entry failure, error message is '{0}'.", errorMessage);
                }

                return;
            }
            
            // 加载成功
            SetCurrentStep(HotfixLoadStep.Complete);

            if (m_EnableLoadCompleteEvent)
            {
                m_EventComponent.Fire(this, HotfixLoadCompleteEventArgs.Create());
            }
            
            Log.Info("HotfixComponent: Launch hotfix entry success.");
        }

        #endregion

        /// <summary>
        /// 设置当前阶段
        /// </summary>
        private void SetCurrentStep(HotfixLoadStep step)
        {
            m_CurrentLoadStep = step;

            if (!m_EnableLoadStepChangedEvent)
            {
                return;
            }

            m_EventComponent.Fire(this, HotfixLoadStepChangedEventArgs.Create(step));
        }
    }
}