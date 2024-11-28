using UnityEditor;
using UnityEngine;
using YooAsset.Editor;

namespace UnityGameFramework.Editor.YooAsset
{
    [DisplayName("收集所有纹理")]
    public class CollectTexture : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var mainAssetType = AssetDatabase.GetMainAssetTypeAtPath(data.AssetPath);
            if (mainAssetType == typeof(Texture2D))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}