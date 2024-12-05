using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 游戏逻辑定义的错误码。
    /// </summary>
    public enum GameErrorCode
    {
        Min = FrameworkErrorCode.Max,
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