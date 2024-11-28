using System;
using System.Collections.Generic;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源管理器。
    /// </summary>
    internal partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        private sealed class CreatePackagePackInfo : IReference
        {
            public string PackageName { get; private set; }
            public ResourceMode ResourceMode { get; private set; }
            public CreatePackageCallbacks Callbacks { get; private set; }
            public object UserData { get; private set; }
            
            public void Clear()
            {
                PackageName = null;
                ResourceMode = default;
                Callbacks = null;
                UserData = null;
            }

            public static CreatePackagePackInfo Create(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks, object userData)
            {
                var packInfo = ReferencePool.Acquire<CreatePackagePackInfo>();
                packInfo.PackageName = packageName;
                packInfo.ResourceMode = resourceMode;
                packInfo.Callbacks = callbacks;
                packInfo.UserData = userData;
                return packInfo;
            }
        }
    }
}