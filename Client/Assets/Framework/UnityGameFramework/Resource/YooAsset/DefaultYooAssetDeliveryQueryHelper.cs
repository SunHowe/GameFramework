using YooAsset;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 默认的YooAsset分发资源查询辅助工具。
    /// </summary>
    public sealed class DefaultYooAssetDeliveryQueryHelper : YooAssetDeliveryQueryHelperBase
    {
        public override bool QueryDeliveryFiles(string packageName, string fileName)
        {
            return false;
        }

        public override DeliveryFileInfo GetDeliveryFileInfo(string packageName, string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}