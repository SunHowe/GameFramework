using System;
using System.Collections.Generic;
using GameFramework.ObjectPool;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源管理器。
    /// </summary>
    internal partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        private sealed class AssetObject : ObjectBase
        {
            private IResourcePackage m_ResourcePackage;

            public static AssetObject Create(string name, IResourcePackage resourcePackage, object target)
            {
                if (target == null)
                {
                    throw new GameFrameworkException("Target is invalid.");
                }

                if (resourcePackage == null)
                {
                    throw new GameFrameworkException("ResourcePackage is invalid.");
                }
                
                AssetObject assetObject = ReferencePool.Acquire<AssetObject>();
                assetObject.Initialize(name, target);
                assetObject.m_ResourcePackage = resourcePackage;
                
                return assetObject;
            }

            public override void Clear()
            {
                base.Clear();
                m_ResourcePackage = null;
            }

            protected internal override void Release(bool isShutdown)
            {
                m_ResourcePackage.UnloadAsset(Target);
            }
        }
    }
}