using GameFramework.Fsm;
using GameFramework.Procedure;

namespace GameMono
{
    /// <summary>
    /// 检查应用版本流程
    /// </summary>
    internal class CheckAppVersionProcedure : ProcedureBase
    {
        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            ChangeState<CheckResVersionProcedure>(procedureOwner);
        }
    }
}