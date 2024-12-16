using GameFramework.Fsm;
using GameFramework.Procedure;
using GameLogic.UI.Login;
using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;

namespace GameLogic
{
    /// <summary>
    /// 登录流程
    /// </summary>
    internal sealed class ProcedureLogin : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Info("LoginProcedure OnEnter, Close Launch UI");
            FGUIComponent.Instance.OpenUIForm<LoginForm>();
        }
    }
}