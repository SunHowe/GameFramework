using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

namespace GameMono
{
    /// <summary>
    /// 启动游戏逻辑流程。
    /// </summary>
    internal class ProcedureLaunchGameLogic : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            HotfixComponent.Instance.LoadHotfix();
        }
    }
}