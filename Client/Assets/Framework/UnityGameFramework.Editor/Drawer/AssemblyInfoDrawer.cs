using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Editor
{
    [CustomPropertyDrawer(typeof(AssemblyInfo))]
    public class AssemblyInfoDrawer : PropertyDrawer
    {
        private SerializedProperty m_NameProperty;
        private SerializedProperty m_GuidProperty;
        
        private Object m_CacheObject;
        private string m_HelpText;
        private MessageType m_HelpMessageType;
        
        private const float ERROR_BOX_HEIGHT = 32f;
        private const float LINE_HEIGHT = 16f;
        private const float PADDING = 4f;
        private const float BASIC_HEIGHT = LINE_HEIGHT;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rect = new Rect(position.x, position.y, position.width, LINE_HEIGHT);
            var asset = EditorGUI.ObjectField(rect, m_CacheObject, typeof(AssemblyDefinitionAsset), false);

            if (asset != m_CacheObject)
            {
                if (asset == null)
                {
                    // 清空引用
                    m_GuidProperty.stringValue = string.Empty;
                    m_NameProperty.stringValue = string.Empty;
                }
                else
                {
                    m_NameProperty.stringValue = asset.name;
                    m_GuidProperty.stringValue = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset));
                }
            }

            #region [绘制提示框]

            if (m_HelpMessageType == MessageType.None)
            {
                return;
            }

            rect = new Rect(position.x, rect.y + rect.height + PADDING, position.width, ERROR_BOX_HEIGHT);
            EditorGUI.HelpBox(rect, m_HelpText, m_HelpMessageType);

            #endregion
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            m_NameProperty = property.FindPropertyRelative("Name");
            m_GuidProperty = property.FindPropertyRelative("Guid");
            
            var guid = m_GuidProperty.stringValue;
            (_, m_CacheObject) = AssetDatabaseUtility.LoadAssetWithGUID(guid, typeof(AssemblyDefinitionAsset));

            if (string.IsNullOrEmpty(guid))
            {
                m_HelpText = string.Empty;
                m_HelpMessageType = MessageType.None;
            }
            else if (m_CacheObject != null)
            {
                m_HelpText = string.Empty;
                m_HelpMessageType = MessageType.None;
                
                m_NameProperty.stringValue = m_CacheObject.name;
            }
            else
            {
                m_HelpText = $"程序集引用丢失: {m_NameProperty.stringValue}";
                m_HelpMessageType = MessageType.Error;
            }

            return m_HelpMessageType != MessageType.None ? ERROR_BOX_HEIGHT + PADDING + BASIC_HEIGHT : BASIC_HEIGHT;
        }
    }
}