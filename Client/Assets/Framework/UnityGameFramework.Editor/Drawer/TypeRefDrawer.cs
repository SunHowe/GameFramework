using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Editor
{
    [CustomPropertyDrawer(typeof(TypeRef))]
    public class TypeRefDrawer : PropertyDrawer
    {
        private SerializedProperty m_AssemblyName;
        private SerializedProperty m_FullName;
        private System.Type m_BaseType;

        private string[] m_TypeFullNames;
        private readonly Dictionary<string, string> m_TypeFullName2AssemblyNameDict = new Dictionary<string, string>();

        private const float HEIGHT = 40f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            m_AssemblyName = property.FindPropertyRelative("m_AssemblyName");
            m_FullName = property.FindPropertyRelative("m_FullName");

            var baseType = property.boxedValue is TypeRef typeRef ? typeRef.BaseType : null;
            if (baseType == null)
            {
                EditorGUI.HelpBox(position, "BaseType is null.", MessageType.Error);
                return;
            }
            
            // 刷新列表
            if (baseType != m_BaseType)
            {
                m_BaseType = baseType;
                m_TypeFullName2AssemblyNameDict.Clear();

                var types = Type.GetTypes(m_BaseType, Type.AssemblyType.Runtime, Type.AssemblyType.Hotfix);
                m_TypeFullNames = new string[types.Length];
                for (var index = 0; index < types.Length; index++)
                {
                    var type = types[index];
                    m_TypeFullNames[index] = type.FullName;
                    m_TypeFullName2AssemblyNameDict.Add(type.FullName!, type.Assembly.FullName);
                }
            }
            
            var typeFullName = m_FullName.stringValue;
            var indexOf = !string.IsNullOrEmpty(typeFullName) ? Array.IndexOf(m_TypeFullNames, typeFullName) : -1;

            var selectedIndex = EditorGUI.Popup(position, label.text, indexOf, m_TypeFullNames);
            if (selectedIndex >= 0 && selectedIndex != indexOf)
            {
                indexOf = selectedIndex;
                typeFullName = m_TypeFullNames[indexOf];
            }
            
            m_FullName.stringValue = typeFullName;
            m_AssemblyName.stringValue = m_TypeFullName2AssemblyNameDict.TryGetValue(typeFullName, out var assemblyName) ? assemblyName : string.Empty;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return HEIGHT;
        }
    }

    [CustomPropertyDrawer(typeof(HotfixAppTypeRef))]
    public class HotfixAppTypeRefDrawer : TypeRefDrawer
    {
    }
}