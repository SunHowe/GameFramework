using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using GameMono.UI.Launch;
using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;

namespace GameMono
{
    /// <summary>
    /// 打开启动界面流程。
    /// </summary>
    public class ProcedureOpenLaunchForm : ProcedureBase
    {
        private bool m_IsDone;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_IsDone = false;
            EventComponent.Instance.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            
            FGUIComponent.Instance.OpenUIForm<LaunchForm>();
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            
            EventComponent.Instance.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!m_IsDone)
            {
                return;
            }
            
            ChangeState<ProcedureCreateDefaultPackage>(procedureOwner);
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            var eventArgs = (OpenUIFormSuccessEventArgs) e;
            if (eventArgs.UIForm.UIFormAssetName != FGUIComponent.Instance.GetUIFormAssetName<LaunchForm>())
            {
                return;
            }
            
            m_IsDone = true;
        }
    }
}