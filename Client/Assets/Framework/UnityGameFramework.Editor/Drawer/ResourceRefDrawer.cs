using System;
using Framework.UnityGameFramework;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;
using Object = UnityEngine.Object;

namespace UnityGameFramework.Editor
{
    [CustomPropertyDrawer(typeof(ResourceRef))]
    public class ResourceRefDrawer : PropertyDrawer
    {
        private SerializedProperty m_ResourcePathProperty;
        private SerializedProperty m_GuidProperty;
        private System.Type m_ResourceType;
        private string m_CacheAssetPath;
        private Object m_CacheObject;
        private string m_HelpText;
        private MessageType m_HelpMessageType;

        private const float ERROR_BOX_HEIGHT = 32f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var asset = EditorGUI.ObjectField(rect, label, m_CacheObject, m_ResourceType, false);

            if (asset != m_CacheObject)
            {
                if (asset == null)
                {
                    // 清空引用
                    m_GuidProperty.stringValue = string.Empty;
                    m_ResourcePathProperty.stringValue = string.Empty;
                }
                else
                {
                    m_ResourcePathProperty.stringValue = AssetDatabase.GetAssetPath(asset);
                    m_GuidProperty.stringValue = AssetDatabase.AssetPathToGUID(m_ResourcePathProperty.stringValue);
                }
            }

            if (m_HelpMessageType == MessageType.None)
            {
                return;
            }
            
            rect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, ERROR_BOX_HEIGHT);
            EditorGUI.HelpBox(rect, m_HelpText, m_HelpMessageType);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            m_ResourcePathProperty = property.FindPropertyRelative("m_ResourcePath");
            m_GuidProperty = property.FindPropertyRelative("m_Guid");
            m_ResourceType = property.boxedValue is ResourceRef referenceRef ? referenceRef.ResourceType : typeof(Object);

            var guid = m_GuidProperty.stringValue;
            (m_CacheAssetPath, m_CacheObject) = AssetDatabaseUtility.LoadAssetWithGUID(guid, m_ResourceType);

            if (string.IsNullOrEmpty(guid))
            {
                m_HelpText = string.Empty;
                m_HelpMessageType = MessageType.None;
            }
            else if (m_CacheObject != null)
            {
                m_HelpText = string.Empty;
                m_HelpMessageType = MessageType.None;

                m_ResourcePathProperty.stringValue = m_CacheAssetPath;
            }
            else
            {
                m_HelpText = $"资源引用丢失, 原资源路径: {m_ResourcePathProperty.stringValue}";
                m_HelpMessageType = MessageType.Error;
            }

            return m_HelpMessageType != MessageType.None ? ERROR_BOX_HEIGHT + EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight;
        }
    }

    [CustomPropertyDrawer(typeof(HotfixConfigurationResourceRef))]
    public class HotfixConfigurationResourceRefDrawer : ResourceRefDrawer
    {
    }
}