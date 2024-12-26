using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 挂件管理功能拓展方法。
    /// </summary>
    public static class WidgetsManageFeatureExtensions
    {
        /// <summary>
        /// 设置挂件源属性。
        /// </summary>
        public static void SetWidgetsGather(this FeatureContainer container, WidgetsGather widgets)
        {
            container.AddFeature<WidgetsManageFeature>().SetWidgetsGather(widgets);
        }

        /// <summary>
        /// 设置挂件源属性。
        /// </summary>
        public static void SetWidgetGather(this FeatureContainer container, GameObject gameObject)
        {
            var widgets = gameObject.GetComponent<WidgetsGather>();
            if (widgets == null)
            {
                Log.Error("WidgetsGather is invalid.");
                return;
            }
            
            container.SetWidgetsGather(widgets);
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public static Object GetWidget(this FeatureContainer container, string widgetName)
        {
            return container.AddFeature<WidgetsManageFeature>().GetWidget(widgetName);
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public static T GetWidget<T>(this FeatureContainer container, string widgetName) where T : Object
        {
            return container.AddFeature<WidgetsManageFeature>().GetWidget<T>(widgetName);
        }

        /// <summary>
        /// 设置挂件源属性。
        /// </summary>
        public static void SetWidgetsGather(this IFeatureContainerOwner owner, WidgetsGather widgets)
        {
            owner.FeatureContainer.SetWidgetsGather(widgets);
        }

        /// <summary>
        /// 设置挂件源属性。
        /// </summary>
        public static void SetWidgetGather(this IFeatureContainerOwner owner, GameObject gameObject)
        {
            owner.FeatureContainer.SetWidgetGather(gameObject);
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public static Object GetWidget(this IFeatureContainerOwner owner, string widgetName)
        {
            return owner.FeatureContainer.GetWidget(widgetName);
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public static T GetWidget<T>(this IFeatureContainerOwner owner, string widgetName) where T : Object
        {
            return owner.FeatureContainer.GetWidget<T>(widgetName);
        }
    }
}