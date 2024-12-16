using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime.FairyGUI;

namespace GameMono
{
    /// <summary>
    /// 闪屏流程。
    /// </summary>
    internal class ProcedureSplash : ProcedureBase
    {
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            FGUIComponent.Instance.RegisterFGUIFormAndCustomComponent(GetType().Assembly);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            
            // TODO: 播放闪屏动画
            ChangeState<ProcedureCreateLauncherPackage>(procedureOwner);
        }
    }
}