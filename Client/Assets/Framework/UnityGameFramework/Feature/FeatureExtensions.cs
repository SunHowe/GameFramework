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

        /// <summary>
        /// 添加子功能。
        /// </summary>
        public static T AddFeature<T>(this IFeatureContainerOwner owner) where T : class, IFeature, new()
        {
            return owner.FeatureContainer.AddFeature<T>();
        }

        /// <summary>
        /// 获取指定类型的子功能。
        /// </summary>
        public static T GetFeature<T>(this IFeatureContainerOwner owner) where T : class, IFeature
        {
            return owner.FeatureContainer.GetFeature<T>();
        }

        /// <summary>
        /// 判断是否有指定的子功能
        /// </summary>
        public static bool HasFeature<T>(this IFeatureContainerOwner owner) where T : class, IFeature
        {
            return owner.FeatureContainer.HasFeature<T>();
        }

        /// <summary>
        /// 移除指定子功能。
        /// </summary>
        public static void RemoveFeature<T>(this IFeatureContainerOwner owner) where T : class, IFeature
        {
            owner.FeatureContainer.RemoveFeature<T>();
        }
    }
}