using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 平台模块。作为平台业务的入口。平台的业务采用子逻辑的方式添加在该模块下。
    /// </summary>
    public sealed class LobbyModule : GameLogicBase<LobbyModule>
    {
        protected override void OnAwake()
        {
            // 添加平台子模块。
            
            // this.AddSubGameLogic<PlayerModule>(); // 角色模块
            // this.AddSubGameLogic<BackpackModule>(); // 背包模块
            // this.AddSubGameLogic<QuestModule>(); // 任务模块
            // ...
        }

        protected override void OnShutdown()
        {
        }
    }
}