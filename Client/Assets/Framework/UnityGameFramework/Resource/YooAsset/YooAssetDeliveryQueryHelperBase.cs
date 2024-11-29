using UnityEngine;
using YooAsset;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// YooAsset分发资源查询辅助工具基类。
    /// </summary>
    public abstract class YooAssetDeliveryQueryHelperBase : MonoBehaviour, IDeliveryQueryServices
    {
        public abstract bool QueryDeliveryFiles(string packageName, string fileName);
        public abstract DeliveryFileInfo GetDeliveryFileInfo(string packageName, string fileName);
    }
}