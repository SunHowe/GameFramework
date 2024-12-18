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
            var entranceProcedure = new ProcedureLaunch();
            var procedures = new ProcedureBase[]
            {
                entranceProcedure,
                new ProcedureLoadLocalization(),
                new ProcedureLoadDataTable(),
                new ProcedureLoadMainUIMapping(),
                new ProcedureLogin(),
                new ProcedureLobby(),
            };
            
            ProcedureComponent.Instance.StartProcedure(procedures, entranceProcedure);
        }

        protected override void OnShutdown()
        {
        }
    }
}