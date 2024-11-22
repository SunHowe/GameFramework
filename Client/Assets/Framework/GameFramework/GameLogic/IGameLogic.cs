namespace GameFramework
{
    /// <summary>
    /// 游戏逻辑接口
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// 启动逻辑
        /// </summary>
        void Awake();

        /// <summary>
        /// 销毁逻辑
        /// </summary>
        void Shutdown();
    }
}