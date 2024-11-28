using System;
using System.Collections;
using GameFramework.Resource;
using YooAsset;

namespace UnityGameFramework.Runtime
{
    public class YooAssetEditorResourcePackageHelper : ResourcePackageHelperBase
    {
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
            YooAssets.Initialize();
        }

        private void OnDestroy()
        {
            YooAssets.Destroy();
        }

        private void CreateResourcePackage(string packageName, ResourceMode resourceMode, bool isSetDefault, CreatePackageCallbacks callbacks)
        {
            try
            {
                var resourcePackage = new YooAssetResourcePackage(packageName, resourceMode, YooAssets.CreatePackage(packageName));
                if (isSetDefault)
                {
                    // 设置为默认资源包
                    YooAssets.SetDefaultPackage(resourcePackage.ResourcePackage);
                }

                StartCoroutine(InitializeAsEditorMode(resourcePackage, callbacks));
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
        /// Editor模式初始化资源包。
        /// </summary>
        private IEnumerator InitializeAsEditorMode(YooAssetResourcePackage resourcePackage, CreatePackageCallbacks callbacks)
        {
            var initParameters = new EditorSimulateModeParameters();
            var simulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.BuiltinBuildPipeline, resourcePackage.PackageName);
            initParameters.SimulateManifestFilePath = simulateManifestFilePath;
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
    }
}