using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// GameObject拓展方法。
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// 从自己或父节点上搜索指定类型的组件。
        /// </summary>
        public static T GetComponentInSelfOrParent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.GetComponentInParent<T>();
            }

            return component;
        }

        /// <summary>
        /// 从自己或父节点上搜索指定类型的组件。
        /// </summary>
        public static T GetComponentInSelfOrParent<T>(this GameObject gameObject, bool includeInactive) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.GetComponentInParent<T>(includeInactive);
            }

            return component;
        }

        /// <summary>
        /// 从自己或父节点上搜索指定类型的组件。
        /// </summary>
        public static T GetComponentInSelfOrParent<T>(this Component component) where T : Component
        {
            return component.gameObject.GetComponentInSelfOrParent<T>();
        }

        /// <summary>
        /// 从自己或父节点上搜索指定类型的组件。
        /// </summary>
        public static T GetComponentInSelfOrParent<T>(this Component component, bool includeInactive) where T : Component
        {
            return component.gameObject.GetComponentInSelfOrParent<T>(includeInactive);
        }
    }
}