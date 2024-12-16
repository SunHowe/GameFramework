using GameFramework.Fsm;
using GameFramework.Procedure;

namespace GameMono
{
    /// <summary>
    /// 检测版本流程。
    /// </summary>
    internal class ProcedureCheckVersion : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            // TODO: 检测版本
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            ChangeState<ProcedureLaunchGameLogic>(procedureOwner);
        }
    }
}