using GameFramework.Fsm;
using GameFramework.Procedure;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 持有功能容器的流程抽象类。
    /// </summary>
    public abstract class FeatureProcedureBase : ProcedureBase, IFeatureContainerOwner
    {
        public FeatureContainer FeatureContainer { get; private set; }

        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            FeatureContainer = new FeatureContainer(this);
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            FeatureContainer.Awake();
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            FeatureContainer.Shutdown();
            base.OnLeave(procedureOwner, isShutdown);
        }
    }
}