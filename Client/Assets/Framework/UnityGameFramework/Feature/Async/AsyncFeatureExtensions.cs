using System.Threading;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 异步请求功能拓展函数。
    /// </summary>
    public static class AsyncFeatureExtensions
    {
        /// <summary>
        /// 获取异步取消令牌。
        /// </summary>
        public static CancellationToken GetCancellationToken(this FeatureContainer container)
        {
            return container.AddFeature<AsyncFeature>().CancellationToken;
        }
        
        /// <summary>
        /// 获取异步取消令牌。
        /// </summary>
        public static CancellationToken GetCancellationToken(this IFeatureContainerOwner owner)
        {
            return owner.FeatureContainer.AddFeature<AsyncFeature>().CancellationToken;
        }
    }
}