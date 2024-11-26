namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 功能持有者拓展方法。
    /// </summary>
    public static class FeatureOwnerExtensions
    {
        /// <summary>
        /// 获取指定类型的子功能。
        /// </summary>
        public static T GetFeature<T>(this IFeatureOwner owner) where T : class, IFeature
        {
            return owner.FeatureContainer?.GetFeature<T>();
        }

        /// <summary>
        /// 判断是否有指定的子功能
        /// </summary>
        public static bool HasFeature<T>(this IFeatureOwner owner) where T : class, IFeature
        {
            return owner.FeatureContainer?.HasFeature<T>() ?? false;
        }

        /// <summary>
        /// 添加指定子功能。
        /// </summary>
        public static T AddFeature<T>(this IFeatureOwner owner) where T : class, IFeature, new()
        {
            var featureContainer = owner.FeatureContainer;
            if (featureContainer == null)
            {
                featureContainer = new FeatureContainer(owner);
                owner.FeatureContainer = featureContainer;
            }
            
            return featureContainer.AddFeature<T>();
        }

        /// <summary>
        /// 移除指定子功能。
        /// </summary>
        public static void RemoveFeature<T>(this IFeatureOwner owner) where T : class, IFeature
        {
            owner.FeatureContainer?.RemoveFeature<T>();
        }
    }
}