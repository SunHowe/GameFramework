using System;
using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 异步任务响应包实例，各模块可根据所需自行定义派生自该类的响应包类型。调用Dispose时会进行引用池归还。
    /// </summary>
    public class Response : IReference, IDisposable
    {
        /// <summary>
        /// 错误码 0代表成功
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 是否成功。
        /// </summary>
        public bool IsSuccess => ErrorCode == 0;
        
        /// <summary>
        /// 触发的异常，仅当ErrorCode=FrameworkErrorCode.RequestException时有值
        /// </summary>
        public Exception Exception { get; set; }

        public virtual void Clear()
        {
            ErrorCode = 0;
            Exception = null;
        }

        public void Dispose()
        {
            ReferencePool.Release(this);
        }

        /// <summary>
        /// 创建一个通用应答包实例。
        /// </summary>
        public static Response Create()
        {
            return ReferencePool.Acquire<Response>();
        }

        /// <summary>
        /// 创建一个指定类型的响应包实例。
        /// </summary>
        public static T Create<T>() where T : Response, new()
        {
            return ReferencePool.Acquire<T>();
        }

        /// <summary>
        /// 尝试将当前响应包包转为指定指定类型的响应包。
        /// 若响应包对应的结果是成功，则要求包类型完全一致，不一致时将创建对应的类型的响应包，并设置类型不一致的错误码。
        /// 若响应包对应的结果是失败，则允许包类型不一致，不一致时将创建对应的类型的响应包，并设置原始的错误码。
        /// </summary>
        /// <typeparam name="T">目标响应包类型。</typeparam>
        /// <returns>目标类型的响应包实例，一定是非空值。</returns>
        public T As<T>() where T : Response, new()
        {
            if (this is T response)
            {
                // 当前响应包类型与目标类型一致，直接返回。
                return response;
            }

            response = Create<T>();

            if (ErrorCode == 0)
            {
                // 当前响应包类型与目标类型不一致，返回类型不一致的错误码。
                response.SetErrorCode(FrameworkErrorCode.ResponseTypeNotMatch);
            }
            else
            {
                // 当前响应包返回了错误，则将原始错误码传递给目标响应包。
                response.ErrorCode = ErrorCode;
            }

            // 回收当前包
            Dispose();

            return response;
        }
    }
}