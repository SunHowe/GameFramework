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
        private readonly Dictionary<string, string> m_AssetBelongsToPackageDict;
        private IObjectPool<AssetObject> m_AssetPool;
        
        private readonly Dictionary<string, LoadAssetPacksInfo> m_LoadAssetPacksInfoDict;
        private readonly LoadAssetCallbacks m_LoadAssetCallbacks;

        public HasAssetResult HasAsset(string assetName)
        {
            HasAssetResult hasAssetResult;
            foreach (var resourcePackage in m_ResourcePackages)
            {
                hasAssetResult = resourcePackage.HasAsset(assetName);
                if (hasAssetResult != HasAssetResult.NotExist)
                {
                    return hasAssetResult;
                }
            }

            return HasAssetResult.NotExist;
        }

        private IResourcePackage FindAssetPackage(string assetName)
        {
            if (m_AssetBelongsToPackageDict.TryGetValue(assetName, out var packageName))
            {
                return GetResourcePackage(packageName);
            }

            foreach (var resourcePackage in m_ResourcePackages)
            {
                if (resourcePackage.HasAsset(assetName) == HasAssetResult.NotExist)
                {
                    continue;
                }

                m_AssetBelongsToPackageDict.Add(assetName, resourcePackage.PackageName);
                return resourcePackage;
            }

            return null;
        }

        public void LoadAsset(string assetName, LoadAssetCallbacks callbacks)
        {
            LoadAsset(assetName, null, Constant.DefaultPriority, callbacks, null);
        }

        public void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks callbacks)
        {
            LoadAsset(assetName, assetType, Constant.DefaultPriority, callbacks, null);
        }

        public void LoadAsset(string assetName, int priority, LoadAssetCallbacks callbacks)
        {
            LoadAsset(assetName, null, priority, callbacks, null);
        }

        public void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks callbacks)
        {
            LoadAsset(assetName, assetType, priority, callbacks, null);
        }

        public void LoadAsset(string assetName, LoadAssetCallbacks callbacks, object userData)
        {
            LoadAsset(assetName, null, Constant.DefaultPriority, callbacks, userData);
        }

        public void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks callbacks, object userData)
        {
            LoadAsset(assetName, assetType, Constant.DefaultPriority, callbacks, userData);
        }

        public void LoadAsset(string assetName, int priority, LoadAssetCallbacks callbacks, object userData)
        {
            LoadAsset(assetName, null, priority, callbacks, userData);
        }

        public void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks callbacks, object userData)
        {
            var assetObject = m_AssetPool.Spawn(assetName);
            if (assetObject != null)
            {
                callbacks.LoadAssetSuccessCallback(assetName, assetObject.Target, 0f, userData);
                return;
            }

            if (!m_LoadAssetPacksInfoDict.TryGetValue(assetName, out var packsInfo))
            {
                var package = FindAssetPackage(assetName);
                if (package == null)
                {
                    if (callbacks.LoadAssetFailureCallback != null)
                    {
                        callbacks.LoadAssetFailureCallback(assetName, LoadResourceStatus.NotExist, "Asset not exists in loaded packages.", userData);
                    }

                    return;
                }
                
                packsInfo = LoadAssetPacksInfo.Create();
                packsInfo.PackInfos.Enqueue(LoadAssetPackInfo.Create(callbacks, userData));
                m_LoadAssetPacksInfoDict.Add(assetName, packsInfo);

                package.LoadAsset(assetName, assetType, priority, m_LoadAssetCallbacks, package);
            }
            else
            {
                packsInfo.PackInfos.Enqueue(LoadAssetPackInfo.Create(callbacks, userData));
            }
        }

        public void UnloadAsset(object asset)
        {
            m_AssetPool.Unspawn(asset);
        }

        private void OnLoadAssetSuccess(string assetName, object asset, float duration, object userData)
        {
            var package = (IResourcePackage)userData;
            
            if (!m_LoadAssetPacksInfoDict.TryGetValue(assetName, out var packsInfo))
            {
                GameFrameworkLog.Fatal(Utility.Text.Format("Cannot find load asset packs info with asset name '{0}'.", assetName));
                package.UnloadAsset(assetName);
                return;
            }

            m_LoadAssetPacksInfoDict.Remove(assetName);

            var assetObject = AssetObject.Create(assetName, package, asset);
            m_AssetPool.Register(assetObject, false);

            while (packsInfo.PackInfos.Count > 0)
            {
                var packInfo = packsInfo.PackInfos.Dequeue();
                packInfo.Callbacks.LoadAssetSuccessCallback(assetName, m_AssetPool.Spawn(assetName).Target, duration, packInfo.UserData);
            }
            
            ReferencePool.Release(packsInfo);
        }

        private void OnLoadAssetFailure(string assetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            if (!m_LoadAssetPacksInfoDict.TryGetValue(assetName, out var packsInfo))
            {
                GameFrameworkLog.Fatal(Utility.Text.Format("Cannot find load asset packs info with asset name '{0}'.", assetName));
                return;
            }

            m_LoadAssetPacksInfoDict.Remove(assetName);
            
            while (packsInfo.PackInfos.Count > 0)
            {
                var packInfo = packsInfo.PackInfos.Dequeue();

                if (packInfo.Callbacks.LoadAssetFailureCallback != null)
                {
                    packInfo.Callbacks.LoadAssetFailureCallback(assetName, status, errorMessage, packInfo.UserData);
                }
            }
            
            ReferencePool.Release(packsInfo);
        }
    }
}