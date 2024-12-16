using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using GameMono.UI.Launch;
using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;

namespace GameMono
{
    /// <summary>
    /// 创建默认资源包流程。
    /// </summary>
    internal class ProcedureCreateDefaultPackage : ProcedureBase
    {
        private bool m_IsCompleted = false;
        private CreatePackageCallbacks m_CreatePackageCallbacks;

        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            m_IsCompleted = false;
            m_CreatePackageCallbacks = new CreatePackageCallbacks(OnCreatePackageSuccess);
        }
        
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            
            ResourceComponent.Instance.CreateDefaultResourcePackage(m_CreatePackageCallbacks);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!m_IsCompleted)
            {
                return;
            }
            
            if (BaseComponent.Instance.EditorResourceMode)
            {
                // 编辑器模式
                Log.Info("Use EditorResourceMode.");
                ChangeState<ProcedureLaunchGameLogic>(procedureOwner);
            }
            else if (ResourceComponent.Instance.DefaultPackageResourceMode == ResourceMode.Package)
            {
                // 单机模式
                Log.Info("Use PackageResourceMode.");
                ChangeState<ProcedureLaunchGameLogic>(procedureOwner);
            }
            else
            {
                // 可更新模式
                Log.Info("Use UpdatableResourceMode.");
                ChangeState<ProcedureCheckVersion>(procedureOwner);
            }
        }

        private void OnCreatePackageSuccess(string packageName, ResourceMode resourceMode, IResourcePackage resourcePackage, object userData)
        {
            m_IsCompleted = true;
        }
    }
}