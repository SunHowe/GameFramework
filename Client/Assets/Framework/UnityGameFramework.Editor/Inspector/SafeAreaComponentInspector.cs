//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEditor;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Editor
{
    [CustomEditor(typeof(SafeAreaComponent))]
    internal sealed class SafeAreaComponentInspector : GameFrameworkInspector
    {
        private SerializedProperty m_EnableSafeAreaChangeEvent = null;

        private HelperInfo<SafeAreaHelperBase> m_SafeAreaHelperInfo = new HelperInfo<SafeAreaHelperBase>("SafeArea");

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            SafeAreaComponent t = (SafeAreaComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                EditorGUILayout.PropertyField(m_EnableSafeAreaChangeEvent);
                m_SafeAreaHelperInfo.Draw();
            }
            EditorGUI.EndDisabledGroup();

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
            m_EnableSafeAreaChangeEvent = serializedObject.FindProperty("m_EnableSafeAreaChangeEvent");
            m_SafeAreaHelperInfo.Init(serializedObject);

            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_SafeAreaHelperInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
