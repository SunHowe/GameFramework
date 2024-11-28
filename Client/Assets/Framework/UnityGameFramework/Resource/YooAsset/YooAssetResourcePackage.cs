using System;
using System.Collections.Generic;
using GameFramework.Resource;
using YooAsset;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// YooAsset资源包。
    /// </summary>
    public class YooAssetResourcePackage : IResourcePackage
    {
        public string PackageName { get; }
        public ResourceMode ResourceMode { get; }
        public string ResourceVersion { get; }
        public ResourcePackage ResourcePackage { get; }

        public YooAssetResourcePackage(string packageName, ResourceMode resourceMode, ResourcePackage resourcePackage)
        {
            PackageName = packageName;
            ResourceMode = resourceMode;
            ResourcePackage = resourcePackage;
        }

        public string[] GetAssetNames()
        {
            throw new NotImplementedException();
        }

        public void GetAssetNames(List<string> results)
        {
            throw new NotImplementedException();
        }

        public HasAssetResult HasAsset(string assetName)
        {
            throw new NotImplementedException();
        }

        public void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks callbacks, object userData)
        {
            throw new NotImplementedException();
        }

        public void UnloadAsset(object asset)
        {
            throw new NotImplementedException();
        }

        public void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks callbacks, object userData)
        {
            throw new NotImplementedException();
        }

        public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks callbacks, object userData)
        {
            throw new NotImplementedException();
        }

        public byte[] LoadBinary(string binaryAssetName, out LoadResourceStatus status)
        {
            throw new NotImplementedException();
        }

        public void LoadBinary(string binaryAssetName, LoadBinaryCallbacks callbacks, object userData)
        {
            throw new NotImplementedException();
        }
    }
}