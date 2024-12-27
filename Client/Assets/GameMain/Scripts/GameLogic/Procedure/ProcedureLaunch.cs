using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 热更启动流程-初始化基础模块。
    /// </summary>
    internal sealed class ProcedureLaunch : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            // 配置表模块。
            GameLogicComponent.Instance.AddGameLogic<DataTableModule>();
            // 登录模块。
            GameLogicComponent.Instance.AddGameLogic<LoginModule>();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            
            ChangeState<ProcedureLoadLocalization>(procedureOwner);
        }
    }
}