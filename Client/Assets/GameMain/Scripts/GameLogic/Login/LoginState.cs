namespace GameLogic
{
    /// <summary>
    /// 登录状态枚举。
    /// </summary>
    public enum LoginState
    {
        /// <summary>
        /// 未登录。
        /// </summary>
        None = 0,
        
        /// <summary>
        /// 连接服务器中。
        /// </summary>
        ConnectServer,
        
        /// <summary>
        /// 登录请求交互中。
        /// </summary>
        LoginRequest,
        
        /// <summary>
        /// 登录完成，游戏中。
        /// </summary>
        OnGame,
        
        /// <summary>
        /// 游戏中断开链接。
        /// </summary>
        Disconnect,
        
        /// <summary>
        /// 重连服务器中。
        /// </summary>
        ReconnectServer,
        
        /// <summary>
        /// 重连请求交互中。
        /// </summary>
        ReconnectRequest,
    }
}