using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 异步请求拓展方法。
    /// </summary>
    public static class RequestExtensions
    {
        /// <summary>
        /// 执行异步请求 当指定的超时时间触发后 请求将自动取消 并返回被取消的错误码响应包。
        /// </summary>
        /// <param name="request">请求实例。</param>
        /// <param name="timeout">超时时间，毫秒。</param>
        /// <param name="cancellationToken">取消令牌，若有传入取消令牌，当取消令牌被取消或超时时，都会中止任务。</param>
        /// <returns>响应包实例。</returns>
        public static UniTask<T> Execute<T>(this RequestBase<T> request, int timeout, CancellationToken cancellationToken = default) where T : Response, new()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                // 请求已经被取消，无需执行后续逻辑。
                return UniTask.FromResult(Response.Create<T>().SetErrorCode(FrameworkErrorCode.RequestCancelled));
            }

            if (timeout <= 0)
            {
                // 超时时间为0，直接执行请求、
                return request.Execute(cancellationToken);
            }

            // 创建超时令牌
            using var cancellationSource = new CancellationTokenSource(timeout);

            if (cancellationToken == default)
            {
                // 未传入取消令牌，则直接用超时令牌进行执行
                return request.Execute(cancellationSource.Token);
            }

            // 合并取消令牌
            using var combinedCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cancellationSource.Token);

            // 使用合并后的取消令牌进行执行
            return request.Execute(combinedCancellationToken.Token);
        }
    }
}