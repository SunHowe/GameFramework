using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 平台流程
    /// </summary>
    internal sealed class ProcedureLobby : FeatureProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Info("ProcedureLobby OnEnter");
        }
    }
}