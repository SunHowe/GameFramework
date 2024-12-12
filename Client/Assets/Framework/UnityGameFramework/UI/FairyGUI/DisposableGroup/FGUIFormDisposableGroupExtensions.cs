namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FGUI界面DisposableGroup功能拓展函数。
    /// </summary>
    public static class FGUIFormDisposableGroupExtensions
    {
        /// <summary>
        /// 获取DisposableGroup实例。它会在界面被回收时调用Dispose()。
        /// </summary>
        public static DisposableGroup GetDisposableGroupOnRecycle(this FGUIFormLogic owner)
        {
            return owner.FeatureContainerOnInit.GetDisposableGroup();
        }
        
        /// <summary>
        /// 获取DisposableGroup实例。它会在界面被关闭时调用Dispose()。
        /// </summary>
        public static DisposableGroup GetDisposableGroupOnClose(this FGUIFormLogic owner)
        {
            return owner.FeatureContainerOnOpen.GetDisposableGroup();
        }
        
        /// <summary>
        /// 获取DisposableGroup实例。它会在组件被销毁时调用Dispose()。
        /// </summary>
        public static DisposableGroup GetDisposableGroupOnDispose(this IFGUICustomComponent owner)
        {
            return owner.FeatureContainerOnInit.GetDisposableGroup();
        }
        
        /// <summary>
        /// 获取DisposableGroup实例。它会在组件从舞台移除时调用Dispose()。
        /// </summary>
        public static DisposableGroup GetDisposableGroupOnRemoveFromStage(this IFGUICustomComponent owner)
        {
            return owner.FeatureContainerOnAddedToStage.GetDisposableGroup();
        }
    }
}