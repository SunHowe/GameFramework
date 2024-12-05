using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// WebRequestComponent 异步拓展支持。
    /// </summary>
    public partial class WebRequestComponent
    {
        private readonly Dictionary<int, AutoResetUniTaskCompletionSource<WebRequestResponse>> m_CompletionSourceDict = new Dictionary<int, AutoResetUniTaskCompletionSource<WebRequestResponse>>();

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri)
        {
            return SendWebRequest(webRequestUri, null, null, null, DefaultPriority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, byte[] postData)
        {
            return SendWebRequest(webRequestUri, postData, null, null, DefaultPriority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="wwwForm">WWW 表单。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, WWWForm wwwForm)
        {
            return SendWebRequest(webRequestUri, null, wwwForm, null, DefaultPriority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, string tag)
        {
            return SendWebRequest(webRequestUri, null, null, tag, DefaultPriority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, int priority)
        {
            return SendWebRequest(webRequestUri, null, null, null, priority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, byte[] postData, string tag)
        {
            return SendWebRequest(webRequestUri, postData, null, tag, DefaultPriority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="wwwForm">WWW 表单。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, WWWForm wwwForm, string tag)
        {
            return SendWebRequest(webRequestUri, null, wwwForm, tag, DefaultPriority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, byte[] postData, int priority)
        {
            return SendWebRequest(webRequestUri, postData, null, null, priority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="wwwForm">WWW 表单。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, WWWForm wwwForm, int priority)
        {
            return SendWebRequest(webRequestUri, null, wwwForm, null, priority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, string tag, int priority)
        {
            return SendWebRequest(webRequestUri, null, null, tag, priority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, byte[] postData, string tag, int priority)
        {
            return SendWebRequest(webRequestUri, postData, null, tag, priority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="wwwForm">WWW 表单。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, WWWForm wwwForm, string tag, int priority)
        {
            return SendWebRequest(webRequestUri, null, wwwForm, tag, priority, CancellationToken.None);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, null, null, null, DefaultPriority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, byte[] postData, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, postData, null, null, DefaultPriority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="wwwForm">WWW 表单。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, WWWForm wwwForm, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, null, wwwForm, null, DefaultPriority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, string tag, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, null, null, tag, DefaultPriority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, int priority, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, null, null, null, priority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, byte[] postData, string tag, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, postData, null, tag, DefaultPriority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="wwwForm">WWW 表单。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, WWWForm wwwForm, string tag, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, null, wwwForm, tag, DefaultPriority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, byte[] postData, int priority, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, postData, null, null, priority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="wwwForm">WWW 表单。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, WWWForm wwwForm, int priority, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, null, wwwForm, null, priority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, string tag, int priority, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, null, null, tag, priority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, byte[] postData, string tag, int priority, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, postData, null, tag, priority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="wwwForm">WWW 表单。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        public UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, WWWForm wwwForm, string tag, int priority, CancellationToken cancellationToken)
        {
            return SendWebRequest(webRequestUri, null, wwwForm, tag, priority, cancellationToken);
        }

        /// <summary>
        /// 发送 Web 请求，并异步返回结果。
        /// </summary>
        /// <param name="webRequestUri">Web 请求地址。</param>
        /// <param name="postData">要发送的数据流。</param>
        /// <param name="wwwForm">WWW 表单。</param>
        /// <param name="tag">Web 请求任务的标签。</param>
        /// <param name="priority">Web 请求任务的优先级。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>新增 Web 请求任务的序列编号。</returns>
        private UniTask<WebRequestResponse> SendWebRequest(string webRequestUri, byte[] postData, WWWForm wwwForm, string tag, int priority, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                // 请求已经被取消。
                return UniTask.FromResult(Response.Create<WebRequestResponse>().SetErrorCode(FrameworkErrorCode.RequestCancelled));
            }
            
            var serialId = m_WebRequestManager.AddWebRequest(webRequestUri, postData, tag, priority, WWWFormInfo.Create(wwwForm, null));
            var completionSource = AutoResetUniTaskCompletionSource<WebRequestResponse>.Create();
            m_CompletionSourceDict.Add(serialId, completionSource);
            
            cancellationToken.Register(OnTokenCancelled, serialId);

            return completionSource.Task;
        }

        /// <summary>
        /// 取消令牌被标记为取消。
        /// </summary>
        private void OnTokenCancelled(object obj)
        {
            if (obj is not int serialId)
            {
                return;
            }

            if (!m_CompletionSourceDict.TryGetValue(serialId, out var completionSource))
            {
                return;
            }

            if (!m_WebRequestManager.RemoveWebRequest(serialId))
            {
                return;
            }

            m_CompletionSourceDict.Remove(serialId);
            completionSource.TrySetResult(Response.Create<WebRequestResponse>().SetErrorCode(FrameworkErrorCode.RequestCancelled));
        }

        /// <summary>
        /// 尝试设置异步等待结果。
        /// </summary>
        private bool TrySetAwaitResult(int serialId, GameFramework.WebRequest.WebRequestSuccessEventArgs successEventArgs)
        {
            if (!m_CompletionSourceDict.TryGetValue(serialId, out var completionSource))
            {
                return false;
            }
            
            m_CompletionSourceDict.Remove(serialId);
            completionSource.TrySetResult(Response.Create<WebRequestResponse>().SetRawData(successEventArgs.GetWebResponseBytes()));
            return true;
        }

        /// <summary>
        /// 尝试设置异步等待结果。
        /// </summary>
        private bool TrySetAwaitResult(int serialId, GameFramework.WebRequest.WebRequestFailureEventArgs failureEventArgs)
        {
            if (!m_CompletionSourceDict.TryGetValue(serialId, out var completionSource))
            {
                return false;
            }

            m_CompletionSourceDict.Remove(serialId);
            completionSource.TrySetResult(Response.Create<WebRequestResponse>()
                .SetException(new GameFrameworkException(failureEventArgs.ErrorMessage))
                .SetErrorCode(FrameworkErrorCode.WebRequestFailure));
            return true;
        }
    }
}