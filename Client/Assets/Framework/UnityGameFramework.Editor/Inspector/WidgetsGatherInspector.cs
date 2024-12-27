using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;
using Object = UnityEngine.Object;

namespace UnityGameFramework.Editor
{
    [CustomEditor(typeof(WidgetsGather))]
    internal sealed class WidgetsGatherInspector : GameFrameworkInspector
    {
        /// <summary>
        /// 每页最大的item数量。
        /// </summary>
        private const int PAGE_ITEM_LIMIT = 10;

        private int m_PageIndex = 0;

        private string m_AddKey = string.Empty;
        private Object m_AddValue = null;

        public static bool UpdateWidgetsGather(WidgetsGather t)
        {
            var isModify = false;
            
            var widgets = t.GetComponentsInChildren<Widget>();
            foreach (var widget in widgets)
            {
                var widgetName = !string.IsNullOrEmpty(widget.WidgetName) ? widget.WidgetName : widget.name;
                var widgetType = System.Type.GetType(widget.WidgetTypeName);
                if (widgetType == null)
                {
                    Debug.LogError($"无效的组件类型在节点{widget.name}上: {widget.WidgetTypeName}");
                    continue;
                }

                Object widgetObject;

                if (widgetType == typeof(GameObject))
                {
                    widgetObject = widget.gameObject;
                }
                else if (typeof(Component).IsAssignableFrom(widgetType))
                {
                    widgetObject = widget.GetComponent(widgetType);
                }
                else
                {
                    Debug.LogError($"无效的组件类型在节点{widget.name}上: {widget.WidgetTypeName}");
                    continue;
                }

                if (widgetObject == null)
                {
                    Debug.LogError($"无效的组件类型在节点{widget.name}上: {widget.WidgetTypeName}");
                    continue;
                }

                var index = t.WidgetNames.IndexOf(widgetName);
                if (index >= 0)
                {
                    if (t.Widgets[index] != widgetObject)
                    {
                        Debug.LogError($"重复的挂件名称: {widgetName}");
                    }
                                    
                    continue;
                }
                                
                t.WidgetNames.Add(widgetName);
                t.Widgets.Add(widgetObject);

                isModify = true;
            }
            
            return isModify;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            var t = (WidgetsGather)target;
            t.Widgets ??= new List<Object>();
            t.WidgetNames ??= new List<string>();

            var count = Math.Min(t.WidgetNames.Count, t.Widgets.Count);
            var pageCount = Mathf.CeilToInt((float)count / PAGE_ITEM_LIMIT);
            m_PageIndex = pageCount > 0 ? Mathf.Clamp(m_PageIndex, 0, pageCount - 1) : 0;

            var isModify = false;

            using (GameFrameworkEditorGUIUtility.MakeDisabledGroupScope(EditorApplication.isPlayingOrWillChangePlaymode))
            {
                using (GameFrameworkEditorGUIUtility.MakeVerticalScope("box"))
                {
                    // 绘制添加按钮。
                    using (GameFrameworkEditorGUIUtility.MakeHorizontalScope("box"))
                    {
                        // key框, value框, +
                        m_AddKey = EditorGUILayout.TextField(m_AddKey);

                        var value = EditorGUILayout.ObjectField(m_AddValue, typeof(Object), true);
                        if (value != m_AddValue)
                        {
                            m_AddValue = value;
                            if (value != null && string.IsNullOrEmpty(m_AddKey))
                            {
                                // 默认传入节点的名字。
                                m_AddKey = value.name;
                            }
                        }

                        // key value有一个为空时禁用+按钮
                        using (GameFrameworkEditorGUIUtility.MakeDisabledGroupScope(string.IsNullOrEmpty(m_AddKey) || m_AddValue == null))
                        {
                            if (GUILayout.Button("+", GUILayout.Width(20)))
                            {
                                t.WidgetNames.Add(m_AddKey);
                                t.Widgets.Add(m_AddValue);

                                m_AddKey = string.Empty;
                                m_AddValue = null;

                                ++count;

                                isModify = true;
                            }
                        }
                    }

                    // 绘制组件字典对。
                    using (GameFrameworkEditorGUIUtility.MakeVerticalScope("box"))
                    {
                        // 限制每页的显示数量。
                        var startIndex = m_PageIndex * PAGE_ITEM_LIMIT;

                        for (var offset = 0; offset < PAGE_ITEM_LIMIT; ++offset)
                        {
                            var index = startIndex + offset;
                            if (index >= count)
                            {
                                break;
                            }

                            var key = t.WidgetNames[index];
                            var value = t.Widgets[index];

                            // 绘制Key、value对的字典。
                            using (GameFrameworkEditorGUIUtility.MakeHorizontalScope())
                            {
                                t.WidgetNames[index] = EditorGUILayout.TextField(key);
                                t.Widgets[index] = EditorGUILayout.ObjectField(value, typeof(Object), true);

                                if (GUILayout.Button("X", GUILayout.Width(20)))
                                {
                                    t.WidgetNames.RemoveAt(index);
                                    t.Widgets.RemoveAt(index);

                                    --count;

                                    isModify = true;
                                }
                            }
                        }
                    }

                    // 绘制分页按钮。
                    using (GameFrameworkEditorGUIUtility.MakeHorizontalScope("box"))
                    {
                        GUILayout.FlexibleSpace();
                        // EditorGUILayout.Space();
                        
                        // 绘制上一页按钮。在index已经为0时禁用。
                        using (GameFrameworkEditorGUIUtility.MakeDisabledGroupScope(m_PageIndex <= 0))
                        {
                            if (GUILayout.Button("<<", GUILayout.Width(50)))
                            {
                                --m_PageIndex;
                            }
                        }
                        
                        GUILayout.FlexibleSpace();
                        
                        // 绘制 当前页数/总页数 的Label
                        EditorGUILayout.LabelField($"{m_PageIndex + 1}/{pageCount}", GUILayout.MaxWidth(60));
                        
                        GUILayout.FlexibleSpace();

                        // 绘制下一页按钮。在index已经为最后一页时禁用。
                        using (GameFrameworkEditorGUIUtility.MakeDisabledGroupScope(m_PageIndex >= pageCount - 1))
                        {
                            if (GUILayout.Button(">>", GUILayout.Width(50)))
                            {
                                ++m_PageIndex;
                            }
                        }
                        
                        GUILayout.FlexibleSpace();
                    }

                    // 绘制功能按钮区
                    using (GameFrameworkEditorGUIUtility.MakeVerticalScope("box"))
                    {
                        if (GUILayout.Button("清空收集器"))
                        {
                            t.WidgetNames.Clear();
                            t.Widgets.Clear();

                            count = 0;
                            m_PageIndex = 0;

                            isModify = true;
                        }

                        if (GUILayout.Button("收集所有Widget组件(自己和子节点)"))
                        {
                            if (UpdateWidgetsGather(t))
                            {
                                
                            }
                        }

                        if (t.GetComponent<WidgetsGatherGenerator>() == null)
                        {
                            if (GUILayout.Button("生成静态绑定器"))
                            {
                                var generator = t.gameObject.AddComponent<WidgetsGatherGenerator>();
                                WidgetsGatherGeneratorInspector.SetMissingDefaultValue(generator);
                            }
                        }
                    }
                }
            }

            if (isModify)
            {
                EditorUtility.SetDirty(t);
            }

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }
    }
}