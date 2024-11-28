using System;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源管理器。
    /// </summary>
    internal partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        public byte[] LoadBinary(string binaryAssetName)
        {
            return LoadBinary(binaryAssetName, out _);
        }

        public byte[] LoadBinary(string binaryAssetName, out LoadResourceStatus status)
        {
            var package = FindAssetPackage(binaryAssetName);
            if (package == null)
            {
                status = LoadResourceStatus.NotExist;
                return Array.Empty<byte>();
            }

            return package.LoadBinary(binaryAssetName, out status);
        }

        public void LoadBinary(string binaryAssetName, LoadBinaryCallbacks callbacks)
        {
            LoadBinary(binaryAssetName, callbacks, null);
        }

        public void LoadBinary(string binaryAssetName, LoadBinaryCallbacks callbacks, object userData)
        {
            var package = FindAssetPackage(binaryAssetName);
            if (package == null)
            {
                if (callbacks.LoadBinaryFailureCallback != null)
                {
                    callbacks.LoadBinaryFailureCallback(binaryAssetName, LoadResourceStatus.NotExist, "Not Exist Asset", userData);
                    return;
                }
                
                throw new GameFrameworkException(Utility.Text.Format("Not Exists Binary Asset:{0}", binaryAssetName));
            }
            
            package.LoadBinary(binaryAssetName, callbacks, userData);
        }
    }
}