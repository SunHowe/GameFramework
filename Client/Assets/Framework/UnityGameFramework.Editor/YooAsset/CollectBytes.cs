using System.IO;
using YooAsset.Editor;

namespace UnityGameFramework.Editor.YooAsset
{
    [DisplayName("收集bytes")]
    public class CollectBytes : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            return Path.GetExtension(data.AssetPath) == ".bytes";
        }
    }
}