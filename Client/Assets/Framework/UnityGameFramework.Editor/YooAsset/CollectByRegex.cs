using System.Text.RegularExpressions;
using YooAsset.Editor;

namespace UnityGameFramework.Editor.YooAsset
{
    [DisplayName("通过正则表达式收集")]
    public class CollectByRegex : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            if (string.IsNullOrEmpty(data.UserData))
                return true;
            
            return Regex.IsMatch(data.AssetPath, data.UserData);
        }
    }
}