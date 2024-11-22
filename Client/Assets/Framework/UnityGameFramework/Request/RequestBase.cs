using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 异步请求基类。
    /// </summary>
    public abstract class RequestBase<T> where T : Response, new()
    {
        /// <summary>
        /// 执行异步请求 并返回结果，支持通过传入取消令牌进行中断异步任务。
        /// </summary>
        /// <param name="cancellationToken">取消令牌，用于中断异步任务。</param>
        /// <returns>异步任务响应包实例。</returns>
        public async UniTask<T> Execute(CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                // 请求已经被取消，无需执行后续逻辑。
                return Response.Create<T>().SetErrorCode(FrameworkErrorCode.RequestCancelled);
            }

            bool isCanceled;
            T response;

            try
            {
                (isCanceled, response) = await OnExecute(cancellationToken).SuppressCancellationThrow();
            }
            catch (Exception e)
            {
                // 异步任务执行过程中发生异常，返回异常响应包。
                return Response.Create<T>().SetException(e);
            }

            if (response != null)
            {
                return response;
            }

            // 请求被取消
            if (isCanceled)
            {
                return Response.Create<T>().SetErrorCode(FrameworkErrorCode.RequestCancelled);
            }
            
            // 请求违背取消 但返回空响应包
            return Response.Create<T>().SetErrorCode(FrameworkErrorCode.ResponseEmpty);
        }

        protected abstract UniTask<T> OnExecute(CancellationToken cancellationToken = default);
    }
}