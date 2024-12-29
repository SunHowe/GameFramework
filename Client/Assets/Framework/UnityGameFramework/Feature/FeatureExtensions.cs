using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 功能模块拓展方法。
    /// </summary>
    public static class FeatureExtensions
    {
        /// <summary>
        /// 获取GameObject的FeatureOwner实例。
        /// </summary>
        public static IFeatureContainerOwner GetFeatureOwner(this GameObject gameObject)
        {
            return gameObject.GetOrAddComponent<FeatureOwnerObject>();
        }
    }
}