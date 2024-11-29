using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Resource;
using UnityEngine.SceneManagement;
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
        private ResourcePackageHelperBase m_Helper;

        private readonly Dictionary<object, AssetOperationHandle> m_AssetHandleDict = new Dictionary<object, AssetOperationHandle>();
        private readonly Dictionary<string, SceneOperationHandle> m_SceneHandleDict = new Dictionary<string, SceneOperationHandle>();

        public YooAssetResourcePackage(string packageName, ResourceMode resourceMode, ResourcePackage resourcePackage, ResourcePackageHelperBase helper)
        {
            PackageName = packageName;
            ResourceMode = resourceMode;
            ResourcePackage = resourcePackage;
            m_Helper = helper;
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
            if (!ResourcePackage.CheckLocationValid(assetName))
            {
                return HasAssetResult.NotExist;
            }

            var assetInfo = ResourcePackage.GetAssetInfo(assetName);
            if (ResourcePackage.IsNeedDownloadFromRemote(assetInfo))
            {
                return HasAssetResult.NotReady;
            }

            return HasAssetResult.ReadyAsset; // TODO check ReadyBinary
        }

        public void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks callbacks, object userData)
        {
            if (!ResourcePackage.CheckLocationValid(assetName))
            {
                if (callbacks.LoadAssetFailureCallback != null)
                {
                    callbacks.LoadAssetFailureCallback(assetName, LoadResourceStatus.NotExist, "Asset location is invalid.", userData);
                    return;
                }

                throw new Exception("Asset location is invalid.");
            }

            m_Helper.StartCoroutine(LoadAssetAsync(assetName, assetType, priority, callbacks, userData));
        }

        public void UnloadAsset(object asset)
        {
            if (!m_AssetHandleDict.TryGetValue(asset, out var handle))
            {
                throw new Exception("Asset handle is invalid");
            }

            m_AssetHandleDict.Remove(asset);
            handle.Release();
        }

        public void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks callbacks, object userData)
        {
            if (!ResourcePackage.CheckLocationValid(sceneAssetName))
            {
                if (callbacks.LoadSceneFailureCallback != null)
                {
                    callbacks.LoadSceneFailureCallback(sceneAssetName, LoadResourceStatus.NotExist, "Asset location is invalid.", userData);
                    return;
                }

                throw new Exception("Asset location is invalid.");
            }

            m_Helper.StartCoroutine(LoadSceneAsync(sceneAssetName, priority, callbacks, userData));
        }

        public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks callbacks, object userData)
        {
            if (!m_SceneHandleDict.TryGetValue(sceneAssetName, out var handle))
            {
                if (callbacks.UnloadSceneFailureCallback != null)
                {
                    callbacks.UnloadSceneFailureCallback(sceneAssetName, userData);
                    return;
                }

                throw new Exception("Scene handle is invalid");
            }

            m_Helper.StartCoroutine(UnloadSceneAsync(sceneAssetName, handle, callbacks, userData));
        }

        public byte[] LoadBinary(string binaryAssetName, out LoadResourceStatus status)
        {
            if (!ResourcePackage.CheckLocationValid(binaryAssetName))
            {
                status = LoadResourceStatus.NotExist;
                return Array.Empty<byte>();
            }
            
            var handle = ResourcePackage.LoadRawFileSync(binaryAssetName);
            if (handle.Status != EOperationStatus.Succeed)
            {
                status = LoadResourceStatus.AssetError;
                return Array.Empty<byte>();
            }
            
            var bytes = handle.GetRawFileData();
            handle.Release();
            status = LoadResourceStatus.Success;
            
            return bytes;
        }

        public void LoadBinary(string binaryAssetName, LoadBinaryCallbacks callbacks, object userData)
        {
            if (!ResourcePackage.CheckLocationValid(binaryAssetName))
            {
                if (callbacks.LoadBinaryFailureCallback != null)
                {
                    callbacks.LoadBinaryFailureCallback(binaryAssetName, LoadResourceStatus.NotExist, "Asset location is invalid.", userData);
                    return;
                }
                
                throw new Exception("Asset location is invalid.");
            }
            
            m_Helper.StartCoroutine(LoadBinaryAsync(binaryAssetName, callbacks, userData));
        }

        private IEnumerator LoadAssetAsync(string assetName, Type assetType, int priority, LoadAssetCallbacks callbacks, object userData)
        {
            var time = DateTime.UtcNow;
            var handle = ResourcePackage.LoadAssetAsync(assetName, assetType);
            yield return handle;

            if (handle.Status != EOperationStatus.Succeed)
            {
                handle.Release();
                if (callbacks.LoadAssetFailureCallback != null)
                {
                    callbacks.LoadAssetFailureCallback(assetName, LoadResourceStatus.AssetError, handle.LastError, userData);
                    yield break;
                }

                Log.Fatal("Load asset failure: {0}, error: {1}", assetName, handle.LastError);
                yield break;
            }

            var asset = handle.AssetObject;
            m_AssetHandleDict[asset] = handle;
            callbacks.LoadAssetSuccessCallback(assetName, asset, (float)(DateTime.UtcNow - time).TotalSeconds, userData);
        }

        private IEnumerator LoadSceneAsync(string sceneAssetName, int priority, LoadSceneCallbacks callbacks, object userData)
        {
            var time = DateTime.UtcNow;
            var handle = ResourcePackage.LoadSceneAsync(sceneAssetName, LoadSceneMode.Additive, false);
            yield return handle;

            if (handle.Status != EOperationStatus.Succeed)
            {
                if (callbacks.LoadSceneFailureCallback != null)
                {
                    callbacks.LoadSceneFailureCallback(sceneAssetName, LoadResourceStatus.AssetError, handle.LastError, userData);
                    yield break;
                }

                Log.Fatal("Load scene failure: {0}, error: {1}", sceneAssetName, handle.LastError);
                yield break;
            }

            m_SceneHandleDict[sceneAssetName] = handle;
            callbacks.LoadSceneSuccessCallback(sceneAssetName, (float)(DateTime.UtcNow - time).TotalSeconds, userData);
        }

        private IEnumerator UnloadSceneAsync(string sceneAssetName, SceneOperationHandle handle, UnloadSceneCallbacks callbacks, object userData)
        {
            var operation = handle.UnloadAsync();
            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                if (callbacks.UnloadSceneFailureCallback != null)
                {
                    callbacks.UnloadSceneFailureCallback(sceneAssetName, userData);
                    yield break;
                }
                
                Log.Fatal("Unload scene failure: {0}, error: {1}", sceneAssetName, operation.Error);
                yield break;
            }
            
            m_SceneHandleDict.Remove(sceneAssetName);
            callbacks.UnloadSceneSuccessCallback(sceneAssetName, userData);
        }

        private IEnumerator LoadBinaryAsync(string binaryAssetName, LoadBinaryCallbacks callbacks, object userData)
        {
            var time = DateTime.UtcNow;
            var handle = ResourcePackage.LoadRawFileAsync(binaryAssetName);
            
            yield return handle;

            if (handle.Status != EOperationStatus.Succeed)
            {
                handle.Release();
                if (callbacks.LoadBinaryFailureCallback != null)
                {
                    callbacks.LoadBinaryFailureCallback(binaryAssetName, LoadResourceStatus.AssetError, handle.LastError, userData);
                    yield break;
                }
                
                Log.Fatal("Load binary failure: {0}, error: {1}", binaryAssetName, handle.LastError);
                yield break;
            }
            
            var bytes = handle.GetRawFileData();
            handle.Release();
            callbacks.LoadBinarySuccessCallback(binaryAssetName, bytes, (float)(DateTime.UtcNow - time).TotalSeconds, userData);
        }
    }
}