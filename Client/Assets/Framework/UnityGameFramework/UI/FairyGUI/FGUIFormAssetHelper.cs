using System;
using FairyGUI;
using FairyGUI.Dynamic;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FairyGUI界面资源辅助器。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    public sealed class FGUIFormAssetHelper : MonoBehaviour, IUIFormAssetHelper
    {
        private UIAssetManager m_UIAssetManager = null;

        /// <summary>
        /// 初始化辅助器。
        /// </summary>
        /// <param name="uiPackageHelper">UIPackage辅助工具。</param>
        /// <param name="uiAssetLoader">FairyGUI资源加载器。</param>
        /// <param name="unloadUnusedUIPackageImmediately">是否立即卸载未使用的UIPackage。</param>
        public void InitHelper(FGUIPackageHelperBase uiPackageHelper, FGUIAssetLoaderHelperBase uiAssetLoader, bool unloadUnusedUIPackageImmediately)
        {
            m_UIAssetManager = new UIAssetManager();
            m_UIAssetManager.Initialize(new UIAssetManagerConfiguration(uiPackageHelper, uiAssetLoader, unloadUnusedUIPackageImmediately));
        }

        private void OnDestroy()
        {
            if (m_UIAssetManager == null)
            {
                return;
            }

            m_UIAssetManager.Dispose();
            m_UIAssetManager = null;
        }

        /// <summary>
        /// 异步加载界面资源。
        /// </summary>
        /// <param name="uiFormAssetName">要加载界面资源的名称。</param>
        /// <param name="priority">加载资源的优先级。</param>
        /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void LoadUIFormAsset(string uiFormAssetName, int priority, LoadAssetCallbacks loadAssetCallbacks, object userData)
        {
            var startTime = DateTime.UtcNow;
            
            UIPackage.CreateObjectFromURLAsync(uiFormAssetName, gObject =>
            {
                if (gObject == null)
                {
                    loadAssetCallbacks.LoadAssetFailureCallback?.Invoke(uiFormAssetName, LoadResourceStatus.NotExist, $"FairyGUI asset not found: {uiFormAssetName}", userData);
                    return;
                }
                
                var gComponent = gObject as GComponent;
                if (gComponent == null)
                {
                    gObject.Dispose(); // 销毁该组件
                    loadAssetCallbacks.LoadAssetFailureCallback?.Invoke(uiFormAssetName, LoadResourceStatus.NotExist, $"FairyGUI asset is not a GComponent: {uiFormAssetName}", userData);
                    return;
                }

                // 加载成功
                loadAssetCallbacks.LoadAssetSuccessCallback?.Invoke(uiFormAssetName, gComponent, (float)(DateTime.UtcNow - startTime).TotalSeconds, userData);
            });
        }

        private sealed class UIAssetManagerConfiguration : IUIAssetManagerConfiguration
        {
            public IUIPackageHelper PackageHelper { get; }
            public IUIAssetLoader AssetLoader { get; }
            public bool UnloadUnusedUIPackageImmediately { get; }

            public UIAssetManagerConfiguration(IUIPackageHelper packageHelper, IUIAssetLoader assetLoader, bool unloadUnusedUIPackageImmediately)
            {
                PackageHelper = packageHelper;
                AssetLoader = assetLoader;
                UnloadUnusedUIPackageImmediately = unloadUnusedUIPackageImmediately;
            }
        }
    }
}