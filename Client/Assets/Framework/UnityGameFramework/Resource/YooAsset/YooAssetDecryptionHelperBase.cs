using System.IO;
using UnityEngine;
using YooAsset;

namespace UnityGameFramework.Runtime
{
    public abstract class YooAssetDecryptionHelperBase : MonoBehaviour, IDecryptionServices
    {
        public abstract AssetBundle LoadAssetBundle(DecryptFileInfo fileInfo, out Stream managedStream);

        public abstract AssetBundleCreateRequest LoadAssetBundleAsync(DecryptFileInfo fileInfo, out Stream managedStream);
    }
}