using System.IO;
using UnityEngine;
using YooAsset;

namespace UnityGameFramework.Runtime
{
    public abstract class YooAssetDecryptionHelperBase : MonoBehaviour, IDecryptionServices
    {
        public abstract ulong LoadFromFileOffset(DecryptFileInfo fileInfo);
        public abstract byte[] LoadFromMemory(DecryptFileInfo fileInfo);
        public abstract Stream LoadFromStream(DecryptFileInfo fileInfo);
        public abstract uint GetManagedReadBufferSize();
    }
}