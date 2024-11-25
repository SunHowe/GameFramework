using System;
using System.Collections.Generic;
using System.Reflection;
using FairyGUI.Dynamic;
using GameFramework;
using GameFramework.ObjectPool;
using GameFramework.Resource;
using GameFramework.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// 基于FairyGUI的界面组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/FGUI")]
    public class FGUIComponent : GameFrameworkComponent<FGUIComponent>
    {
        private const int DefaultPriority = 0;

        private IUIManager m_UIManager = null;
        private EventComponent m_EventComponent = null;

        private readonly List<IUIForm> m_InternalUIFormResults = new List<IUIForm>();

        [SerializeField]
        private bool m_EnableOpenUIFormSuccessEvent = true;

        [SerializeField]
        private bool m_EnableOpenUIFormFailureEvent = true;

        [SerializeField]
        private bool m_EnableOpenUIFormUpdateEvent = false;

        [SerializeField]
        private bool m_EnableOpenUIFormDependencyAssetEvent = false;

        [SerializeField]
        private bool m_EnableCloseUIFormCompleteEvent = true;

        [SerializeField]
        private float m_InstanceAutoReleaseInterval = 60f;

        [SerializeField]
        private int m_InstanceCapacity = 16;

        [SerializeField]
        private float m_InstanceExpireTime = 60f;

        [SerializeField]
        private int m_InstancePriority = 0;

        [SerializeField]
        private string m_UIFormHelperTypeName = "UnityGameFramework.Runtime.FairyGUI.DefaultFGUIFormHelper";

        [SerializeField]
        private UIFormHelperBase m_CustomUIFormHelper = null;

        [SerializeField]
        private string m_FGUIAssetLoaderHelperTypeName = "UnityGameFramework.Runtime.FairyGUI.DefaultFGUIAssetLoaderHelper";
        
        [SerializeField]
        private FGUIAssetLoaderHelperBase m_CustomFGUIAssetLoaderHelper = null;

        [SerializeField]
        private string[] m_UIGroups = null;

        [SerializeField]
        private UIPackageMapping m_FGUIPackageMappingOnResources;

        [SerializeField]
        private UIPackageMappingAssetRef m_FGUIPackageMappingHotfix;

        [SerializeField]
        private bool m_UnloadUnusedUIPackageImmediately = true;

        private FGUIPackageHelper m_FGUIPackageHelper;

        private readonly Dictionary<string, UIFormBindingInfo> m_UIFormBindingInfoDict = new Dictionary<string, UIFormBindingInfo>();
        private readonly Dictionary<Type, UIFormBindingInfo> m_UIFormBindingInfoByLogicTypeDict = new Dictionary<Type, UIFormBindingInfo>();
        
        /// <summary>
        /// 获取界面组数量。
        /// </summary>
        public int UIGroupCount
        {
            get
            {
                return m_UIManager.UIGroupCount;
            }
        }

        /// <summary>
        /// 获取或设置界面实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        public float InstanceAutoReleaseInterval
        {
            get
            {
                return m_UIManager.InstanceAutoReleaseInterval;
            }
            set
            {
                m_UIManager.InstanceAutoReleaseInterval = m_InstanceAutoReleaseInterval = value;
            }
        }

        /// <summary>
        /// 获取或设置界面实例对象池的容量。
        /// </summary>
        public int InstanceCapacity
        {
            get
            {
                return m_UIManager.InstanceCapacity;
            }
            set
            {
                m_UIManager.InstanceCapacity = m_InstanceCapacity = value;
            }
        }

        /// <summary>
        /// 获取或设置界面实例对象池对象过期秒数。
        /// </summary>
        public float InstanceExpireTime
        {
            get
            {
                return m_UIManager.InstanceExpireTime;
            }
            set
            {
                m_UIManager.InstanceExpireTime = m_InstanceExpireTime = value;
            }
        }

        /// <summary>
        /// 获取或设置界面实例对象池的优先级。
        /// </summary>
        public int InstancePriority
        {
            get
            {
                return m_UIManager.InstancePriority;
            }
            set
            {
                m_UIManager.InstancePriority = m_InstancePriority = value;
            }
        }

        /// <summary>
        /// FairyGUI包辅助器。
        /// </summary>
        public FGUIPackageHelper FGUIPackageHelper
        {
            get
            {
                return m_FGUIPackageHelper;
            }
        }

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            m_UIManager = GameFrameworkEntry.GetModule<IUIManager>();
            if (m_UIManager == null)
            {
                Log.Fatal("UI manager is invalid.");
                return;
            }

            if (m_FGUIPackageMappingOnResources == null)
            {
                Log.Fatal("FGUI package mapping on resources is invalid.");
                return;
            }

            if (m_EnableOpenUIFormSuccessEvent)
            {
                m_UIManager.OpenUIFormSuccess += OnOpenUIFormSuccess;
            }

            m_UIManager.OpenUIFormFailure += OnOpenUIFormFailure;

            if (m_EnableOpenUIFormUpdateEvent)
            {
                m_UIManager.OpenUIFormUpdate += OnOpenUIFormUpdate;
            }

            if (m_EnableOpenUIFormDependencyAssetEvent)
            {
                m_UIManager.OpenUIFormDependencyAsset += OnOpenUIFormDependencyAsset;
            }

            if (m_EnableCloseUIFormCompleteEvent)
            {
                m_UIManager.CloseUIFormComplete += OnCloseUIFormComplete;
            }
        }

        private void Start()
        {
            BaseComponent baseComponent = GameEntry.GetComponent<BaseComponent>();
            if (baseComponent == null)
            {
                Log.Fatal("Base component is invalid.");
                return;
            }

            m_EventComponent = GameEntry.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }

            m_UIManager.SetObjectPoolManager(GameFrameworkEntry.GetModule<IObjectPoolManager>());
            m_UIManager.InstanceAutoReleaseInterval = m_InstanceAutoReleaseInterval;
            m_UIManager.InstanceCapacity = m_InstanceCapacity;
            m_UIManager.InstanceExpireTime = m_InstanceExpireTime;
            m_UIManager.InstancePriority = m_InstancePriority;

            UIFormHelperBase uiFormHelper = Helper.CreateHelper(m_UIFormHelperTypeName, m_CustomUIFormHelper);
            if (uiFormHelper == null)
            {
                Log.Error("Can not create UI form helper.");
                return;
            }

            uiFormHelper.name = "UI Form Helper";
            Transform transform = uiFormHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;

            m_UIManager.SetUIFormHelper(uiFormHelper);

            m_FGUIPackageHelper = new GameObject("FGUI Package Helper").AddComponent<FGUIPackageHelper>();
            transform = m_FGUIPackageHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;
            m_FGUIPackageHelper.AddPackageMapping(m_FGUIPackageMappingOnResources);
            
            var assetLoaderHelper = Helper.CreateHelper(m_FGUIAssetLoaderHelperTypeName, m_CustomFGUIAssetLoaderHelper);
            if (assetLoaderHelper == null)
            {
                Log.Error("Can not create FUI asset loader helper.");
                return;
            }
            
            assetLoaderHelper.name = "FGUI Asset Loader Helper";
            transform = assetLoaderHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;

            var uiFormAssetHelper = new GameObject("UI Form Asset Helper").AddComponent<FGUIFormAssetHelper>();
            transform = uiFormAssetHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;
            uiFormAssetHelper.InitHelper(m_FGUIPackageHelper, assetLoaderHelper, m_UnloadUnusedUIPackageImmediately);
            
            m_UIManager.SetUIFormAssetHelper(uiFormAssetHelper);

            for (int i = 0; i < m_UIGroups.Length; i++)
            {
                if (!AddUIGroup(m_UIGroups[i], i))
                {
                    Log.Warning("Add UI group '{0}' failure.", m_UIGroups[i]);
                    continue;
                }
            }
        }

        /// <summary>
        /// 是否存在界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>是否存在界面组。</returns>
        public bool HasUIGroup(string uiGroupName)
        {
            return m_UIManager.HasUIGroup(uiGroupName);
        }

        /// <summary>
        /// 获取界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>要获取的界面组。</returns>
        public IUIGroup GetUIGroup(string uiGroupName)
        {
            return m_UIManager.GetUIGroup(uiGroupName);
        }

        /// <summary>
        /// 获取所有界面组。
        /// </summary>
        /// <returns>所有界面组。</returns>
        public IUIGroup[] GetAllUIGroups()
        {
            return m_UIManager.GetAllUIGroups();
        }

        /// <summary>
        /// 获取所有界面组。
        /// </summary>
        /// <param name="results">所有界面组。</param>
        public void GetAllUIGroups(List<IUIGroup> results)
        {
            m_UIManager.GetAllUIGroups(results);
        }

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>是否增加界面组成功。</returns>
        public bool AddUIGroup(string uiGroupName)
        {
            return AddUIGroup(uiGroupName, 0);
        }

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="depth">界面组深度。</param>
        /// <returns>是否增加界面组成功。</returns>
        public bool AddUIGroup(string uiGroupName, int depth)
        {
            if (m_UIManager.HasUIGroup(uiGroupName))
            {
                return false;
            }

            return m_UIManager.AddUIGroup(uiGroupName, depth, new FGUIGroupHelper(uiGroupName, depth));
        }

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>是否存在界面。</returns>
        public bool HasUIForm(int serialId)
        {
            return m_UIManager.HasUIForm(serialId);
        }

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>是否存在界面。</returns>
        public bool HasUIForm(string uiFormAssetName)
        {
            return m_UIManager.HasUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>要获取的界面。</returns>
        public FGUIForm GetUIForm(int serialId)
        {
            return (FGUIForm)m_UIManager.GetUIForm(serialId);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>要获取的界面。</returns>
        public FGUIForm GetUIForm(string uiFormAssetName)
        {
            return (FGUIForm)m_UIManager.GetUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>要获取的界面。</returns>
        public FGUIForm[] GetUIForms(string uiFormAssetName)
        {
            IUIForm[] uiForms = m_UIManager.GetUIForms(uiFormAssetName);
            FGUIForm[] uiFormImpls = new FGUIForm[uiForms.Length];
            for (int i = 0; i < uiForms.Length; i++)
            {
                uiFormImpls[i] = (FGUIForm)uiForms[i];
            }

            return uiFormImpls;
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="results">要获取的界面。</param>
        public void GetUIForms(string uiFormAssetName, List<FGUIForm> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
                return;
            }

            results.Clear();
            m_UIManager.GetUIForms(uiFormAssetName, m_InternalUIFormResults);
            foreach (IUIForm uiForm in m_InternalUIFormResults)
            {
                results.Add((FGUIForm)uiForm);
            }
        }

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <returns>所有已加载的界面。</returns>
        public FGUIForm[] GetAllLoadedUIForms()
        {
            IUIForm[] uiForms = m_UIManager.GetAllLoadedUIForms();
            FGUIForm[] uiFormImpls = new FGUIForm[uiForms.Length];
            for (int i = 0; i < uiForms.Length; i++)
            {
                uiFormImpls[i] = (FGUIForm)uiForms[i];
            }

            return uiFormImpls;
        }

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <param name="results">所有已加载的界面。</param>
        public void GetAllLoadedUIForms(List<FGUIForm> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
                return;
            }

            results.Clear();
            m_UIManager.GetAllLoadedUIForms(m_InternalUIFormResults);
            foreach (IUIForm uiForm in m_InternalUIFormResults)
            {
                results.Add((FGUIForm)uiForm);
            }
        }

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <returns>所有正在加载界面的序列编号。</returns>
        public int[] GetAllLoadingUIFormSerialIds()
        {
            return m_UIManager.GetAllLoadingUIFormSerialIds();
        }

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <param name="results">所有正在加载界面的序列编号。</param>
        public void GetAllLoadingUIFormSerialIds(List<int> results)
        {
            m_UIManager.GetAllLoadingUIFormSerialIds(results);
        }

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>是否正在加载界面。</returns>
        public bool IsLoadingUIForm(int serialId)
        {
            return m_UIManager.IsLoadingUIForm(serialId);
        }

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>是否正在加载界面。</returns>
        public bool IsLoadingUIForm(string uiFormAssetName)
        {
            return m_UIManager.IsLoadingUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 是否是合法的界面。
        /// </summary>
        /// <param name="uiForm">界面。</param>
        /// <returns>界面是否合法。</returns>
        public bool IsValidUIForm(FGUIForm uiForm)
        {
            return m_UIManager.IsValidUIForm(uiForm);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, DefaultPriority, false, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, priority, false, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, DefaultPriority, pauseCoveredUIForm, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, object userData)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, DefaultPriority, false, userData);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority, object userData)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, priority, false, userData);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, object userData)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, DefaultPriority, pauseCoveredUIForm, userData);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm, object userData)
        {
            return m_UIManager.OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, userData);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, DefaultPriority, false, null);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, int priority)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, priority, false, null);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, DefaultPriority, pauseCoveredUIForm, null);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, object userData)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, DefaultPriority, false, userData);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, null);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, int priority, object userData)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, priority, false, userData);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, object userData)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, DefaultPriority, pauseCoveredUIForm, userData);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm, object userData)
        {
            IUIForm form = m_UIManager.GetUIForm(uiFormAssetName);
            if (form != null)
            {
                m_UIManager.RefocusUIForm(form, userData);
                return form.SerialId;
            }
            
            return m_UIManager.OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, userData);
        }

        /// <summary>
        /// 打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName)
        {
            return OpenUIForm(uiFormAssetName, null);
        }

        /// <summary>
        /// 打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, object userData)
        {
            var bindingInfo = GetUIFormBindingInfo(uiFormAssetName);
            if (bindingInfo == null)
            {
                Log.Error("Cannot find binding info of UIForm '{0}'.", uiFormAssetName);
                return 0;
            }

            if (bindingInfo.AllowMultiple)
            {
                return OpenUIForm(bindingInfo.UIFormAssetName, bindingInfo.UIGroupName, bindingInfo.Priority, bindingInfo.PauseCoveredUIForm, userData);
            }
            else
            {
                return OpenSingletonUIForm(bindingInfo.UIFormAssetName, bindingInfo.UIGroupName, bindingInfo.Priority, bindingInfo.PauseCoveredUIForm, userData);
            }
        }

        /// <summary>
        /// 通过界面逻辑类型打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm<T>() where T : FGUIFormLogic
        {
            return OpenUIForm<T>(null);
        }

        /// <summary>
        /// 通过界面逻辑类型打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm<T>(object userData) where T : FGUIFormLogic
        {
            var bindingInfo = GetUIFormBindingInfo(typeof(T));
            if (bindingInfo == null)
            {
                Log.Error("Cannot find binding info of UIForm '{0}'.", typeof(T).Name);
                return 0;
            }

            if (bindingInfo.AllowMultiple)
            {
                return OpenUIForm(bindingInfo.UIFormAssetName, bindingInfo.UIGroupName, bindingInfo.Priority, bindingInfo.PauseCoveredUIForm, userData);
            }
            else
            {
                return OpenSingletonUIForm(bindingInfo.UIFormAssetName, bindingInfo.UIGroupName, bindingInfo.Priority, bindingInfo.PauseCoveredUIForm, userData);
            }
        }
        
        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        public void CloseUIForm(int serialId)
        {
            m_UIManager.CloseUIForm(serialId);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseUIForm(int serialId, object userData)
        {
            m_UIManager.CloseUIForm(serialId, userData);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        public void CloseUIForm(FGUIForm uiForm)
        {
            m_UIManager.CloseUIForm(uiForm);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseUIForm(FGUIForm uiForm, object userData)
        {
            m_UIManager.CloseUIForm(uiForm, userData);
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        public void CloseAllLoadedUIForms()
        {
            m_UIManager.CloseAllLoadedUIForms();
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseAllLoadedUIForms(object userData)
        {
            m_UIManager.CloseAllLoadedUIForms(userData);
        }

        /// <summary>
        /// 关闭所有正在加载的界面。
        /// </summary>
        public void CloseAllLoadingUIForms()
        {
            m_UIManager.CloseAllLoadingUIForms();
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        public void RefocusUIForm(FGUIForm uiForm)
        {
            m_UIManager.RefocusUIForm(uiForm);
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void RefocusUIForm(FGUIForm uiForm, object userData)
        {
            m_UIManager.RefocusUIForm(uiForm, userData);
        }

        /// <summary>
        /// 设置界面是否被加锁。
        /// </summary>
        /// <param name="uiForm">要设置是否被加锁的界面。</param>
        /// <param name="locked">界面是否被加锁。</param>
        public void SetUIFormInstanceLocked(FGUIForm uiForm, bool locked)
        {
            if (uiForm == null)
            {
                Log.Warning("UI form is invalid.");
                return;
            }

            m_UIManager.SetUIFormInstanceLocked(uiForm.GameObject, locked);
        }

        /// <summary>
        /// 设置界面的优先级。
        /// </summary>
        /// <param name="uiForm">要设置优先级的界面。</param>
        /// <param name="priority">界面优先级。</param>
        public void SetUIFormInstancePriority(FGUIForm uiForm, int priority)
        {
            if (uiForm == null)
            {
                Log.Warning("UI form is invalid.");
                return;
            }

            m_UIManager.SetUIFormInstancePriority(uiForm.GameObject, priority);
        }

        /// <summary>
        /// 通过反射进行界面逻辑类型与界面信息的绑定。需要界面逻辑类型标注了UIFormAttribute特性。
        /// </summary>
        /// <param name="uiFormLogicType">界面逻辑类型。</param>
        public void RegisterUIFormBinding(Type uiFormLogicType)
        {
            var attribute = uiFormLogicType.GetCustomAttribute<UIFormAttribute>(false);
            if (attribute == null)
            {
                Log.Error("UIFormAttribute is invalid.");
                return;
            }

            var bindingInfo = new UIFormBindingInfo(attribute, uiFormLogicType);
            m_UIFormBindingInfoDict.Add(attribute.UIFormAssetName, bindingInfo);
            m_UIFormBindingInfoByLogicTypeDict.Add(uiFormLogicType, bindingInfo);
        }

        /// <summary>
        /// 通过反射进行界面逻辑类型与界面信息的绑定。需要界面逻辑类型标注了UIFormAttribute特性。
        /// </summary>
        public void RegisterUIFormBinding<T>() where T : FGUIFormLogic
        {
            RegisterUIFormBinding(typeof(T));
        }

        /// <summary>
        /// 获取界面绑定信息。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>绑定信息实例。</returns>
        public UIFormBindingInfo GetUIFormBindingInfo(string uiFormAssetName)
        {
            return m_UIFormBindingInfoDict.TryGetValue(uiFormAssetName, out var bindingInfo) ? bindingInfo : null;
        }

        /// <summary>
        /// 通过逻辑类型获取界面绑定信息。
        /// </summary>
        /// <param name="uiFormLogicType">界面逻辑类型。</param>
        /// <returns>绑定信息实例。</returns>
        public UIFormBindingInfo GetUIFormBindingInfo(Type uiFormLogicType)
        {
            return m_UIFormBindingInfoByLogicTypeDict.TryGetValue(uiFormLogicType, out var bindingInfo) ? bindingInfo : null;
        }

        /// <summary>
        /// 获取界面逻辑类型。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>逻辑类型。</returns>
        public Type GetUIFormLogicType(string uiFormAssetName)
        {
            return GetUIFormBindingInfo(uiFormAssetName)?.FormLogicType;
        }

        /// <summary>
        /// 通过逻辑类型获取界面资源名。
        /// </summary>
        /// <typeparam name="T">界面逻辑类型。</typeparam>
        /// <returns>界面资源名。</returns>
        public string GetUIFormAssetName<T>() where T : FGUIFormLogic
        {
            return GetUIFormBindingInfo(typeof(T))?.UIFormAssetName ?? string.Empty;
        }

        /// <summary>
        /// 判断是否是Resources目录下的包。
        /// </summary>
        public bool IsPackageOnResources(string packageName)
        {
            return Array.IndexOf(m_FGUIPackageMappingOnResources.PackageNames, packageName) >= 0;
        }
        
        private void OnOpenUIFormSuccess(object sender, GameFramework.UI.OpenUIFormSuccessEventArgs e)
        {
            m_EventComponent.Fire(this, OpenUIFormSuccessEventArgs.Create(e));
        }

        private void OnOpenUIFormFailure(object sender, GameFramework.UI.OpenUIFormFailureEventArgs e)
        {
            Log.Warning("Open UI form failure, asset name '{0}', UI group name '{1}', pause covered UI form '{2}', error message '{3}'.", e.UIFormAssetName, e.UIGroupName, e.PauseCoveredUIForm, e.ErrorMessage);
            if (m_EnableOpenUIFormFailureEvent)
            {
                m_EventComponent.Fire(this, OpenUIFormFailureEventArgs.Create(e));
            }
        }

        private void OnOpenUIFormUpdate(object sender, GameFramework.UI.OpenUIFormUpdateEventArgs e)
        {
            m_EventComponent.Fire(this, OpenUIFormUpdateEventArgs.Create(e));
        }

        private void OnOpenUIFormDependencyAsset(object sender, GameFramework.UI.OpenUIFormDependencyAssetEventArgs e)
        {
            m_EventComponent.Fire(this, OpenUIFormDependencyAssetEventArgs.Create(e));
        }

        private void OnCloseUIFormComplete(object sender, GameFramework.UI.CloseUIFormCompleteEventArgs e)
        {
            m_EventComponent.Fire(this, CloseUIFormCompleteEventArgs.Create(e));
        }
    }
}