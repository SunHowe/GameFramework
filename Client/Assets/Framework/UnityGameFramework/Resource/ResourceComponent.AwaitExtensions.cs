using System;
using Cysharp.Threading.Tasks;
using GameFramework;
using GameFramework.Resource;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 资源组件 异步任务await拓展。
    /// </summary>
    public sealed partial class ResourceComponent
    {
        private LoadAssetCallbacks m_LoadAssetCallbacks;
        private LoadBinaryCallbacks m_LoadBinaryCallbacks;

        private void AwakeAwaitExtensions()
        {
            m_LoadAssetCallbacks = new LoadAssetCallbacks(OnLoadAssetSuccess, OnLoadAssetFailure);
            m_LoadBinaryCallbacks = new LoadBinaryCallbacks(OnLoadBinarySuccess, OnLoadBinaryFailure);
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public UniTask<object> LoadAssetAsync(string assetName)
        {
            var task = AutoResetUniTaskCompletionSource<object>.Create();
            LoadAsset(assetName, m_LoadAssetCallbacks, task);
            return task.Task;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public UniTask<object> LoadAssetAsync(string assetName, int priority)
        {
            var task = AutoResetUniTaskCompletionSource<object>.Create();
            LoadAsset(assetName, priority, m_LoadAssetCallbacks, task);
            return task.Task;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public UniTask<object> LoadAssetAsync(string assetName, Type assetType)
        {
            var task = AutoResetUniTaskCompletionSource<object>.Create();
            LoadAsset(assetName, assetType, m_LoadAssetCallbacks, task);
            return task.Task;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public UniTask<object> LoadAssetAsync(string assetName, Type assetType, int priority)
        {
            var task = AutoResetUniTaskCompletionSource<object>.Create();
            LoadAsset(assetName, assetType, priority, m_LoadAssetCallbacks, task);
            return task.Task;
        }

        /// <summary>
        /// 异步加载二进制文件
        /// </summary>
        public UniTask<byte[]> LoadBinaryAsync(string assetName)
        {
            var task = AutoResetUniTaskCompletionSource<byte[]>.Create();
            LoadBinary(assetName, m_LoadBinaryCallbacks, task);
            return task.Task;
        }

        /// <summary>
        /// 加载成功回调
        /// </summary>
        private void OnLoadAssetSuccess(string assetName, object asset, float duration, object userdata)
        {
            var task = (AutoResetUniTaskCompletionSource<object>)userdata;
            task.TrySetResult(asset);
        }

        /// <summary>
        /// 加载失败回调
        /// </summary>
        private void OnLoadAssetFailure(string assetName, LoadResourceStatus status, string errorMessage, object userdata)
        {
            var task = (AutoResetUniTaskCompletionSource<object>)userdata;
            task.TrySetException(new GameFrameworkException($"Load asset '{assetName}' failure, status '{status}', error message '{errorMessage}'."));
        }

        /// <summary>
        /// 加载二进制文件成功回调
        /// </summary>
        private void OnLoadBinarySuccess(string binaryAssetName, byte[] binaryBytes, float duration, object userdata)
        {
            var task = (AutoResetUniTaskCompletionSource<byte[]>)userdata;
            task.TrySetResult(binaryBytes);
        }

        /// <summary>
        /// 加载二进制文件失败回调
        /// </summary>
        private void OnLoadBinaryFailure(string binaryAssetName, LoadResourceStatus status, string errorMessage, object userdata)
        {
            var task = (AutoResetUniTaskCompletionSource<byte[]>)userdata;
            task.TrySetException(new GameFrameworkException($"Load binary '{binaryAssetName}' failure, status '{status}', error message '{errorMessage}'."));
        }
    }
}