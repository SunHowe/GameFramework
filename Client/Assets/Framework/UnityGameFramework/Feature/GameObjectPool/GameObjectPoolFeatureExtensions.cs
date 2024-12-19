using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// GameObject对象池模块拓展方法。
    /// </summary>
    public static class GameObjectPoolFeatureExtensions
    {
        /// <summary>
        /// 获取实例对象池容量。
        /// </summary>
        public static int GetGameObjectPoolCapacity(this FeatureContainer container)
        {
            return container.GetFeature<GameObjectPoolFeature>()?.Capacity ?? GameObjectPoolFeature.DEFAULT_CAPACITY;
        }

        /// <summary>
        /// 设置实例对象池容量。
        /// </summary>
        public static void SetGameObjectPoolCapacity(this FeatureContainer container, int capacity)
        {
            container.AddFeature<GameObjectPoolFeature>().Capacity = capacity;
        }

        /// <summary>
        /// 获取实例对象池对象过期秒数。
        /// </summary>
        public static float GetGameObjectPoolExpireTime(this FeatureContainer container)
        {
            return container.GetFeature<GameObjectPoolFeature>()?.ExpireTime ?? GameObjectPoolFeature.DEFAULT_EXPIRE_TIME;
        }

        /// <summary>
        /// 设置实例对象池对象过期秒数。
        /// </summary>
        public static void SetGameObjectPoolExpireTime(this FeatureContainer container, float expireTime)
        {
            container.AddFeature<GameObjectPoolFeature>().ExpireTime = expireTime;
        }

        /// <summary>
        /// 获取实例对象池的优先级。
        /// </summary>
        public static int GetGameObjectPoolPriority(this FeatureContainer container)
        {
            return container.GetFeature<GameObjectPoolFeature>()?.Priority ?? GameObjectPoolFeature.DEFAULT_PRIORITY;
        }

        /// <summary>
        /// 设置实例对象池的优先级。
        /// </summary>
        public static void SetGameObjectPoolPriority(this FeatureContainer container, int priority)
        {
            container.AddFeature<GameObjectPoolFeature>().Priority = priority;
        }

        /// <summary>
        /// 获取实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        public static float GetGameObjectPoolAutoReleaseInterval(this FeatureContainer container)
        {
            return container.GetFeature<GameObjectPoolFeature>()?.AutoReleaseInterval ?? GameObjectPoolFeature.DEFAULT_AUTO_RELEASE_INTERVAL;
        }

        /// <summary>
        /// 设置实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        public static void SetGameObjectPoolAutoReleaseInterval(this FeatureContainer container, float autoReleaseInterval)
        {
            container.AddFeature<GameObjectPoolFeature>().AutoReleaseInterval = autoReleaseInterval;
        }
        
        /// <summary>
        /// 同步实例化资源。仅支持在对象池已有缓存时使用。
        /// </summary>
        public static GameObject Instantiate(this FeatureContainer container, string assetName)
        {
            return container.AddFeature<GameObjectPoolFeature>().Instantiate(assetName);
        }

        /// <summary>
        /// 异步实例化资源。若资源在对象池中不存在则会加载资源并实例化新的对象。
        /// </summary>
        public static UniTask<GameObject> InstantiateAsync(this FeatureContainer container, string assetName)
        {
            return container.AddFeature<GameObjectPoolFeature>().InstantiateAsync(assetName);
        }

        /// <summary>
        /// 归还GameObject实例。
        /// </summary>
        public static void Destroy(this FeatureContainer container, GameObject gameObject)
        {
            var feature = container.GetFeature<GameObjectPoolFeature>();
            if (feature != null)
            {
                feature.Destroy(gameObject);
                return;
            }

            if (gameObject != null)
            {
                Object.Destroy(gameObject);
            }
        }
        
        /// <summary>
        /// 获取实例对象池容量。
        /// </summary>
        public static int GetGameObjectPoolCapacity(this IFeatureContainerOwner owner)
        {
            return owner.FeatureContainer.GetGameObjectPoolCapacity();
        }

        /// <summary>
        /// 设置实例对象池容量。
        /// </summary>
        public static void SetGameObjectPoolCapacity(this IFeatureContainerOwner owner, int capacity)
        {
            owner.FeatureContainer.SetGameObjectPoolCapacity(capacity);
        }

        /// <summary>
        /// 获取实例对象池对象过期秒数。
        /// </summary>
        public static float GetGameObjectPoolExpireTime(this IFeatureContainerOwner owner)
        {
            return owner.FeatureContainer.GetGameObjectPoolExpireTime();
        }

        /// <summary>
        /// 设置实例对象池对象过期秒数。
        /// </summary>
        public static void SetGameObjectPoolExpireTime(this IFeatureContainerOwner owner, float expireTime)
        {
            owner.FeatureContainer.SetGameObjectPoolExpireTime(expireTime);
        }

        /// <summary>
        /// 获取实例对象池的优先级。
        /// </summary>
        public static int GetGameObjectPoolPriority(this IFeatureContainerOwner owner)
        {
            return owner.FeatureContainer.GetGameObjectPoolPriority();
        }

        /// <summary>
        /// 设置实例对象池的优先级。
        /// </summary>
        public static void SetGameObjectPoolPriority(this IFeatureContainerOwner owner, int priority)
        {
            owner.FeatureContainer.SetGameObjectPoolPriority(priority);
        }

        /// <summary>
        /// 获取实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        public static float GetGameObjectPoolAutoReleaseInterval(this IFeatureContainerOwner owner)
        {
            return owner.FeatureContainer.GetGameObjectPoolAutoReleaseInterval();
        }

        /// <summary>
        /// 设置实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        public static void SetGameObjectPoolAutoReleaseInterval(this IFeatureContainerOwner owner, float autoReleaseInterval)
        {
            owner.FeatureContainer.SetGameObjectPoolAutoReleaseInterval(autoReleaseInterval);
        }
        
        /// <summary>
        /// 同步实例化资源。仅支持在对象池已有缓存时使用。
        /// </summary>
        public static GameObject Instantiate(this IFeatureContainerOwner owner, string assetName)
        {
            return owner.FeatureContainer.AddFeature<GameObjectPoolFeature>().Instantiate(assetName);
        }

        /// <summary>
        /// 异步实例化资源。若资源在对象池中不存在则会加载资源并实例化新的对象。
        /// </summary>
        public static UniTask<GameObject> InstantiateAsync(this IFeatureContainerOwner owner, string assetName)
        {
            return owner.FeatureContainer.AddFeature<GameObjectPoolFeature>().InstantiateAsync(assetName);
        }

        /// <summary>
        /// 归还GameObject实例。
        /// </summary>
        public static void Destroy(this IFeatureContainerOwner owner, GameObject gameObject)
        {
            owner.FeatureContainer.AddFeature<GameObjectPoolFeature>().Destroy(gameObject);
        }
    }
}