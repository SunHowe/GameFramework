using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 资源加载功能模块拓展方法。
    /// </summary>
    public static class ResourceFeatureExtensions
    {
        /// <summary>
        /// 异步加载资源。
        /// </summary>
        public static UniTask<T> LoadAssetAsync<T>(this FeatureContainer container, string assetName) where T : Object
        {
            return container.AddFeature<ResourceFeature>().LoadAssetAsync<T>(assetName);
        }

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        public static UniTask<T> LoadAssetAsync<T>(this FeatureContainer container, string assetName, int priority) where T : Object
        {
            return container.AddFeature<ResourceFeature>().LoadAssetAsync<T>(assetName, priority);
        }

        /// <summary>
        /// 异步加载二进制文件
        /// </summary>
        public static UniTask<byte[]> LoadBinaryAsync(this FeatureContainer container, string assetName)
        {
            return container.AddFeature<ResourceFeature>().LoadBinaryAsync(assetName);
        }

        /// <summary>
        /// 设置父级资源模块。
        /// </summary>
        public static void SetParent(this FeatureContainer container, ResourceFeature parent)
        {
            container.AddFeature<ResourceFeature>().SetParent(parent);
        }
        
        /// <summary>
        /// 异步加载资源。
        /// </summary>
        public static UniTask<T> LoadAssetAsync<T>(this IFeatureContainerOwner owner, string assetName) where T : Object
        {
            return owner.FeatureContainer.LoadAssetAsync<T>(assetName);
        }

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        public static UniTask<T> LoadAssetAsync<T>(this IFeatureContainerOwner owner, string assetName, int priority) where T : Object
        {
            return owner.FeatureContainer.LoadAssetAsync<T>(assetName, priority);
        }

        /// <summary>
        /// 异步加载二进制文件
        /// </summary>
        public static UniTask<byte[]> LoadBinaryAsync(this IFeatureContainerOwner owner, string assetName)
        {
            return owner.FeatureContainer.LoadBinaryAsync(assetName);
        }

        /// <summary>
        /// 设置父级资源模块。
        /// </summary>
        public static void SetParent(IFeatureContainerOwner owner, ResourceFeature parent)
        {
            owner.FeatureContainer.SetParent(parent);
        }
    }
}