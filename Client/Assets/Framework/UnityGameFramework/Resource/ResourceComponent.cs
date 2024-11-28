using System;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Resource")]
    public sealed partial class ResourceComponent : GameFrameworkComponent<ResourceComponent>
    {
        private IResourceManager m_ResourceManager = null;

        private bool m_ForceUnloadUnusedAssets = false;
        private bool m_PreorderUnloadUnusedAssets = false;
        private bool m_PerformGCCollect = false;
        private AsyncOperation m_AsyncOperation = null;
        private float m_LastUnloadUnusedAssetsOperationElapseSeconds = 0f;

        [SerializeField]
        private string m_ResourcePackageHelperTypeName = "UnityGameFramework.Runtime.FairyGUI.DefaultResourcePackageHelper";
        
        [SerializeField]
        private ResourcePackageHelperBase m_CustomResourcePackageHelper = null;

        [SerializeField]
        private float m_MinUnloadUnusedAssetsInterval = 60f;

        [SerializeField]
        private float m_MaxUnloadUnusedAssetsInterval = 300f;

        #region [Resource Package]

        /// <summary>
        /// 默认资源包实例（需要先通过CreateDefaultResourcePackage创建）。
        /// </summary>
        public IResourcePackage DefaultPackage => m_ResourceManager.DefaultPackage;

        /// <summary>
        /// 获取当前已创建的资源包实例列表。
        /// </summary>
        /// <returns>资源包实例列表。</returns>
        public IResourcePackage[] GetResourcePackages()
        {
            return m_ResourceManager.GetResourcePackages();
        }

        /// <summary>
        /// 获取当前已创建的资源包实例列表
        /// </summary>
        /// <param name="result">资源包实例列表。</param>
        public void GetResourcePackages(List<IResourcePackage> result)
        {
            m_ResourceManager.GetResourcePackages(result);
        }

        /// <summary>
        /// 创建指定名字的资源包。
        /// </summary>
        /// <param name="packageName">包名。</param>
        /// <param name="resourceMode">资源模式。</param>
        /// <param name="callbacks">加载回调。</param>
        public void CreateResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks)
        {
            m_ResourceManager.CreateResourcePackage(packageName, resourceMode, callbacks);
        }

        /// <summary>
        /// 销毁指定的资源包。
        /// </summary>
        /// <param name="packageName">包名。</param>
        public void DestroyResourcePackage(string packageName)
        {
            m_ResourceManager.DestroyResourcePackage(packageName);
        }

        /// <summary>
        /// 获取指定名字的资源包（需要先通过CreateResourcePackage创建）。
        /// </summary>
        /// <param name="packageName">包名。</param>
        /// <returns>资源包实例。</returns>
        public IResourcePackage GetResourcePackage(string packageName)
        {
            return m_ResourceManager.GetResourcePackage(packageName);
        }

        /// <summary>
        /// 获取当前已加载的资源包下所有资源的资源标识。
        /// </summary>
        /// <returns>当前已加载的资源包下所有资源的资源标识。</returns>
        public string[] GetAssetNames()
        {
            return m_ResourceManager.GetAssetNames();
        }

        /// <summary>
        /// 获取当前已加载的资源包下所有资源的资源标识。
        /// </summary>
        /// <param name="results">将资源标识写到该list中。</param>
        public void GetAssetNames(List<string> results)
        {
            m_ResourceManager.GetAssetNames(results);
        }

        /// <summary>
        /// 判断指定资源是否存在。
        /// </summary>
        /// <param name="assetName">资源标识名。</param>
        /// <returns>资源存在判定结果。</returns>
        public HasAssetResult HasAsset(string assetName)
        {
            return m_ResourceManager.HasAsset(assetName);
        }

        #endregion

        #region [Load Asset]

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadAsset(string assetName, LoadAssetCallbacks callbacks)
        {
            m_ResourceManager.LoadAsset(assetName, callbacks);
        }

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="assetType">资源类型。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks callbacks)
        {
            m_ResourceManager.LoadAsset(assetName, assetType, callbacks);
        }

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadAsset(string assetName, int priority, LoadAssetCallbacks callbacks)
        {
            m_ResourceManager.LoadAsset(assetName, priority, callbacks);
        }

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="assetType">资源类型。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks callbacks)
        {
            m_ResourceManager.LoadAsset(assetName, assetType, priority, callbacks);
        }

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadAsset(string assetName, LoadAssetCallbacks callbacks, object userData)
        {
            m_ResourceManager.LoadAsset(assetName, callbacks, userData);
        }

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="assetType">资源类型。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks callbacks, object userData)
        {
            m_ResourceManager.LoadAsset(assetName, assetType, callbacks, userData);
        }

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadAsset(string assetName, int priority, LoadAssetCallbacks callbacks, object userData)
        {
            m_ResourceManager.LoadAsset(assetName, priority, callbacks, userData);
        }

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="assetType">资源类型。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks callbacks, object userData)
        {
            m_ResourceManager.LoadAsset(assetName, assetType, priority, callbacks, userData);
        }

        /// <summary>
        /// 卸载资源。
        /// </summary>
        public void UnloadAsset(object asset)
        {
            m_ResourceManager.UnloadAsset(asset);
        }

        #endregion

        #region [Load Scene]

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadScene(string sceneAssetName, LoadSceneCallbacks callbacks)
        {
            m_ResourceManager.LoadScene(sceneAssetName, callbacks);
        }

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks callbacks)
        {
            m_ResourceManager.LoadScene(sceneAssetName, priority, callbacks);
        }

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadScene(string sceneAssetName, LoadSceneCallbacks callbacks, object userData)
        {
            m_ResourceManager.LoadScene(sceneAssetName, callbacks, userData);
        }

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks callbacks, object userData)
        {
            m_ResourceManager.LoadScene(sceneAssetName, priority, callbacks, userData);
        }

        /// <summary>
        /// 异步卸载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="callbacks">卸载回调。</param>
        public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks callbacks)
        {
            m_ResourceManager.UnloadScene(sceneAssetName, callbacks);
        }

        /// <summary>
        /// 异步卸载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">卸载回调。</param>
        public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks callbacks, object userData)
        {
            m_ResourceManager.UnloadScene(sceneAssetName, callbacks, userData);
        }

        #endregion

        #region [Load Binary]

        /// <summary>
        /// 同步加载二进制数据。
        /// </summary>
        /// <param name="binaryAssetName">二进制资源标识。</param>
        /// <returns>二进制数据，若加载失败则返回null</returns>
        public byte[] LoadBinary(string binaryAssetName)
        {
            return LoadBinary(binaryAssetName, out LoadResourceStatus status);
        }

        /// <summary>
        /// 同步加载二进制数据。
        /// </summary>
        /// <param name="binaryAssetName">二进制资源标识。</param>
        /// <param name="status">加载状态。</param>
        /// <returns>二进制数据，若加载失败则返回null</returns>
        public byte[] LoadBinary(string binaryAssetName, out LoadResourceStatus status)
        {
            return m_ResourceManager.LoadBinary(binaryAssetName, out status);
        }

        /// <summary>
        /// 异步加载二进制数据。
        /// </summary>
        /// <param name="binaryAssetName">二进制资源标识。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadBinary(string binaryAssetName, LoadBinaryCallbacks callbacks)
        {
            m_ResourceManager.LoadBinary(binaryAssetName, callbacks);
        }

        /// <summary>
        /// 异步加载二进制数据。
        /// </summary>
        /// <param name="binaryAssetName">二进制资源标识。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        public void LoadBinary(string binaryAssetName, LoadBinaryCallbacks callbacks, object userData)
        {
            m_ResourceManager.LoadBinary(binaryAssetName, callbacks, userData);
        }

        #endregion

        /// <summary>
        /// 强制执行释放未被使用的资源。
        /// </summary>
        /// <param name="performGCCollect">是否使用垃圾回收。</param>
        public void ForceUnloadUnusedAssets(bool performGCCollect)
        {
            m_ForceUnloadUnusedAssets = true;
            if (performGCCollect)
            {
                m_PerformGCCollect = performGCCollect;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            m_ResourceManager = GameFrameworkEntry.GetModule<IResourceManager>();
            if (m_ResourceManager == null)
            {
                Log.Fatal("Resource manager is invalid.");
                return;
            }

            var packageHelper = Helper.CreateHelper(m_ResourcePackageHelperTypeName, m_CustomResourcePackageHelper);
            if (packageHelper == null)
            {
                Log.Fatal("Can not create resource package helper.");
                return;
            }

            packageHelper.gameObject.name = "Resource Package Helper";
            var helperTransform = packageHelper.transform;
            helperTransform.SetParent(transform);
            helperTransform.localScale = Vector3.one;
            m_ResourceManager.SetResourcePackageHelper(packageHelper);
        }

        private void Update()
        {
            m_LastUnloadUnusedAssetsOperationElapseSeconds += Time.unscaledDeltaTime;
            if (m_AsyncOperation == null && (m_ForceUnloadUnusedAssets || m_LastUnloadUnusedAssetsOperationElapseSeconds >= m_MaxUnloadUnusedAssetsInterval || m_PreorderUnloadUnusedAssets && m_LastUnloadUnusedAssetsOperationElapseSeconds >= m_MinUnloadUnusedAssetsInterval))
            {
                Log.Info("Unload unused assets...");
                m_ForceUnloadUnusedAssets = false;
                m_PreorderUnloadUnusedAssets = false;
                m_LastUnloadUnusedAssetsOperationElapseSeconds = 0f;
                m_AsyncOperation = Resources.UnloadUnusedAssets();
            }

            if (m_AsyncOperation != null && m_AsyncOperation.isDone)
            {
                m_AsyncOperation = null;
                if (m_PerformGCCollect)
                {
                    Log.Info("GC.Collect...");
                    m_PerformGCCollect = false;
                    GC.Collect();
                }
            }
        }
    }
}