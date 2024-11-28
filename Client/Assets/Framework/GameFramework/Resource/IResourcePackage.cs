using System;
using System.Collections.Generic;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源包接口。
    /// </summary>
    public interface IResourcePackage
    {
        /// <summary>
        /// 资源包名字。
        /// </summary>
        string PackageName { get; }

        /// <summary>
        /// 资源包采用的资源模式。
        /// </summary>
        ResourceMode ResourceMode { get; }

        /// <summary>
        /// 获取资源包下所有资源的资源标识。
        /// </summary>
        /// <returns>资源包下所有资源的资源标识。</returns>
        string[] GetAssetNames();

        /// <summary>
        /// 获取资源包下所有资源的资源标识。
        /// </summary>
        /// <param name="results">将资源标识写到该list中。</param>
        void GetAssetNames(List<string> results);

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