using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Editor
{
    [CustomEditor(typeof(HotfixConfiguration))]
    internal sealed class HotfixConfigurationInspector : GameFrameworkInspector
    {
        private SerializedProperty m_HotfixAssemblies;
        private SerializedProperty m_HotfixAppType;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            
            EditorGUILayout.PropertyField(m_HotfixAssemblies, new GUIContent("热更新程序集列表"));
            EditorGUILayout.PropertyField(m_HotfixAppType, new GUIContent("热更新应用程序类型"));

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }

        private void OnEnable()
        {
            m_HotfixAssemblies = serializedObject.FindProperty("m_HotfixAssemblies");
            m_HotfixAppType = serializedObject.FindProperty("m_HotfixAppType");
        }
    }
}