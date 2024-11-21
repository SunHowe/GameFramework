using Framework.UnityGameFramework;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Editor
{
    [CustomPropertyDrawer(typeof(ResourceRef))]
    public class ResourceRefDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position, "资源引用");
        }
    }

    [CustomPropertyDrawer(typeof(HotfixConfigurationResourceRef))]
    public class HotfixConfigurationResourceRefDrawer : ResourceRefDrawer
    {
    }
}