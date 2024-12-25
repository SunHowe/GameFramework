using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 挂件管理功能。
    /// </summary>
    public sealed class WidgetsManageFeature : Feature
    {
        /// <summary>
        /// 挂件数据源。
        /// </summary>
        private WidgetsGather m_WidgetsGather;
        
        /// <summary>
        /// 设置挂件数据源组件。
        /// </summary>
        public void SetWidgetsGather(WidgetsGather widgets)
        {
            m_WidgetsGather = widgets;
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public Object GetWidget(string widgetName)
        {
            if (m_WidgetsGather == null)
            {
                Log.Error("Widgets source is invalid.");
                return null;
            }

            return m_WidgetsGather.GetWidget(widgetName);
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public T GetWidget<T>(string widgetName) where T : Object
        {
            if (m_WidgetsGather == null)
            {
                Log.Error("Widgets source is invalid.");
                return null;
            }

            return m_WidgetsGather.GetWidget<T>(widgetName);
        }
        
        public override void Awake(object featureOwner, FeatureContainer featureContainer)
        {
            base.Awake(featureOwner, featureContainer);

            // 如果持有者是MonoBehaviour 则尝试从持有者上获取挂载节点的组件。
            if (featureOwner is not MonoBehaviour monoBehaviour)
            {
                return;
            }
            
            var widgets = monoBehaviour.GetComponent<WidgetsGather>();
            if (widgets != null)
            {
                SetWidgetsGather(widgets);
            }
        }

        public override void Shutdown()
        {
            m_WidgetsGather = null;
        }
    }
}