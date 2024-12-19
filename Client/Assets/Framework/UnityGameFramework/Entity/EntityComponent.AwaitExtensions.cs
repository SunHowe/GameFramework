using System;
using Cysharp.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    public partial class EntityComponent
    {
        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        public UniTask<Entity> ShowEntityAsync<T>(int entityId, string entityAssetName, string entityGroupName) where T : EntityLogic
        {
            return ShowEntityAsync(entityId, typeof(T), entityAssetName, entityGroupName, DefaultPriority, null);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        public UniTask<Entity> ShowEntityAsync(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName)
        {
            return ShowEntityAsync(entityId, entityLogicType, entityAssetName, entityGroupName, DefaultPriority, null);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        public UniTask<Entity> ShowEntityAsync<T>(int entityId, string entityAssetName, string entityGroupName, int priority) where T : EntityLogic
        {
            return ShowEntityAsync(entityId, typeof(T), entityAssetName, entityGroupName, priority, null);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        public UniTask<Entity> ShowEntityAsync(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName, int priority)
        {
            return ShowEntityAsync(entityId, entityLogicType, entityAssetName, entityGroupName, priority, null);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        public UniTask<Entity> ShowEntityAsync<T>(int entityId, string entityAssetName, string entityGroupName, object userData) where T : EntityLogic
        {
            return ShowEntityAsync(entityId, typeof(T), entityAssetName, entityGroupName, DefaultPriority, userData);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        public UniTask<Entity> ShowEntityAsync(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName, object userData)
        {
            return ShowEntityAsync(entityId, entityLogicType, entityAssetName, entityGroupName, DefaultPriority, userData);
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
        public UniTask<Entity> ShowEntityAsync<T>(int entityId, string entityAssetName, string entityGroupName, int priority, object userData) where T : EntityLogic
        {
            return ShowEntityAsync(entityId, typeof(T), entityAssetName, entityGroupName, priority, userData);
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
        public UniTask<Entity> ShowEntityAsync(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName, int priority, object userData)
        {
            if (entityLogicType == null)
            {
                Log.Error("Entity type is invalid.");
                return UniTask.FromResult<Entity>(null);
            }

            var task = AutoResetUniTaskCompletionSource<Entity>.Create();
            m_EntityManager.ShowEntity(entityId, entityAssetName, entityGroupName, priority, ShowEntityInfo.Create(entityLogicType, userData, task));
            return task.Task;
        }

        /// <summary>
        /// 检查Await拓展功能。
        /// </summary>
        private void CheckAwaitExtensions(GameFramework.Entity.ShowEntitySuccessEventArgs eventArgs)
        {
            var showEntityInfo = (ShowEntityInfo)eventArgs.UserData;
            if (showEntityInfo.Task != null)
            {
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
                showEntityInfo.Task.TrySetResult(null);
            }
        }
    }
}