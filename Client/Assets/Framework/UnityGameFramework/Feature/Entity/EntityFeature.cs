using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 实体管理功能模块。用于辅助创建实体，并在功能模块销毁时一并销毁由该功能模块创建的实体。
    /// </summary>
    public sealed class EntityFeature : Feature
    {
        /// <summary>
        /// 实体组件引用。
        /// </summary>
        private EntityComponent m_EntityComponent;

        /// <summary>
        /// 记录由该模块创建的实体id。
        /// </summary>
        private readonly HashSet<int> m_EntityIdSet = new HashSet<int>();

        /// <summary>
        /// 取消令牌源。
        /// </summary>
        private CancellationTokenSource m_CancellationTokenSource;

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="token">取消令牌。</param>
        public UniTask<Entity> ShowEntityAsync<T>(string entityAssetName, string entityGroupName, CancellationToken token) where T : EntityLogic
        {
            token = CreateLinkedCancellationToken(token);
            var task = m_EntityComponent.ShowEntityAsync<T>(entityAssetName, entityGroupName, token);
            return task.ContinueWith(AfterShowEntity);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="token">取消令牌。</param>
        public UniTask<Entity> ShowEntityAsync(Type entityLogicType, string entityAssetName, string entityGroupName, CancellationToken token)
        {
            token = CreateLinkedCancellationToken(token);
            var task = m_EntityComponent.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, token);
            return task.ContinueWith(AfterShowEntity);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="token">取消令牌。</param>
        public UniTask<Entity> ShowEntityAsync<T>(string entityAssetName, string entityGroupName, int priority, CancellationToken token) where T : EntityLogic
        {
            token = CreateLinkedCancellationToken(token);
            var task = m_EntityComponent.ShowEntityAsync<T>(entityAssetName, entityGroupName, priority, token);
            return task.ContinueWith(AfterShowEntity);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="token">取消令牌。</param>
        public UniTask<Entity> ShowEntityAsync(Type entityLogicType, string entityAssetName, string entityGroupName, int priority, CancellationToken token)
        {
            token = CreateLinkedCancellationToken(token);
            var task = m_EntityComponent.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, token);
            return task.ContinueWith(AfterShowEntity);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public UniTask<Entity> ShowEntityAsync<T>(string entityAssetName, string entityGroupName, object userData, CancellationToken token) where T : EntityLogic
        {
            token = CreateLinkedCancellationToken(token);
            var task = m_EntityComponent.ShowEntityAsync<T>(entityAssetName, entityGroupName, userData, token);
            return task.ContinueWith(AfterShowEntity);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public UniTask<Entity> ShowEntityAsync(Type entityLogicType, string entityAssetName, string entityGroupName, object userData, CancellationToken token)
        {
            token = CreateLinkedCancellationToken(token);
            var task = m_EntityComponent.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, userData, token);
            return task.ContinueWith(AfterShowEntity);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public UniTask<Entity> ShowEntityAsync<T>(string entityAssetName, string entityGroupName, int priority, object userData, CancellationToken token) where T : EntityLogic
        {
            token = CreateLinkedCancellationToken(token);
            var task = m_EntityComponent.ShowEntityAsync<T>(entityAssetName, entityGroupName, priority, userData, token);
            return task.ContinueWith(AfterShowEntity);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="token">取消令牌。</param>
        public UniTask<Entity> ShowEntityAsync(Type entityLogicType, string entityAssetName, string entityGroupName, int priority, object userData, CancellationToken token)
        {
            token = CreateLinkedCancellationToken(token);
            var task = m_EntityComponent.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, userData, token);
            return task.ContinueWith(AfterShowEntity);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        public void HideEntity(int entityId)
        {
            if (!m_EntityIdSet.Remove(entityId))
            {
                return;
            }

            m_EntityComponent.HideEntity(entityId);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void HideEntity(int entityId, object userData)
        {
            if (!m_EntityIdSet.Remove(entityId))
            {
                return;
            }

            m_EntityComponent.HideEntity(entityId, userData);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="entity">实体。</param>
        public void HideEntity(Entity entity)
        {
            if (!m_EntityIdSet.Remove(entity.Id))
            {
                return;
            }

            m_EntityComponent.HideEntity(entity);
        }

        /// <summary>
        /// 隐藏实体。
        /// </summary>
        /// <param name="entity">实体。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void HideEntity(Entity entity, object userData)
        {
            if (!m_EntityIdSet.Remove(entity.Id))
            {
                return;
            }

            m_EntityComponent.HideEntity(entity, userData);
        }

        /// <summary>
        /// 隐藏所有已加载的实体。
        /// </summary>
        public void HideAllLoadedEntities()
        {
            if (m_EntityIdSet.Count == 0)
            {
                return;
            }

            foreach (var id in m_EntityIdSet)
            {
                m_EntityComponent.HideEntity(id);
            }
            
            m_EntityIdSet.Clear();
        }

        /// <summary>
        /// 隐藏所有已加载的实体。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public void HideAllLoadedEntities(object userData)
        {
            if (m_EntityIdSet.Count == 0)
            {
                return;
            }

            foreach (var id in m_EntityIdSet)
            {
                m_EntityComponent.HideEntity(id, userData);
            }
            
            m_EntityIdSet.Clear();
        }

        /// <summary>
        /// 隐藏所有正在加载的实体。
        /// </summary>
        public void HideAllLoadingEntities()
        {
            if (m_CancellationTokenSource == null)
            {
                return;
            }

            m_CancellationTokenSource.Cancel();
            m_CancellationTokenSource.Dispose();
            m_CancellationTokenSource = null;
        }

        private UniTask<Entity> AfterShowEntity(Entity entity)
        {
            if (entity != null)
            {
                m_EntityIdSet.Add(entity.Id);
            }
            
            return UniTask.FromResult(entity);
        }

        /// <summary>
        /// 创建取消令牌。
        /// </summary>
        private CancellationToken CreateLinkedCancellationToken(CancellationToken token)
        {
            var basicToken = (m_CancellationTokenSource ??= new CancellationTokenSource()).Token;
            if (token == default)
                return basicToken;
            
            return CancellationTokenSource.CreateLinkedTokenSource(basicToken, token).Token;
        }

        public override void Awake(object featureOwner, FeatureContainer featureContainer)
        {
            base.Awake(featureOwner, featureContainer);

            m_EntityComponent = EntityComponent.Instance;
        }

        public override void Shutdown()
        {
            HideAllLoadedEntities();
            HideAllLoadingEntities();

            m_EntityComponent = null;
        }
    }
}