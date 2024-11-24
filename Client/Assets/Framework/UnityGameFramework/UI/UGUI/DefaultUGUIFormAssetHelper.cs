using GameFramework.Resource;

namespace UnityGameFramework.Runtime.UGUI
{
    /// <summary>
    /// 默认的UGUI界面资源辅助器。
    /// </summary>
    public sealed class DefaultUGUIFormAssetHelper : UIFormAssetHelperBase
    {
        private ResourceComponent m_ResourceComponent = null;
        
        /// <summary>
        /// 异步加载界面资源。
        /// </summary>
        /// <param name="uiFormAssetName">要加载界面资源的名称。</param>
        /// <param name="priority">加载资源的优先级。</param>
        /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
        /// <param name="userData">用户自定义数据。</param>
        public override void LoadUIFormAsset(string uiFormAssetName, int priority, LoadAssetCallbacks loadAssetCallbacks, object userData)
        {
            m_ResourceComponent.LoadAsset(uiFormAssetName, priority, loadAssetCallbacks, userData);
        }

        private void Start()
        {
            m_ResourceComponent = GameEntry.GetComponent<ResourceComponent>();
            if (m_ResourceComponent == null)
            {
                Log.Fatal("Resource component is invalid.");
                return;
            }
        }
    }
}