using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 挂件收集器组件。挂载某个GameObject，用于配置供逻辑层快速获取的子节点组件使用。
    /// </summary>
    public sealed class WidgetsGather : MonoBehaviour
    {
        /// <summary>
        /// 挂件名称。
        /// </summary>
        public List<string> WidgetNames;

        /// <summary>
        /// 挂件
        /// </summary>
        public List<Object> Widgets;

        /// <summary>
        /// 转换后的字典实例。
        /// </summary>
        private readonly Dictionary<string, Object> m_WidgetsDictionary = new Dictionary<string, Object>();

        private void Awake()
        {
            if (WidgetNames == null || Widgets == null)
            {
                return;
            }

            var count = Math.Min(WidgetNames.Count, Widgets.Count);
            for (var i = 0; i < count; i++)
            {
                m_WidgetsDictionary.Add(WidgetNames[i], Widgets[i]);
            }
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public Object GetWidget(string widgetName)
        {
            return m_WidgetsDictionary.TryGetValue(widgetName, out var widget) ? widget : null;
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public T GetWidget<T>(string widgetName) where T : Object
        {
            return GetWidget(widgetName) as T;
        }
    }
}