using System;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 资源引用。
    /// </summary>
    [Serializable]
    public class AssetRef
    {
        [SerializeField]
        private string m_AssetPath;

        [SerializeField]
        private string m_Guid;

        /// <summary>
        /// 资源路径
        /// </summary>
        public string AssetPath => m_AssetPath;

        /// <summary>
        /// 资源类型
        /// </summary>
        public virtual Type ResourceType { get; } = typeof(UnityEngine.Object);

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="component">资源组件引用。</param>
        /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
        public void LoadAsset(ResourceComponent component, LoadAssetCallbacks loadAssetCallbacks)
        {
            if (string.IsNullOrEmpty(m_AssetPath))
            {
                loadAssetCallbacks.LoadAssetFailureCallback?.Invoke(string.Empty, LoadResourceStatus.NotExist, "AssetPath is invalid.", this);
                return;
            }

            component.LoadAsset(AssetPath, ResourceType, loadAssetCallbacks);
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="component">资源组件引用。</param>
        /// <param name="priority">加载资源的优先级。</param>
        /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
        public void LoadAsset(ResourceComponent component, int priority, LoadAssetCallbacks loadAssetCallbacks)
        {
            if (string.IsNullOrEmpty(m_AssetPath))
            {
                loadAssetCallbacks.LoadAssetFailureCallback?.Invoke(string.Empty, LoadResourceStatus.NotExist, "AssetPath is invalid.", this);
                return;
            }

            component.LoadAsset(AssetPath, ResourceType, priority, loadAssetCallbacks);
        }
    }

    /// <summary>
    /// 泛型资源引用
    /// </summary>
    [Serializable]
    public abstract class AssetRef<T> : AssetRef where T : UnityEngine.Object
    {
        public override Type ResourceType => typeof(T);
    }
}