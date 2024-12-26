using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Editor
{
    [CustomEditor(typeof(Widget))]
    internal sealed class WidgetInspector : GameFrameworkInspector
    {
        private readonly List<Component> m_ComponentBuffer = new List<Component>();
        private readonly HashSet<System.Type> m_ComponentTypeBuffer = new HashSet<System.Type>();
        private readonly List<System.Type> m_IgnoreTypes = new List<System.Type>()
        {
            typeof(Widget),
        };
        
        private readonly List<string> m_TypeNames = new List<string>();
        private readonly List<string> m_FullTypeNames = new List<string>();
        private readonly List<System.Type> m_Types = new List<System.Type>();
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();
            
            var t = (Widget)target;

            using (GameFrameworkEditorGUIUtility.MakeDisabledGroupScope(EditorApplication.isPlayingOrWillChangePlaymode))
            {
                t.WidgetName = EditorGUILayout.TextField("WidgetName", t.WidgetName);

                var index = m_FullTypeNames.IndexOf(t.WidgetTypeName);
                if (index < 0)
                {
                    index = 0;
                }
                
                var newIndex = EditorGUILayout.Popup("WidgetType", index, m_TypeNames.ToArray());
                if (newIndex != index)
                {
                    t.WidgetTypeName = m_FullTypeNames[newIndex];
                }
            }
            
            serializedObject.ApplyModifiedProperties();
            Repaint();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshTypeNames();
        }

        private void OnEnable()
        {
            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_ComponentBuffer.Clear();
            m_ComponentTypeBuffer.Clear();
            
            var t = (Widget)target;
            t.GetComponents(m_ComponentBuffer);
            
            m_Types.Clear();
            m_FullTypeNames.Clear();
            m_TypeNames.Clear();
            
            foreach (var component in m_ComponentBuffer)
            {
                var componentType = component.GetType();
                if (m_IgnoreTypes.Contains(componentType))
                {
                    continue;
                }
                
                if (!m_ComponentTypeBuffer.Add(componentType))
                {
                    continue;
                }

                m_Types.Add(componentType);
            }
            
            m_Types.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            m_Types.Insert(0, typeof(GameObject));
            foreach (var type in m_Types)
            {
                m_TypeNames.Add(type.Name);
                m_FullTypeNames.Add(type.FullName);
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}