using FairyGUI.Dynamic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;

namespace GameLogic
{
    /// <summary>
    /// 加载游戏主UI包映射流程。
    /// </summary>
    internal sealed class ProcedureLoadMainUIMapping : ProcedureBase
    {
        private bool m_IsDone;
        private LoadAssetCallbacks m_LoadAssetCallbacks;

        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            m_LoadAssetCallbacks = new LoadAssetCallbacks(OnLoadAssetSuccess);
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            
            // 注册界面绑定与自定义组件绑定。
            FGUIComponent.Instance.RegisterFGUIFormAndCustomComponent(GetType().Assembly);
            
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
            
            ChangeState<ProcedureLogin>(procedureOwner);
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