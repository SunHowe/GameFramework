namespace GameFramework
{
    /// <summary>
    /// 游戏逻辑管理器，统一管理游戏内的逻辑实例。
    /// </summary>
    public interface IGameLogicManager
    {
        /// <summary>
        /// 游戏逻辑是否已经销毁。
        /// </summary>
        public bool IsShutdown { get; }
        
        /// <summary>
        /// 添加受托管的游戏逻辑实例。
        /// </summary>
        /// <param name="gameLogic">游戏逻辑实例。</param>
        void AddGameLogic(IGameLogic gameLogic);

        /// <summary>
        /// 移除受托管的游戏逻辑实例
        /// </summary>
        /// <param name="gameLogic">游戏逻辑实例。</param>
        void RemoveGameLogic(IGameLogic gameLogic);
    }
}