using System;
using FairyGUI.Dynamic;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// 默认的FairyGUI资源加载器辅助工具。
    /// </summary>
    public sealed class DefaultFUIAssetLoaderHelper : FUIAssetLoaderHelperBase
    {
        private const string UIPackageAssetFormat = "Assets/GameMain/Res/UI/{0}_fui.bytes";
        private const string UITextureAssetFormat = "Assets/GameMain/Res/UI/{0}.{1}";
        private const string UIAudioAssetFormat = "Assets/GameMain/Res/UISound/{0}.{1}";
        
        private ResourceComponent m_ResourceComponent = null;
        private LoadBinaryCallbacks m_LoadBinaryCallbacks = null;
        private LoadAssetCallbacks m_LoadTextureAssetCallbacks = null;
        private LoadAssetCallbacks m_LoadAudioAssetCallbacks = null;
        
        public override void LoadUIPackageBytesAsync(string packageName, LoadUIPackageBytesCallback callback)
        {
            var assetName = Utility.Text.Format(UIPackageAssetFormat, packageName);
            m_ResourceComponent.LoadBinary(assetName, m_LoadBinaryCallbacks, callback);
        }

        public override void LoadUIPackageBytes(string packageName, out byte[] bytes, out string assetNamePrefix)
        {
            var assetName = Utility.Text.Format(UIPackageAssetFormat, packageName);
            bytes = m_ResourceComponent.LoadBinaryFromFileSystem(assetName);
            assetNamePrefix = string.Empty;
        }

        public override void LoadTextureAsync(string packageName, string assetName, string extension, LoadTextureCallback callback)
        {
            assetName = Utility.Text.Format(UITextureAssetFormat, assetName, extension);
            m_ResourceComponent.LoadAsset(assetName, typeof(Texture), m_LoadTextureAssetCallbacks, callback);
        }

        public override void UnloadTexture(Texture texture)
        {
            m_ResourceComponent.UnloadAsset(texture);
        }

        public override void LoadAudioClipAsync(string packageName, string assetName, string extension, LoadAudioClipCallback callback)
        {
            assetName = Utility.Text.Format(UIAudioAssetFormat, assetName, extension);
            m_ResourceComponent.LoadAsset(assetName, typeof(AudioClip), m_LoadAudioAssetCallbacks, callback);
        }

        public override void UnloadAudioClip(AudioClip audioClip)
        {
            m_ResourceComponent.UnloadAsset(audioClip);
        }

        private void Start()
        {
            m_ResourceComponent = GameEntry.GetComponent<ResourceComponent>();
            if (m_ResourceComponent == null)
            {
                Log.Fatal("Resource component is invalid.");
                return;
            }

            m_LoadBinaryCallbacks = new LoadBinaryCallbacks(OnLoadBinarySuccess, OnLoadBinaryFailure);
            m_LoadTextureAssetCallbacks = new LoadAssetCallbacks(OnLoadTextureAssetSuccess, OnLoadAssetTextureFailure);
            m_LoadAudioAssetCallbacks = new LoadAssetCallbacks(OnLoadAudioAssetSuccess, OnLoadAudioAssetFailure);
        }

        private void OnLoadBinarySuccess(string binaryAssetName, byte[] binaryBytes, float duration, object userdata)
        {
            var callback = (LoadUIPackageBytesCallback)userdata;
            callback(binaryBytes, string.Empty);
        }

        private void OnLoadBinaryFailure(string binaryAssetName, LoadResourceStatus status, string errorMessage, object userdata)
        {
            Log.Error(Utility.Text.Format("Load UIPackage binary asset '{0}' failure, status '{1}', error message '{2}'", binaryAssetName, status, errorMessage));
            var callback = (LoadUIPackageBytesCallback)userdata;
            callback(null, string.Empty);
        }

        private void OnLoadTextureAssetSuccess(string assetName, object asset, float duration, object userdata)
        {
            var callback = (LoadTextureCallback)userdata;
            callback((Texture)asset);
        }

        private void OnLoadAssetTextureFailure(string assetName, LoadResourceStatus status, string errorMessage, object userdata)
        {
            Log.Error(Utility.Text.Format("Load asset '{0}' failure, status '{1}', error message '{2}'", assetName, status, errorMessage));
            var callback = (LoadTextureCallback)userdata;
            callback(null);
        }

        private void OnLoadAudioAssetSuccess(string assetName, object asset, float duration, object userdata)
        {
            var callback = (LoadAudioClipCallback)userdata;
            callback((AudioClip)asset);
        }

        private void OnLoadAudioAssetFailure(string assetName, LoadResourceStatus status, string errorMessage, object userdata)
        {
            Log.Error(Utility.Text.Format("Load asset '{0}' failure, status '{1}', error message '{2}'", assetName, status, errorMessage));
            var callback = (LoadAudioClipCallback)userdata;
            callback(null);
        }        
    }
}