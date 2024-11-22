namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 框架定义的错误码
    /// </summary>
    public enum FrameworkErrorCode
    {
        Success = 0,
        
        /// <summary>
        /// 游戏正在被销毁
        /// </summary>
        GameShutdown,
        
        /// <summary>
        /// 返回的响应包为空。
        /// </summary>
        ResponseEmpty,
        
        /// <summary>
        /// 返回的响应包类型不匹配。
        /// </summary>
        ResponseTypeNotMatch,
        
        /// <summary>
        /// 请求被取消。
        /// </summary>
        RequestCancelled,
        
        /// <summary>
        /// 请求触发异常。
        /// </summary>
        RequestException,
        
        /// <summary>
        /// 超过该值的错误码给游戏逻辑使用。
        /// </summary>
        Max = 100000,
    }
}