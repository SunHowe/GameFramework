using System;
using Cysharp.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 响应包拓展方法。
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// 尝试将当前响应包包转为指定指定类型的响应包。
        /// 若响应包对应的结果是成功，则要求包类型完全一致，不一致时将创建对应的类型的响应包，并设置类型不一致的错误码。
        /// 若响应包对应的结果是失败，则允许包类型不一致，不一致时将创建对应的类型的响应包，并设置原始的错误码。
        /// </summary>
        public static UniTask<T> As<T>(this UniTask<Response> uniTask) where T : Response, new()
        {
            return uniTask.ContinueWith(response => response.As<T>());
        }

        /// <summary>
        /// 等异步任务返回后 仅需它的错误码作为结果返回
        /// </summary>
        public static async UniTask<int> PickCode(this UniTask<Response> uniTask)
        {
            using var response = await uniTask;
            return response.ErrorCode;
        }

        /// <summary>
        /// 将响应包标记为成功，并返回响应包实例。
        /// </summary>
        public static T SetSuccess<T>(this T response) where T : Response, new()
        {
            response.ErrorCode = 0;
            return response;
        }

        /// <summary>
        /// 使用框架错误码设置响应包，并返回响应包实例。
        /// </summary>
        public static T SetErrorCode<T>(this T response, FrameworkErrorCode errorCode) where T : Response, new()
        {
            response.ErrorCode = (int)errorCode;
            return response;
        }

        /// <summary>
        /// 使用指定错误码设置响应包，并返回响应包实例。
        /// </summary>
        public static T SetErrorCode<T>(this T response, int errorCode) where T : Response, new()
        {
            response.ErrorCode = errorCode;
            return response;
        }

        /// <summary>
        /// 使用指定异常设置响应包，并返回响应包实例。
        /// </summary>
        public static T SetException<T>(this T response, Exception exception) where T : Response, new()
        {
            response.Exception = exception;
            response.ErrorCode = (int)FrameworkErrorCode.RequestException;
            return response;
        }
    }
}