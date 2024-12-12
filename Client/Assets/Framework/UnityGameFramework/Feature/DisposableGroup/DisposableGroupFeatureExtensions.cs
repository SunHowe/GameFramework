namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// DisposableGroup 功能拓展函数。
    /// </summary>
    public static class DisposableGroupFeatureExtensions
    {
        /// <summary>
        /// 获取DisposableGroup实例。
        /// </summary>
        public static DisposableGroup GetDisposableGroup(this FeatureContainer container)
        {
            return container.AddFeature<DisposableGroupFeature>().DisposableGroup;
        }
        
        /// <summary>
        /// 获取DisposableGroup实例。
        /// </summary>
        public static DisposableGroup GetDisposableGroup(this IFeatureContainerOwner owner)
        {
            return owner.FeatureContainer.GetDisposableGroup();
        }
    }
}