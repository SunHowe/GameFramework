using GameFramework.Procedure;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 热更新应用程序。
    /// </summary>
    public class HotfixApp : HotfixAppBase
    {
        protected override void OnAwake()
        {
            // 重新载入流程状态机
            var entranceProcedure = new LaunchProcedure();
            var procedures = new ProcedureBase[]
            {
                entranceProcedure,
                new LoadDataTableProcedure(),
                new LoginProcedure(),
                new LobbyProcedure(),
            };
            
            ProcedureComponent.Instance.StartProcedure(procedures, entranceProcedure);
        }

        protected override void OnShutdown()
        {
        }
    }
}