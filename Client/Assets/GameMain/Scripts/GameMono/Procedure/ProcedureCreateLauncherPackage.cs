using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using UnityGameFramework.Runtime;

namespace GameMono
{
    /// <summary>
    /// 创建启动资源包流程。
    /// </summary>
    public class ProcedureCreateLauncherPackage : ProcedureBase
    {
        private bool m_CreatePackageDone;
        private CreatePackageCallbacks m_CreatePackageCallbacks;

        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            m_CreatePackageCallbacks = new CreatePackageCallbacks(OnCreatePackageSuccess);
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_CreatePackageDone = false;
            
            // 创建启动器资源包
            ResourceComponent.Instance.CreateResourcePackage("GameLauncher", ResourceMode.Updatable, m_CreatePackageCallbacks);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (m_CreatePackageDone)
            {
                ChangeState<ProcedureLoadLauncherUIMapping>(procedureOwner);
            }
        }

        private void OnCreatePackageSuccess(string packageName, ResourceMode resourceMode, IResourcePackage resourcePackage, object userData)
        {
            m_CreatePackageDone = true;
        }
    }
}