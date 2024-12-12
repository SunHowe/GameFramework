namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 功能容器持有者。
    /// </summary>
    public interface IFeatureContainerOwner
    {
        /// <summary>
        /// 获取功能容器。
        /// </summary>
        public FeatureContainer FeatureContainer { get; }
    }
}