using System;
using System.Collections.Generic;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源管理器。
    /// </summary>
    internal partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        private sealed class LoadAssetPackInfo : IReference
        {
            public LoadAssetCallbacks Callbacks { get; private set; }
            public object UserData { get; private set; }
            
            public void Clear()
            {
                Callbacks = null;
                UserData = null;
            }

            public static LoadAssetPackInfo Create(LoadAssetCallbacks loadAssetCallbacks, object userData)
            {
                if (loadAssetCallbacks == null)
                {
                    throw new GameFrameworkException("Load asset callbacks is invalid.");
                }
                
                LoadAssetPackInfo loadAssetPackInfo = ReferencePool.Acquire<LoadAssetPackInfo>();
                loadAssetPackInfo.Callbacks = loadAssetCallbacks;
                loadAssetPackInfo.UserData = userData;
                
                return loadAssetPackInfo;
            }
        }
    }
}