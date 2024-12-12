using System.Threading;

namespace UnityGameFramework.Runtime.FairyGUI.Async
{
    /// <summary>
    /// FGUI界面 异步功能拓展函数。
    /// </summary>
    public static class FGUIFormAsyncExtensions
    {
        /// <summary>
        /// 获取异步取消令牌。其会在界面销毁时标记为取消。
        /// </summary>
        public static CancellationToken GetCancellationTokenOnRecycle(this FGUIFormLogic owner)
        {
            return owner.FeatureContainerOnInit.AddFeature<AsyncFeature>().CancellationToken;
        }

        /// <summary>
        /// 获取异步取消令牌。其会在界面关闭时标记为取消。
        /// </summary>
        public static CancellationToken GetCancellationTokenOnClose(this FGUIFormLogic owner)
        {
            return owner.FeatureContainerOnOpen.AddFeature<AsyncFeature>().CancellationToken;
        }
        
        /// <summary>
        /// 获取异步取消令牌。其会在组件销毁时标记为取消。
        /// </summary>
        public static CancellationToken GetCancellationTokenOnDispose(this IFGUICustomComponent owner)
        {
            return owner.FeatureContainerOnInit.AddFeature<AsyncFeature>().CancellationToken;
        }

        /// <summary>
        /// 获取异步取消令牌。其会在组件从舞台移除时标记为取消。
        /// </summary>
        public static CancellationToken GetCancellationTokenOnRemoveFromStage(this IFGUICustomComponent owner)
        {
            return owner.FeatureContainerOnAddedToStage.AddFeature<AsyncFeature>().CancellationToken;
        }
    }
}