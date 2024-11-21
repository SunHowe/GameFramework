using UnityEditor;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Editor
{
    [CustomEditor(typeof(HotfixComponent))]
    internal sealed class HotfixComponentInspector : GameFrameworkInspector
    {
        private readonly HelperInfo<HotfixAssetHelperBase> m_HotfixAssetHelperInfo = new HelperInfo<HotfixAssetHelperBase>("HotfixAsset");
        private SerializedProperty m_HotfixConfigResourceRef;
        private SerializedProperty m_EnableLoadCompleteEvent;
        private SerializedProperty m_EnableLoadFailureEvent;
        private SerializedProperty m_EnableLoadStepChangedEvent;
        private SerializedProperty m_EnableLoadAssemblyFailureEvent;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            
            HotfixComponent t = (HotfixComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_HotfixAssetHelperInfo.Draw();
                EditorGUILayout.PropertyField(m_EnableLoadCompleteEvent);
                EditorGUILayout.PropertyField(m_EnableLoadFailureEvent);
                EditorGUILayout.PropertyField(m_EnableLoadStepChangedEvent);
                EditorGUILayout.PropertyField(m_EnableLoadAssemblyFailureEvent);
                
                EditorGUILayout.PropertyField(m_HotfixConfigResourceRef);
            }
            EditorGUI.EndDisabledGroup();

            if (EditorApplication.isPlaying && IsPrefabInHierarchy(t.gameObject))
            {
                
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
            m_HotfixAssetHelperInfo.Init(serializedObject);
            m_HotfixConfigResourceRef = serializedObject.FindProperty("m_HotfixConfigAssetRef");
            m_EnableLoadCompleteEvent = serializedObject.FindProperty("m_EnableLoadCompleteEvent");
            m_EnableLoadFailureEvent = serializedObject.FindProperty("m_EnableLoadFailureEvent");
            m_EnableLoadStepChangedEvent = serializedObject.FindProperty("m_EnableLoadStepChangedEvent");
            m_EnableLoadAssemblyFailureEvent = serializedObject.FindProperty("m_EnableLoadAssemblyFailureEvent");

            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_HotfixAssetHelperInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }
    }
}