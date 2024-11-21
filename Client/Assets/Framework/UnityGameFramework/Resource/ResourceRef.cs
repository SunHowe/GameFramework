using System;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 资源引用。
    /// </summary>
    [Serializable]
    public class ResourceRef
    {
        [SerializeField]
        private string m_ResourcePath;

        [SerializeField]
        private string m_Guid;

#if UNITY_EDITOR
        public void FixResourcePath()
        {
            var assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(m_Guid);
            if (string.IsNullOrEmpty(assetPath))
            {
                UnityEngine.Debug.LogError($"Resource path is invalid. Previous resource path is {m_ResourcePath}, guid is {m_Guid}.");
                return;
            }
            
            m_ResourcePath = assetPath;
        }
#endif

        /// <summary>
        /// 资源路径
        /// </summary>
        public string ResourcePath => m_ResourcePath;
        
        /// <summary>
        /// 资源类型
        /// </summary>
        public virtual Type ResourceType { get; } = typeof(UnityEngine.Object);
    }

    /// <summary>
    /// 泛型资源引用
    /// </summary>
    [Serializable]
    public abstract class ResourceRef<T> : ResourceRef where T : UnityEngine.Object
    {
        public override Type ResourceType => typeof(T);
    }
}