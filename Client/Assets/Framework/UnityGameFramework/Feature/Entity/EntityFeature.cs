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
        /// 父级模块。
        /// </summary>
        private EntityFeature m_Parent;

        /// <summary>
        /// 记录由该模块创建的实体id。
        /// </summary>
        private readonly HashSet<int> m_EntityIdSet = new HashSet<int>();

        /// <summary>
        /// 取消令牌源。
        /// </summary>
        private CancellationTokenSource m_CancellationTokenSource;

        /// <summary>
        /// 设置父级模块。若存在父级模块则从父模块上进行实际的实体管理。
        /// </summary>
        public void SetParent(EntityFeature parent)
        {
            if (m_CancellationTokenSource != null || m_EntityIdSet.Count != 0)
            {
                throw new Exception("It's not allow to set parent when entity feature is running.");
            }

            m_Parent = parent;
        }

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
            var task = m_Parent?.ShowEntityAsync<T>(entityAssetName, entityGroupName, token) ?? m_EntityComponent.ShowEntityAsync<T>(entityAssetName, entityGroupName, token);
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
            var task = m_Parent?.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, token) ?? m_EntityComponent.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, token);
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
            var task = m_Parent?.ShowEntityAsync<T>(entityAssetName, entityGroupName, priority, token) ?? m_EntityComponent.ShowEntityAsync<T>(entityAssetName, entityGroupName, priority, token);
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
            var task = m_Parent?.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, token) ?? m_EntityComponent.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, token);
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
            var task = m_Parent?.ShowEntityAsync<T>(entityAssetName, entityGroupName, userData, token) ?? m_EntityComponent.ShowEntityAsync<T>(entityAssetName, entityGroupName, userData, token);
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
            var task = m_Parent?.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, userData, token) ?? m_EntityComponent.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, userData, token);
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
            var task = m_Parent?.ShowEntityAsync<T>(entityAssetName, entityGroupName, priority, userData, token) ?? m_EntityComponent.ShowEntityAsync<T>(entityAssetName, entityGroupName, priority, userData, token);
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
            var task = m_Parent?.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, userData, token) ?? m_EntityComponent.ShowEntityAsync(entityLogicType, entityAssetName, entityGroupName, priority, userData, token);
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

            if (m_Parent != null)
            {
                m_Parent.HideEntity(entityId);
            }
            else
            {
                m_EntityComponent.HideEntity(entityId);
            }
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

            if (m_Parent != null)
            {
                m_Parent.HideEntity(entityId, userData);
            }
            else
            {
                m_EntityComponent.HideEntity(entityId, userData);
            }
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

            if (m_Parent != null)
            {
                m_Parent.HideEntity(entity);
            }
            else
            {
                m_EntityComponent.HideEntity(entity);
            }
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

            if (m_Parent != null)
            {
                m_Parent.HideEntity(entity, userData);
            }
            else
            {
                m_EntityComponent.HideEntity(entity, userData);
            }
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

            if (m_Parent != null)
            {
                foreach (var id in m_EntityIdSet)
                {
                    m_Parent.HideEntity(id);
                }
            }
            else
            {
                foreach (var id in m_EntityIdSet)
                {
                    m_EntityComponent.HideEntity(id);
                }
            }
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

            if (m_Parent != null)
            {
                foreach (var id in m_EntityIdSet)
                {
                    m_Parent.HideEntity(id, userData);
                }
            }
            else
            {
                foreach (var id in m_EntityIdSet)
                {
                    m_EntityComponent.HideEntity(id, userData);
                }
            }
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

        public override void Shutdown()
        {
        }
    }
}