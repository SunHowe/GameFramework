using UnityEditor;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Editor
{
    [CustomEditor(typeof(HotfixComponent))]
    internal sealed class HotfixComponentInspector : GameFrameworkInspector
    {
        private readonly HelperInfo<HotfixAssetHelperBase> m_HotfixAssetHelperInfo = new HelperInfo<HotfixAssetHelperBase>("HotfixAsset");
        private SerializedProperty m_HotfixConfigResourceRef;

        public override void OnInspectorGUI()
        {
            HotfixComponent t = (HotfixComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_HotfixAssetHelperInfo.Draw();
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

            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_HotfixAssetHelperInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }
    }
}