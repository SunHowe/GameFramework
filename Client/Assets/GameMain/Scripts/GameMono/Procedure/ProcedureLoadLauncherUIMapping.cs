using FairyGUI.Dynamic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;

namespace GameMono
{
    /// <summary>
    /// 加载启动器UI映射文件流程。
    /// </summary>
    public class ProcedureLoadLauncherUIMapping : ProcedureBase
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
            
            m_IsDone = false;
            
            FGUIComponent.Instance.LauncherPackageMapping.LoadAsset(ResourceComponent.Instance, m_LoadAssetCallbacks);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (m_IsDone)
            {
                
            }
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