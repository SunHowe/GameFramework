using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 登录流程
    /// </summary>
    internal sealed class LoginProcedure : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Info("LoginProcedure OnEnter, Close Launch UI");
        }
    }
}