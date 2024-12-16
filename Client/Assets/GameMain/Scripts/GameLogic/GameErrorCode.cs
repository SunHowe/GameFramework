using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 游戏逻辑定义的错误码。
    /// </summary>
    public enum GameErrorCode
    {
        Min = FrameworkErrorCode.Max,

        #region [登录模块 100001 - 100100]

        /// <summary>
        /// 重复的登录请求。
        /// </summary>
        LoginRequestDuplicate = 100001,
        
        #endregion
    }

    public static class GameErrorCodeExtensions
    {
        /// <summary>
        /// 使用逻辑错误码设置响应包，并返回响应包实例。
        /// </summary>
        public static T SetErrorCode<T>(this T response, GameErrorCode errorCode) where T : Response, new()
        {
            response.ErrorCode = (int)errorCode;
            return response;
        }
    }
}