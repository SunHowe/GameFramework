using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 远程异步请求组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/RemoteRequest")]
    public sealed class RemoteRequestComponent : GameFrameworkComponent<RemoteRequestComponent>
    {
        private readonly Dictionary<int, AutoResetUniTaskCompletionSource<Response>> m_TaskDict = new();
        private int m_RequestId = 0;

        /// <summary>
        /// 注册一个远程请求。远程请求会分配全局唯一的请求id，用于设置响应包时与响应关联。
        /// </summary>
        /// <param name="token">取消令牌，当令牌被标记为取消时，会直接返回。</param>
        /// <returns>唯一的请求id和用于等待的task</returns>
        public (int requestId, UniTask<T> uniTask) RegisterRemoteRequest<T>(CancellationToken token = default) where T : Response, new()
        {
            var (requestId, uniTask) = RegisterRemoteRequest(token);
            
            // 这里会根据响应包类型做进一步检测。
            return (requestId, uniTask.As<T>());
        }

        /// <summary>
        /// 注册一个远程请求。远程请求会分配全局唯一的请求id，用于设置响应包时与响应关联。
        /// </summary>
        /// <param name="token">取消令牌，当令牌被标记为取消时，会直接返回。</param>
        /// <returns>唯一的请求id和用于等待的task</returns>
        public (int requestId, UniTask<Response> uniTask) RegisterRemoteRequest(CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                // 已经被取消，直接返回即可。
                return (-1, UniTask.FromCanceled<Response>(token));
            }

            int requestId;
            var source = AutoResetUniTaskCompletionSource<Response>.Create();

            lock (m_TaskDict)
            {
                requestId = ++m_RequestId;
                m_TaskDict.Add(requestId, source);
            }

            if (token != default)
            {
                token.Register(OnTokenCancelled, requestId);
            }

            return (requestId, source.Task);
        }

        /// <summary>
        /// 令牌取消回调，令指定的响应包返回取消。
        /// </summary>
        private void OnTokenCancelled(object userdata)
        {
            if (userdata is int requestId)
            {
                SetResponse(requestId, Response.Create().SetErrorCode(FrameworkErrorCode.RequestCancelled));
            }
        }

        /// <summary>
        /// 为指定请求id的远程请求设置响应包实例。
        /// </summary>
        /// <param name="requestId">请求id，RegisterRemoteRequest步骤创建。</param>
        /// <param name="response">响应包实例。</param>
        public void SetResponse(int requestId, Response response)
        {
            AutoResetUniTaskCompletionSource<Response> source;

            lock (m_TaskDict)
            {
                if (!m_TaskDict.Remove(requestId, out source))
                {
                    // 无人认领的响应包，直接回收。
                    response.Dispose();
                    return;
                }
            }

            source.TrySetResult(response);
        }

        /// <summary>
        /// 使用指定的错误码打断指定的远程请求执行
        /// </summary>
        /// <param name="requestId">请求id，RegisterRemoteRequest步骤创建。</param>
        /// <param name="errorCode">错误码。</param>
        public void InterruptRemoteRequest(int requestId, int errorCode = (int)FrameworkErrorCode.RequestCancelled)
        {
            SetResponse(requestId, Response.Create().SetErrorCode(errorCode));
        }

        /// <summary>
        /// 使用指定的错误码打断所有的远程请求执行。
        /// </summary>
        /// <param name="errorCode">错误码。</param>
        public void InterruptAllRemoteRequest(int errorCode = (int)FrameworkErrorCode.RequestCancelled)
        {
            foreach (var requestId in m_TaskDict.Keys.ToList())
            {
                InterruptRemoteRequest(requestId, errorCode);
            }
        }

        protected override void OnDestroy()
        {
            InterruptAllRemoteRequest((int)FrameworkErrorCode.GameShutdown);
            base.OnDestroy();
        }
    }
}