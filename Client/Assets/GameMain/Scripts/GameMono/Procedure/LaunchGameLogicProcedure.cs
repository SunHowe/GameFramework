using GameFramework.Fsm;
using GameFramework.Procedure;

namespace GameMono
{
    /// <summary>
    /// 启动游戏逻辑流程
    /// </summary>
    public class LaunchGameLogicProcedure : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Hotfix.LoadHotfix();
        }
    }
}