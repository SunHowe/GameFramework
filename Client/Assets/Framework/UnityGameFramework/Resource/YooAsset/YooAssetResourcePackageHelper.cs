using System;
using System.Collections;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;
using YooAsset;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// YooAsset资源包辅助器。
    /// </summary>
    public sealed partial class YooAssetResourcePackageHelper : ResourcePackageHelperBase
    {
        /// <summary>
        /// CDN文件服务器地址。支持参数{0}=平台名，{1}=应用版本号。
        /// </summary>
        [SerializeField]
        private string m_CDNHostServer;
        
        /// <summary>
        /// 备选的CDN文件服务器地址。支持参数{0}=平台名，{1}=应用版本号。
        /// </summary>
        [SerializeField]
        private string m_FallbackCDNHostServer;

        /// <summary>
        /// 解密辅助工具。
        /// </summary>
        [SerializeField]
        private YooAssetDecryptionHelperBase m_DecryptionHelper;

        private IRemoteServices m_RemoteServices;
        
        public override void CreateDefaultResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks)
        {
            CreateResourcePackage(packageName, resourceMode, true, callbacks);
        }

        public override void CreateResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks)
        {
            CreateResourcePackage(packageName, resourceMode, false, callbacks);
        }

        public override void DestroyResourcePackage(IResourcePackage resourcePackage)
        {
            YooAssets.DestroyPackage(resourcePackage.PackageName);
        }

        private void Awake()
        {
            m_RemoteServices = new RemoteServices(FormatCDNUrl(m_CDNHostServer), FormatCDNUrl(m_FallbackCDNHostServer));
            
            YooAssets.Initialize();
        }

        private void OnDestroy()
        {
            YooAssets.Destroy();
        }

        private void CreateResourcePackage(string packageName, ResourceMode resourceMode, bool isSetDefault, CreatePackageCallbacks callbacks)
        {
            if (resourceMode == ResourceMode.Unspecified)
            {
                if (callbacks.CreatePackageFailureCallback != null)
                {
                    callbacks.CreatePackageFailureCallback(packageName, resourceMode, CreatePackageStatus.NotSupportResourceMode, null);
                    return;
                }

                throw new GameFrameworkException("Resource mode is invalid.");
            }

            try
            {
                var resourcePackage = new YooAssetResourcePackage(packageName, resourceMode, YooAssets.CreatePackage(packageName), this);
                if (isSetDefault)
                {
                    // 设置为默认资源包
                    YooAssets.SetDefaultPackage(resourcePackage.ResourcePackage);
                }

                switch (resourceMode)
                {
                    case ResourceMode.Package:
                        StartCoroutine(InitializeAsPackageMode(resourcePackage, callbacks));
                        break;
                    case ResourceMode.Updatable:
                    case ResourceMode.UpdatableWhilePlaying:
                        StartCoroutine(InitializeAsUpdatableMode(resourcePackage, callbacks));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(resourceMode), resourceMode, null);
                }
            }
            catch (Exception)
            {
                if (callbacks.CreatePackageFailureCallback != null)
                {
                    callbacks.CreatePackageFailureCallback(packageName, resourceMode, CreatePackageStatus.Exception, null);
                    return;
                }

                throw;
            }
        }

        /// <summary>
        /// 单机模式初始化资源包。
        /// </summary>
        private IEnumerator InitializeAsPackageMode(YooAssetResourcePackage resourcePackage, CreatePackageCallbacks callbacks)
        {
            var initParameters = new OfflinePlayModeParameters();
            var initOperation = resourcePackage.ResourcePackage.InitializeAsync(initParameters);
            yield return initOperation;

            if (initOperation.Status == EOperationStatus.Succeed)
            {
                callbacks.CreatePackageSuccessCallback(resourcePackage.PackageName, resourcePackage.ResourceMode, resourcePackage, null);
            }
            else if (callbacks.CreatePackageFailureCallback != null)
            {
                callbacks.CreatePackageFailureCallback(resourcePackage.PackageName, resourcePackage.ResourceMode, CreatePackageStatus.Exception, null);
            }
        }

        /// <summary>
        /// 联网模式初始化资源包。
        /// </summary>
        private IEnumerator InitializeAsUpdatableMode(YooAssetResourcePackage resourcePackage, CreatePackageCallbacks callbacks)
        {
            var initParameters = new HostPlayModeParameters
            {
                BuildinQueryServices = new GameQueryServices(),
                DecryptionServices = m_DecryptionHelper,
                RemoteServices = m_RemoteServices,
            };
            var initOperation = resourcePackage.ResourcePackage.InitializeAsync(initParameters);
            yield return initOperation;

            if (initOperation.Status == EOperationStatus.Succeed)
            {
                callbacks.CreatePackageSuccessCallback(resourcePackage.PackageName, resourcePackage.ResourceMode, resourcePackage, null);
            }
            else if (callbacks.CreatePackageFailureCallback != null)
            {
                callbacks.CreatePackageFailureCallback(resourcePackage.PackageName, resourcePackage.ResourceMode, CreatePackageStatus.Exception, null);
            }
        }

        /// <summary>
        /// 格式化CDN地址。
        /// </summary>
        private string FormatCDNUrl(string url)
        {
            var platform = Application.platform;
            var platformName = platform.ToString();

            switch (platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    platformName = "Windows";
                    break;
            }

            return Utility.Text.Format(url, platformName, Application.version);
        }
    }
}