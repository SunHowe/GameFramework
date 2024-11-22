using GameFramework.Fsm;
using GameFramework.Procedure;

namespace GameMono
{
    /// <summary>
    /// 启动流程 进行必要的工具初始化工作
    /// </summary>
    internal class LaunchProcedure : ProcedureBase
    {
        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            ChangeState<CheckAppVersionProcedure>(procedureOwner);
        }
    }
}