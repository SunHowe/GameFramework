using FairyGUI.Dynamic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;

namespace GameLogic
{
    /// <summary>
    /// 热更新启动流程 - 初始化热更新模块。
    /// </summary>
    internal class LaunchProcedure : ProcedureBase
    {
        private bool m_IsDone;
        private LoadAssetCallbacks m_LoadAssetCallbacks;
        
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            m_LoadAssetCallbacks = new LoadAssetCallbacks(OnLoadAssetSuccess);
            
            // 注册界面绑定与自定义组件绑定。
            FGUIComponent.Instance.RegisterFGUIFormAndCustomComponent(GetType().Assembly);
            
            // 配置模块
            GameLogicComponent.Instance.AddGameLogic<DataTableModule>();
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            
            m_IsDone = false;
            
            // 加载主包UI资源映射表。
            FGUIComponent.Instance.MainPackageMapping.LoadAsset(ResourceComponent.Instance, m_LoadAssetCallbacks);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!m_IsDone)
            {
                return;
            }
            
            ChangeState<LoadDataTableProcedure>(procedureOwner);
        }

        private void OnLoadAssetSuccess(string assetname, object asset, float duration, object userdata)
        {
            var mapping = (UIPackageMapping)asset;
            FGUIComponent.Instance.FGUIPackageHelper.AddPackageMapping(mapping);
            ResourceComponent.Instance.UnloadAsset(mapping);
            
            m_IsDone = true;
        }
    }
}