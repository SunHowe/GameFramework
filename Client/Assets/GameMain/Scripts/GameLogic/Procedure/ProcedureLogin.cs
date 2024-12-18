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
        private int m_FormId;
        
        protected override void OnAddFeatures()
        {
        }
        
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Info("LoginProcedure OnEnter, Close Launch UI");
            m_FormId = FGUIComponent.Instance.OpenUIForm<LoginForm>();
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            FGUIComponent.Instance.CloseUIForm(m_FormId);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            
            if (LoginModule.Instance.State >= LoginState.OnGame)
            {
                ChangeState<ProcedureLobby>(procedureOwner);
            }
        }
    }
}