using System;

namespace UnityGameFramework.Runtime
{
    public partial class EntityComponent
    {
        /// <summary>
        /// 自增长的实体ID。
        /// </summary>
        private int m_EntityIdIncrease;
        
        /// <summary>
        /// 获取一个自增长的实体id。
        /// </summary>
        public int SpawnEntityId()
        {
            if (m_EntityIdIncrease >= int.MaxValue)
            {
                m_EntityIdIncrease = 0;
            }
            
            return ++m_EntityIdIncrease;
        }
        
        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <returns>自增长的实体编号。</returns>
        public int ShowEntity<T>(string entityAssetName, string entityGroupName) where T : EntityLogic
        {
            return ShowEntity(typeof(T), entityAssetName, entityGroupName, DefaultPriority, null);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <returns>自增长的实体编号。</returns>
        public int ShowEntity(Type entityLogicType, string entityAssetName, string entityGroupName)
        {
            return ShowEntity(entityLogicType, entityAssetName, entityGroupName, DefaultPriority, null);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <returns>自增长的实体编号。</returns>
        public int ShowEntity<T>(string entityAssetName, string entityGroupName, int priority) where T : EntityLogic
        {
            return ShowEntity(typeof(T), entityAssetName, entityGroupName, priority, null);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <returns>自增长的实体编号。</returns>
        public int ShowEntity(Type entityLogicType, string entityAssetName, string entityGroupName, int priority)
        {
            return ShowEntity(entityLogicType, entityAssetName, entityGroupName, priority, null);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>自增长的实体编号。</returns>
        public int ShowEntity<T>(string entityAssetName, string entityGroupName, object userData) where T : EntityLogic
        {
            return ShowEntity(typeof(T), entityAssetName, entityGroupName, DefaultPriority, userData);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>自增长的实体编号。</returns>
        public int ShowEntity(Type entityLogicType, string entityAssetName, string entityGroupName, object userData)
        {
            return ShowEntity(entityLogicType, entityAssetName, entityGroupName, DefaultPriority, userData);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>自增长的实体编号。</returns>
        public int ShowEntity<T>(string entityAssetName, string entityGroupName, int priority, object userData) where T : EntityLogic
        {
            return ShowEntity(typeof(T), entityAssetName, entityGroupName, priority, userData);
        }

        /// <summary>
        /// 使用自增长的实体id显示实体。
        /// </summary>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="priority">加载实体资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>自增长的实体编号。</returns>
        public int ShowEntity(Type entityLogicType, string entityAssetName, string entityGroupName, int priority, object userData)
        {
            if (entityLogicType == null)
            {
                Log.Error("Entity type is invalid.");
                return 0;
            }

            var entityId = SpawnEntityId();
            m_EntityManager.ShowEntity(entityId, entityAssetName, entityGroupName, priority, ShowEntityInfo.Create(entityLogicType, userData));
            return entityId;
        }
    }
}