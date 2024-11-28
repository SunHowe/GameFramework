using System;
using System.Collections.Generic;
using GameFramework.ObjectPool;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源管理器。
    /// </summary>
    internal partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        private IResourcePackageHelper m_ResourcePackageHelper;

        /// <summary>
        /// 获取或设置资源对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        public float AssetAutoReleaseInterval
        {
            get { return m_AssetPool.AutoReleaseInterval; }
            set { m_AssetPool.AutoReleaseInterval = value; }
        }

        /// <summary>
        /// 获取或设置资源对象池的容量。
        /// </summary>
        public int AssetCapacity
        {
            get { return m_AssetPool.Capacity; }
            set { m_AssetPool.Capacity = value; }
        }

        /// <summary>
        /// 获取或设置资源对象池对象过期秒数。
        /// </summary>
        public float AssetExpireTime
        {
            get { return m_AssetPool.ExpireTime; }
            set { m_AssetPool.ExpireTime = value; }
        }

        /// <summary>
        /// 获取或设置资源对象池的优先级。
        /// </summary>
        public int AssetPriority
        {
            get { return m_AssetPool.Priority; }
            set { m_AssetPool.Priority = value; }
        }

        public string DefaultPackageResourceVersion => DefaultPackage?.ResourceVersion ?? string.Empty;

        public ResourceManager()
        {
            m_ResourcePackageDict = new Dictionary<string, IResourcePackage>(StringComparer.Ordinal);
            m_ResourcePackages = new List<IResourcePackage>();
            m_ResourcePackagesWhileCreate = new Dictionary<string, CreatePackagePackInfo>(StringComparer.Ordinal);
            m_CreateDefaultPackageCallbacks = new CreatePackageCallbacks(OnCreateDefaultPackageSuccess, OnCreateDefaultPackageFailure);
            m_CreatePackageCallbacks = new CreatePackageCallbacks(OnCreatePackageSuccess, OnCreatePackageFailure);

            m_AssetBelongsToPackageDict = new Dictionary<string, string>(StringComparer.Ordinal);
            m_LoadAssetPacksInfoDict = new Dictionary<string, LoadAssetPacksInfo>(StringComparer.Ordinal);
            m_LoadAssetCallbacks = new LoadAssetCallbacks(OnLoadAssetSuccess, OnLoadAssetFailure);
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        internal override void Shutdown()
        {
        }

        public void SetResourcePackageHelper(IResourcePackageHelper helper)
        {
            m_ResourcePackageHelper = helper;
        }

        public void SetObjectPoolManager(IObjectPoolManager objectPoolManager)
        {
            m_AssetPool = objectPoolManager.CreateMultiSpawnObjectPool<AssetObject>("Asset Pool");
        }
    }
}