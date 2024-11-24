using GameFramework;
using GameFramework.Resource;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 界面资源辅助器基类。
    /// </summary>
    public abstract class UIFormAssetHelperBase : MonoBehaviour, IUIFormAssetHelper
    {
        public abstract void LoadUIFormAsset(string uiFormAssetName, int priority, LoadAssetCallbacks loadAssetCallbacks, object userData);
    }
}