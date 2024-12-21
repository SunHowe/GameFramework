using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 实体管理功能模块拓展方法。
    /// </summary>
    public static class EntityFeatureExtensions
    {
        /// <summary>
        /// 设置父级模块。若存在父级模块则从父模块上进行实际的实体管理。
        /// </summary>
        public static void SetEntityFeatureParent(this FeatureContainer container, EntityFeature parent)
        {
            container.AddFeature<EntityFeature>().SetParent(parent);
        }
        
        /// <summary>
        /// 设置父级模块。若存在父级模块则从父模块上进行实际的实体管理。
        /// </summary>
        public static void SetEntityFeatureParent(this IFeatureContainerOwner owner, EntityFeature parent)
        {
            owner.FeatureContainer.SetEntityFeatureParent(parent);
        }
        
        /// <summary>
        /// 设置父级模块。若存在父级模块则从父模块上进行实际的实体管理。
        /// </summary>
        public static void SetEntityFeatureParent(this FeatureContainer container, FeatureContainer parent)
        {
            container.AddFeature<EntityFeature>().SetParent(parent.AddFeature<EntityFeature>());
        }
        
        /// <summary>
        /// 设置父级模块。若存在父级模块则从父模块上进行实际的实体管理。
        /// </summary>
        public static void SetEntityFeatureParent(this IFeatureContainerOwner owner, IFeatureContainerOwner parent)
        {
            owner.FeatureContainer.SetEntityFeatureParent(parent.FeatureContainer);
        }
        
        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="container"></param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync<T>(this FeatureContainer container, string entityAssetName, string entityGroupName, CancellationToken token) where T : EntityLogic
        {
            return container.AddFeature<EntityFeature>().ShowEntityAsync<T>(entityAssetName, entityGroupName, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync(this FeatureContainer container, Type entityLogicType, string entityAssetName, string entityGroupName, CancellationToken token)
        {
            return container.AddFeature<EntityFeature>().ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="container"></param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync<T>(this FeatureContainer container, string entityAssetName, string entityGroupName, int priority, CancellationToken token) where T : EntityLogic
        {
            return container.AddFeature<EntityFeature>().ShowEntityAsync<T>(entityAssetName, entityGroupName, priority, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync(this FeatureContainer container, Type entityLogicType, string entityAssetName, string entityGroupName, int priority, CancellationToken token)
        {
            return container.AddFeature<EntityFeature>().ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="container"></param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync<T>(this FeatureContainer container, string entityAssetName, string entityGroupName, object userData, CancellationToken token) where T : EntityLogic
        {
            return container.AddFeature<EntityFeature>().ShowEntityAsync<T>(entityAssetName, entityGroupName, userData, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync(this FeatureContainer container, Type entityLogicType, string entityAssetName, string entityGroupName, object userData, CancellationToken token)
        {
            return container.AddFeature<EntityFeature>().ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, userData, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="container"></param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync<T>(this FeatureContainer container, string entityAssetName, string entityGroupName, int priority, object userData, CancellationToken token) where T : EntityLogic
        {
            return container.AddFeature<EntityFeature>().ShowEntityAsync<T>(entityAssetName, entityGroupName, priority, userData, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync(this FeatureContainer container, Type entityLogicType, string entityAssetName, string entityGroupName, int priority, object userData, CancellationToken token)
        {
            return container.AddFeature<EntityFeature>().ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, userData, token);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="entityId">实体编号。</param>
        public static void HideEntity(this FeatureContainer container, int entityId)
        {
            container.GetFeature<EntityFeature>()?.HideEntity(entityId);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="entityId">实体编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public static void HideEntity(this FeatureContainer container, int entityId, object userData)
        {
            container.GetFeature<EntityFeature>()?.HideEntity(entityId, userData);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="entity">实体。</param>
        public static void HideEntity(this FeatureContainer container, Entity entity)
        {
            container.GetFeature<EntityFeature>()?.HideEntity(entity);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="entity">实体。</param>
        /// <param name="userData">用户自定义数据。</param>
        public static void HideEntity(this FeatureContainer container, Entity entity, object userData)
        {
            container.GetFeature<EntityFeature>()?.HideEntity(entity, userData);
        }

        /// <summary>
        /// 隐藏所有已加载的实体。
        /// </summary>
        /// <param name="container"></param>
        public static void HideAllLoadedEntities(this FeatureContainer container)
        {
            container.GetFeature<EntityFeature>()?.HideAllLoadedEntities();
        }

        /// <summary>
        /// 隐藏所有已加载的实体。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="userData">用户自定义数据。</param>
        public static void HideAllLoadedEntities(this FeatureContainer container, object userData)
        {
            container.GetFeature<EntityFeature>()?.HideAllLoadedEntities(userData);
        }

        /// <summary>
        /// 隐藏所有正在加载的实体。
        /// </summary>
        /// <param name="container"></param>
        public static void HideAllLoadingEntities(this FeatureContainer container)
        {
            container.GetFeature<EntityFeature>()?.HideAllLoadingEntities();
        }
        
        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="owner"></param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync<T>(this IFeatureContainerOwner owner, string entityAssetName, string entityGroupName, CancellationToken token) where T : EntityLogic
        {
            return owner.FeatureContainer.ShowEntityAsync<T>(entityAssetName, entityGroupName, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync(this IFeatureContainerOwner owner, Type entityLogicType, string entityAssetName, string entityGroupName, CancellationToken token)
        {
            return owner.FeatureContainer.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="owner"></param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync<T>(this IFeatureContainerOwner owner, string entityAssetName, string entityGroupName, int priority, CancellationToken token) where T : EntityLogic
        {
            return owner.FeatureContainer.ShowEntityAsync<T>(entityAssetName, entityGroupName, priority, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync(this IFeatureContainerOwner owner, Type entityLogicType, string entityAssetName, string entityGroupName, int priority, CancellationToken token)
        {
            return owner.FeatureContainer.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="owner"></param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync<T>(this IFeatureContainerOwner owner, string entityAssetName, string entityGroupName, object userData, CancellationToken token) where T : EntityLogic
        {
            return owner.FeatureContainer.ShowEntityAsync<T>(entityAssetName, entityGroupName, userData, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync(this IFeatureContainerOwner owner, Type entityLogicType, string entityAssetName, string entityGroupName, object userData, CancellationToken token)
        {
            return owner.FeatureContainer.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, userData, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="owner"></param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync<T>(this IFeatureContainerOwner owner, string entityAssetName, string entityGroupName, int priority, object userData, CancellationToken token) where T : EntityLogic
        {
            return owner.FeatureContainer.ShowEntityAsync<T>(entityAssetName, entityGroupName, priority, userData, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public static UniTask<Entity> ShowEntityAsync(this IFeatureContainerOwner owner, Type entityLogicType, string entityAssetName, string entityGroupName, int priority, object userData, CancellationToken token)
        {
            return owner.FeatureContainer.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, userData, token);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="entityId">实体编号。</param>
        public static void HideEntity(this IFeatureContainerOwner owner, int entityId)
        {
            owner.FeatureContainer.HideEntity(entityId);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="entityId">实体编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public static void HideEntity(this IFeatureContainerOwner owner, int entityId, object userData)
        {
            owner.FeatureContainer.HideEntity(entityId, userData);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="entity">实体。</param>
        public static void HideEntity(this IFeatureContainerOwner owner, Entity entity)
        {
            owner.FeatureContainer.HideEntity(entity);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="entity">实体。</param>
        /// <param name="userData">用户自定义数据。</param>
        public static void HideEntity(this IFeatureContainerOwner owner, Entity entity, object userData)
        {
            owner.FeatureContainer.HideEntity(entity, userData);
        }

        /// <summary>
        /// 隐藏所有已加载的实体。
        /// </summary>
        /// <param name="owner"></param>
        public static void HideAllLoadedEntities(this IFeatureContainerOwner owner)
        {
            owner.FeatureContainer.HideAllLoadedEntities();
        }

        /// <summary>
        /// 隐藏所有已加载的实体。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="userData">用户自定义数据。</param>
        public static void HideAllLoadedEntities(this IFeatureContainerOwner owner, object userData)
        {
            owner.FeatureContainer.HideAllLoadedEntities(userData);
        }

        /// <summary>
        /// 隐藏所有正在加载的实体。
        /// </summary>
        /// <param name="owner"></param>
        public static void HideAllLoadingEntities(this IFeatureContainerOwner owner)
        {
            owner.FeatureContainer.HideAllLoadingEntities();
        }
    }
}