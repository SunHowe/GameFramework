using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 资源功能模块。提供局部的资源加载生命周期管理，当模块被销毁时，会自动归还由自己申请加载的资源。
    /// </summary>
    public sealed class ResourceFeature : Feature
    {
        /// <summary>
        /// 资源组件。
        /// </summary>
        private ResourceComponent m_ResourceComponent;
        
        /// <summary>
        /// 已加载的资源。
        /// </summary>
        private readonly List<Object> m_LoadedAssets = new List<Object>();

        /// <summary>
        /// 若有指定Parent，则加载时从Parent中加载资源，加载完成后归还给Parent。
        /// </summary>
        private ResourceFeature m_Parent;

        /// <summary>
        /// 取消令牌管理。
        /// </summary>
        private CancellationTokenSource m_CancellationTokenSource;

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        public async UniTask<T> LoadAssetAsync<T>(string assetName) where T : Object
        {
            var token = (m_CancellationTokenSource ??= new CancellationTokenSource()).Token;
            var asset = await (m_Parent?.LoadAssetAsync<T>(assetName) ?? m_ResourceComponent.LoadAssetAsync<T>(assetName, token));
            if (asset == null)
            {
                // 加载失败。
                return null;
            }
            
            m_LoadedAssets.Add(asset);
            return asset;
        }

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        public async UniTask<T> LoadAssetAsync<T>(string assetName, int priority) where T : Object
        {
            var token = (m_CancellationTokenSource ??= new CancellationTokenSource()).Token;
            var asset = await (m_Parent?.LoadAssetAsync<T>(assetName, priority) ?? m_ResourceComponent.LoadAssetAsync<T>(assetName, priority, token));
            if (asset == null)
            {
                // 加载失败。
                return null;
            }
            
            m_LoadedAssets.Add(asset);
            return asset;
        }

        /// <summary>
        /// 异步加载二进制文件
        /// </summary>
        public UniTask<byte[]> LoadBinaryAsync(string assetName)
        {
            // 这种统一从资源组件加载即可。
            return m_ResourceComponent.LoadBinaryAsync(assetName);
        }

        /// <summary>
        /// 设置父级资源模块。
        /// </summary>
        public void SetParent(ResourceFeature parent)
        {
            if (m_CancellationTokenSource != null)
            {
                throw new System.Exception("Can't set parent when there are loaded assets.");
            }

            m_Parent = parent;
        }

        public override void Awake(object featureOwner, FeatureContainer featureContainer)
        {
            base.Awake(featureOwner, featureContainer);
            m_ResourceComponent = ResourceComponent.Instance;
        }

        public override void Shutdown()
        {
            if (m_CancellationTokenSource != null)
            {
                m_CancellationTokenSource.Cancel();
                m_CancellationTokenSource.Dispose();
                m_CancellationTokenSource = null;
            }

            if (m_Parent != null)
            {
                // 目前ResourceFeature暂不提供归还接口，有父级模块的情况下，由父级模块进行卸载接口。
                m_Parent = null;
            }
            else if (m_ResourceComponent == ResourceComponent.Instance)
            {
                // 向资源组件归还资源。
                foreach (var asset in m_LoadedAssets)
                {
                    m_ResourceComponent.UnloadAsset(asset);
                }
            }
            
            m_LoadedAssets.Clear();
            m_ResourceComponent = null;
        }
    }
}