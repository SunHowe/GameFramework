using UnityEditor;
using UnityEngine;

namespace UnityGameFramework.Editor
{
    /// <summary>
    /// AssetDatabase 工具类
    /// </summary>
    public static class AssetDatabaseUtility
    {
        /// <summary>
        /// 通过GUID加载资源并返回对应资源路径
        /// </summary>
        public static (string assetPath, Object asset) LoadAssetWithGUID(string guid, System.Type type)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return (string.Empty, null);
            }
            
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(assetPath))
            {
                return (string.Empty, null);
            }
            
            var asset = AssetDatabase.LoadAssetAtPath(assetPath, type);
            return (assetPath, asset);
        }
    }
}