using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    public partial class EntityComponent
    {
        /// <summary>
        /// 记录正在Await中的实体id集合。
        /// </summary>
        private readonly HashSet<int> m_AwaitLoadingEntityIdSet = new HashSet<int>();
        
        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync<T>(string entityAssetName, string entityGroupName, CancellationToken token) where T : EntityLogic
        {
            return ShowEntityAsync(typeof(T), entityAssetName, entityGroupName, DefaultPriority, null, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync(Type entityLogicType, string entityAssetName, string entityGroupName, CancellationToken token)
        {
            return ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, DefaultPriority, null, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync<T>(string entityAssetName, string entityGroupName, int priority, CancellationToken token) where T : EntityLogic
        {
            return ShowEntityAsync(typeof(T), entityAssetName, entityGroupName, priority, null, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync(Type entityLogicType, string entityAssetName, string entityGroupName, int priority, CancellationToken token)
        {
            return ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, null, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync<T>(string entityAssetName, string entityGroupName, object userData, CancellationToken token) where T : EntityLogic
        {
            return ShowEntityAsync(typeof(T), entityAssetName, entityGroupName, DefaultPriority, userData, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync(Type entityLogicType, string entityAssetName, string entityGroupName, object userData, CancellationToken token)
        {
            return ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, DefaultPriority, userData, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync<T>(string entityAssetName, string entityGroupName, int priority, object userData, CancellationToken token) where T : EntityLogic
        {
            return ShowEntityAsync(typeof(T), entityAssetName, entityGroupName, priority, userData, token);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync(Type entityLogicType, string entityAssetName, string entityGroupName, int priority, object userData, CancellationToken token)
        {
            return ShowEntityAsync(SpawnEntityId(), entityLogicType, entityAssetName, entityGroupName, priority, userData, token);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync<T>(int entityId, string entityAssetName, string entityGroupName, CancellationToken token) where T : EntityLogic
        {
            return ShowEntityAsync(entityId, typeof(T), entityAssetName, entityGroupName, DefaultPriority, null, token);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName, CancellationToken token)
        {
            return ShowEntityAsync(entityId, entityLogicType, entityAssetName, entityGroupName, DefaultPriority, null, token);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync<T>(int entityId, string entityAssetName, string entityGroupName, int priority, CancellationToken token) where T : EntityLogic
        {
            return ShowEntityAsync(entityId, typeof(T), entityAssetName, entityGroupName, priority, null, token);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName, int priority, CancellationToken token)
        {
            return ShowEntityAsync(entityId, entityLogicType, entityAssetName, entityGroupName, priority, null, token);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync<T>(int entityId, string entityAssetName, string entityGroupName, object userData, CancellationToken token) where T : EntityLogic
        {
            return ShowEntityAsync(entityId, typeof(T), entityAssetName, entityGroupName, DefaultPriority, userData, token);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName, object userData, CancellationToken token)
        {
            return ShowEntityAsync(entityId, entityLogicType, entityAssetName, entityGroupName, DefaultPriority, userData, token);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync<T>(int entityId, string entityAssetName, string entityGroupName, int priority, object userData, CancellationToken token) where T : EntityLogic
        {
            return ShowEntityAsync(entityId, typeof(T), entityAssetName, entityGroupName, priority, userData, token);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌票据。</param>
        public UniTask<Entity> ShowEntityAsync(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName, int priority, object userData, CancellationToken token)
        {
            if (entityLogicType == null)
            {
                Log.Error("Entity type is invalid.");
                return UniTask.FromResult<Entity>(null);
            }

            if (token.IsCancellationRequested)
            {
                // 已经取消加载。
                return UniTask.FromResult<Entity>(null);
            }
            
            token.Register(OnCancellationTokenCancel, entityId);
            m_AwaitLoadingEntityIdSet.Add(entityId);

            var task = AutoResetUniTaskCompletionSource<Entity>.Create();
            m_EntityManager.ShowEntity(entityId, entityAssetName, entityGroupName, priority, ShowEntityInfo.Create(entityLogicType, userData, task));
            return task.Task;
        }

        /// <summary>
        /// 取消令牌标记为取消。
        /// </summary>
        private void OnCancellationTokenCancel(object obj)
        {
            if (obj is int entityId && m_AwaitLoadingEntityIdSet.Remove(entityId))
            {
                m_EntityManager.HideEntity(entityId);
            }
        }

        /// <summary>
        /// 检查Await拓展功能。
        /// </summary>
        private void CheckAwaitExtensions(GameFramework.Entity.ShowEntitySuccessEventArgs eventArgs)
        {
            var showEntityInfo = (ShowEntityInfo)eventArgs.UserData;
            if (showEntityInfo.Task != null)
            {
                m_AwaitLoadingEntityIdSet.Remove(eventArgs.Entity.Id);
                showEntityInfo.Task.TrySetResult((Entity)eventArgs.Entity);
            }
        }

        /// <summary>
        /// 检查Await拓展功能。
        /// </summary>
        private void CheckAwaitExtensions(GameFramework.Entity.ShowEntityFailureEventArgs eventArgs)
        {
            var showEntityInfo = (ShowEntityInfo)eventArgs.UserData;
            if (showEntityInfo.Task != null)
            {
                m_AwaitLoadingEntityIdSet.Remove(eventArgs.EntityId);
                showEntityInfo.Task.TrySetResult(null);
            }
        }
    }
}