using System.Threading;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 异步功能。提供一些异步相关的支持。
    /// </summary>
    public sealed class AsyncFeature : Feature
    {
        /// <summary>
        /// 取消令牌。当功能模块被销毁时，会被标记为取消，
        /// </summary>
        public CancellationToken CancellationToken => (m_CancellationTokenSource ??= new CancellationTokenSource()).Token;

        private CancellationTokenSource m_CancellationTokenSource;

        public override void Awake(object featureOwner, FeatureContainer featureContainer)
        {
            base.Awake(featureOwner, featureContainer);
            m_CancellationTokenSource = null;
        }

        public override void Shutdown()
        {
            m_CancellationTokenSource?.Cancel();
        }
    }
}