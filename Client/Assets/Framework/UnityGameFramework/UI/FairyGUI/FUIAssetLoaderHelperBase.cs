using FairyGUI.Dynamic;
using UnityEngine;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FairyGUI资源加载器基类。
    /// </summary>
    public abstract class FUIAssetLoaderHelperBase : MonoBehaviour, IUIAssetLoader
    {
        /// <summary>
        /// 异步加载UIPackage二进制数据
        /// </summary>
        public abstract void LoadUIPackageBytesAsync(string packageName, LoadUIPackageBytesCallback callback);
        
        /// <summary>
        /// 同步加载UIPackage二进制数据 通过bytes返回数据 assetNamePrefix返回资源前缀
        /// </summary>
        public abstract void LoadUIPackageBytes(string packageName, out byte[] bytes, out string assetNamePrefix);

        /// <summary>
        /// 异步加载Texture资源
        /// </summary>
        public abstract void LoadTextureAsync(string packageName, string assetName, string extension, LoadTextureCallback callback);

        /// <summary>
        /// 卸载Texture资源
        /// </summary>
        public abstract void UnloadTexture(Texture texture);

        /// <summary>
        /// 异步加载AudioClip资源
        /// </summary>
        public abstract void LoadAudioClipAsync(string packageName, string assetName, string extension, LoadAudioClipCallback callback);

        /// <summary>
        /// 卸载AudioCip资源
        /// </summary>
        public abstract void UnloadAudioClip(AudioClip audioClip);
    }
}