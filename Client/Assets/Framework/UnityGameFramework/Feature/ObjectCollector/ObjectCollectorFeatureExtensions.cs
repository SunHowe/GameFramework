using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 对象收集器管理功能拓展方法。
    /// </summary>
    public static class ObjectCollectorFeatureExtensions
    {
        /// <summary>
        /// 设置对象收集器。
        /// </summary>
        public static void SetObjectCollector(this FeatureContainer container, ObjectCollector widgets)
        {
            container.AddFeature<ObjectCollectorFeature>().SetObjectCollector(widgets);
        }

        /// <summary>
        /// 设置对象收集器。
        /// </summary>
        public static void SetObjectCollector(this FeatureContainer container, GameObject gameObject)
        {
            var widgets = gameObject.GetComponent<ObjectCollector>();
            if (widgets == null)
            {
                Log.Error("ObjectCollector is invalid.");
                return;
            }
            
            container.SetObjectCollector(widgets);
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public static Object GetObject(this FeatureContainer container, string widgetName)
        {
            return container.AddFeature<ObjectCollectorFeature>().GetObject(widgetName);
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public static T Get<T>(this FeatureContainer container, string widgetName) where T : Object
        {
            return container.AddFeature<ObjectCollectorFeature>().Get<T>(widgetName);
        }

        /// <summary>
        /// 设置对象收集器。
        /// </summary>
        public static void SetObjectCollector(this IFeatureContainerOwner owner, ObjectCollector widgets)
        {
            owner.FeatureContainer.SetObjectCollector(widgets);
        }

        /// <summary>
        /// 设置对象收集器。
        /// </summary>
        public static void SetObjectCollector(this IFeatureContainerOwner owner, GameObject gameObject)
        {
            owner.FeatureContainer.SetObjectCollector(gameObject);
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public static Object GetObject(this IFeatureContainerOwner owner, string widgetName)
        {
            return owner.FeatureContainer.GetObject(widgetName);
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public static T Get<T>(this IFeatureContainerOwner owner, string widgetName) where T : Object
        {
            return owner.FeatureContainer.Get<T>(widgetName);
        }

        /// <summary>
        /// 获取物体收集器。
        /// </summary>
        public static ObjectCollector GetObjectCollector(this GameObject gameObject)
        {
            return gameObject.GetComponent<ObjectCollector>();
        }

        /// <summary>
        /// 获取物体收集器。
        /// </summary>
        public static ObjectCollector GetObjectCollector(this Component component)
        {
            return component.GetComponent<ObjectCollector>();
        }

        /// <summary>
        /// 获取物体收集器。
        /// </summary>
        public static T GetObjectCollector<T>(this GameObject gameObject) where T : ObjectCollectorBase
        {
            return gameObject.GetComponent<ObjectCollectorGenerator>()?.ObjectCollector as T;
        }

        /// <summary>
        /// 获取物体收集器。
        /// </summary>
        public static T GetObjectCollector<T>(this Component component) where T : ObjectCollectorBase
        {
            return component.GetComponent<ObjectCollectorGenerator>()?.ObjectCollector as T;
        }
    }
}