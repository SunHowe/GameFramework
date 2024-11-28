using System;
using System.Collections.Generic;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源管理器。
    /// </summary>
    internal partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        private sealed class LoadAssetPacksInfo : IReference
        {
            public Queue<LoadAssetPackInfo> PackInfos { get; } = new Queue<LoadAssetPackInfo>();
            
            public void Clear()
            {
                while (PackInfos.Count > 0)
                {
                    ReferencePool.Release(PackInfos.Dequeue());
                }
            }

            public static LoadAssetPacksInfo Create()
            {
                return ReferencePool.Acquire<LoadAssetPacksInfo>();
            }
        }
    }
}