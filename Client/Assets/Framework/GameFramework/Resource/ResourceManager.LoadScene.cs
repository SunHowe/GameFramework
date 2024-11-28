using System;
using System.Collections.Generic;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源管理器。
    /// </summary>
    internal partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        public void LoadScene(string sceneAssetName, LoadSceneCallbacks callbacks)
        {
            LoadScene(sceneAssetName, Constant.DefaultPriority, callbacks, null);
        }

        public void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks callbacks)
        {
            LoadScene(sceneAssetName, priority, callbacks, null);
        }

        public void LoadScene(string sceneAssetName, LoadSceneCallbacks callbacks, object userData)
        {
            LoadScene(sceneAssetName, Constant.DefaultPriority, callbacks, userData);
        }

        public void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks callbacks, object userData)
        {
            var package = FindAssetPackage(sceneAssetName);
            if (package == null)
            {
                if (callbacks.LoadSceneFailureCallback != null)
                {
                    callbacks.LoadSceneFailureCallback(sceneAssetName, LoadResourceStatus.NotReady, "Not Exists Scene Asset.", userData);
                    return;
                }

                throw new GameFrameworkException(Utility.Text.Format("Not Exists Scene Asset:{0}", sceneAssetName));
            }
            
            package.LoadScene(sceneAssetName, priority, callbacks, userData);
        }

        public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks callbacks)
        {
            UnloadScene(sceneAssetName, callbacks, null);
        }

        public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks callbacks, object userData)
        {
            var package = FindAssetPackage(sceneAssetName);
            if (package == null)
            {
                if (callbacks.UnloadSceneFailureCallback != null)
                {
                    callbacks.UnloadSceneFailureCallback(sceneAssetName, userData);
                    return;
                }
                
                throw new GameFrameworkException(Utility.Text.Format("Scene Asset Package Not Found:{0}", sceneAssetName));
            }
            
            package.UnloadScene(sceneAssetName, callbacks, userData);
        }
    }
}