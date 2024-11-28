using System;
using System.Collections.Generic;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源管理器接口。
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        /// 设置资源包辅助工具实例。
        /// </summary>
        /// <param name="helper">资源包辅助工具。</param>
        void SetResourcePackageHelper(IResourcePackageHelper helper);
        
        #region [Resource Package]

        /// <summary>
        /// 默认资源包实例（需要先通过CreateDefaultResourcePackage创建）。
        /// </summary>
        IResourcePackage DefaultPackage { get; }

        /// <summary>
        /// 获取当前已创建的资源包实例列表。
        /// </summary>
        /// <returns>资源包实例列表。</returns>
        IResourcePackage[] GetResourcePackages();

        /// <summary>
        /// 获取当前已创建的资源包实例列表
        /// </summary>
        /// <param name="result">资源包实例列表。</param>
        void GetResourcePackages(List<IResourcePackage> result);

        /// <summary>
        /// 创建默认的资源包。
        /// </summary>
        /// <param name="packageName">默认资源包包名。</param>
        /// <param name="resourceMode">资源模式。</param>
        /// <param name="callbacks">加载回调。</param>
        void CreateDefaultResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks);

        /// <summary>
        /// 创建指定名字的资源包。
        /// </summary>
        /// <param name="packageName">包名。</param>
        /// <param name="resourceMode">资源模式。</param>
        /// <param name="callbacks">加载回调。</param>
        void CreateResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks);

        /// <summary>
        /// 销毁指定的资源包。
        /// </summary>
        /// <param name="packageName">包名。</param>
        void DestroyResourcePackage(string packageName);

        /// <summary>
        /// 获取指定名字的资源包（需要先通过CreateResourcePackage创建）。
        /// </summary>
        /// <param name="packageName">包名。</param>
        /// <returns>资源包实例。</returns>
        IResourcePackage GetResourcePackage(string packageName);

        /// <summary>
        /// 获取当前已加载的资源包下所有资源的资源标识。
        /// </summary>
        /// <returns>当前已加载的资源包下所有资源的资源标识。</returns>
        string[] GetAssetNames();

        /// <summary>
        /// 获取当前已加载的资源包下所有资源的资源标识。
        /// </summary>
        /// <param name="results">将资源标识写到该list中。</param>
        void GetAssetNames(List<string> results);

        /// <summary>
        /// 判断指定资源是否存在。
        /// </summary>
        /// <param name="assetName">资源标识名。</param>
        /// <returns>资源存在判定结果。</returns>
        HasAssetResult HasAsset(string assetName);

        #endregion

        #region [Load Asset]

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadAsset(string assetName, LoadAssetCallbacks callbacks);

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="assetType">资源类型。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks callbacks);

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadAsset(string assetName, int priority, LoadAssetCallbacks callbacks);

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="assetType">资源类型。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks callbacks);

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadAsset(string assetName, LoadAssetCallbacks callbacks, object userData);

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="assetType">资源类型。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks callbacks, object userData);

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadAsset(string assetName, int priority, LoadAssetCallbacks callbacks, object userData);

        /// <summary>
        /// 异步加载资源，若资源所在的资源包未创建则会当做资源不存在处理。
        /// </summary>
        /// <param name="assetName">资源标识。</param>
        /// <param name="assetType">资源类型。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks callbacks, object userData);

        /// <summary>
        /// 卸载资源。
        /// </summary>
        void UnloadAsset(object asset);

        #endregion

        #region [Load Scene]

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadScene(string sceneAssetName, LoadSceneCallbacks callbacks);

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks callbacks);

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadScene(string sceneAssetName, LoadSceneCallbacks callbacks, object userData);

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="priority">加载优先级。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks callbacks, object userData);

        /// <summary>
        /// 异步卸载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="callbacks">卸载回调。</param>
        void UnloadScene(string sceneAssetName, UnloadSceneCallbacks callbacks);

        /// <summary>
        /// 异步卸载场景。
        /// </summary>
        /// <param name="sceneAssetName">场景资源标识。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">卸载回调。</param>
        void UnloadScene(string sceneAssetName, UnloadSceneCallbacks callbacks, object userData);

        #endregion

        #region [Load Binary]

        /// <summary>
        /// 同步加载二进制数据。
        /// </summary>
        /// <param name="binaryAssetName">二进制资源标识。</param>
        /// <returns>二进制数据，若加载失败则返回null</returns>
        byte[] LoadBinary(string binaryAssetName);

        /// <summary>
        /// 同步加载二进制数据。
        /// </summary>
        /// <param name="binaryAssetName">二进制资源标识。</param>
        /// <param name="status">加载状态。</param>
        /// <returns>二进制数据，若加载失败则返回null</returns>
        byte[] LoadBinary(string binaryAssetName, out LoadResourceStatus status);

        /// <summary>
        /// 异步加载二进制数据。
        /// </summary>
        /// <param name="binaryAssetName">二进制资源标识。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadBinary(string binaryAssetName, LoadBinaryCallbacks callbacks);

        /// <summary>
        /// 异步加载二进制数据。
        /// </summary>
        /// <param name="binaryAssetName">二进制资源标识。</param>
        /// <param name="userData">用户数据。</param>
        /// <param name="callbacks">加载回调。</param>
        void LoadBinary(string binaryAssetName, LoadBinaryCallbacks callbacks, object userData);

        #endregion
    }
}