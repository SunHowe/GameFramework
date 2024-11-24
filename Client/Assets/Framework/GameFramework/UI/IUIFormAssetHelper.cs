using GameFramework.Resource;

namespace GameFramework
{
    /// <summary>
    /// 界面资源辅助器。
    /// </summary>
    public interface IUIFormAssetHelper
    {
        /// <summary>
        /// 异步加载界面资源。
        /// </summary>
        /// <param name="uiFormAssetName">要加载界面资源的名称。</param>
        /// <param name="priority">加载资源的优先级。</param>
        /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
        /// <param name="userData">用户自定义数据。</param>
        void LoadUIFormAsset(string uiFormAssetName, int priority, LoadAssetCallbacks loadAssetCallbacks, object userData);
    }
}