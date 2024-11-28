using GameFramework.Fsm;
using GameFramework.Procedure;

namespace GameMono
{
    /// <summary>
    /// 闪屏流程。
    /// </summary>
    public class ProcedureSplash : ProcedureBase
    {
        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            
            // TODO: 播放闪屏动画
            ChangeState<ProcedureCreateLauncherPackage>(procedureOwner);
        }
    }
}