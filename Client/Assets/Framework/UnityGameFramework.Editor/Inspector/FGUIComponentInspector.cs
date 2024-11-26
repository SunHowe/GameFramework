//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEditor;
using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;

namespace UnityGameFramework.Editor
{
    [CustomEditor(typeof(FGUIComponent))]
    internal sealed class FGUIComponentInspector : GameFrameworkInspector
    {
        private SerializedProperty m_EnableOpenUIFormSuccessEvent = null;
        private SerializedProperty m_EnableOpenUIFormFailureEvent = null;
        private SerializedProperty m_EnableOpenUIFormUpdateEvent = null;
        private SerializedProperty m_EnableOpenUIFormDependencyAssetEvent = null;
        private SerializedProperty m_EnableCloseUIFormCompleteEvent = null;
        private SerializedProperty m_InstanceAutoReleaseInterval = null;
        private SerializedProperty m_InstanceCapacity = null;
        private SerializedProperty m_InstanceExpireTime = null;
        private SerializedProperty m_InstancePriority = null;
        private SerializedProperty m_EnableUIFormOpenQueue = null;
        private SerializedProperty m_UIGroups = null;
        private SerializedProperty m_UnloadUnusedUIPackageImmediately = null;
        private SerializedProperty m_FGUIPackageMappingOnResources = null;
        private SerializedProperty m_FGUIPackageMappingHotfix = null;
        private SerializedProperty m_DesignResolutionX = null;
        private SerializedProperty m_DesignResolutionY = null;
        private SerializedProperty m_ScreenMatchMode = null;

        private HelperInfo<UIFormHelperBase> m_UIFormHelperInfo = new HelperInfo<UIFormHelperBase>("UIForm");
        private HelperInfo<FGUIAssetLoaderHelperBase> m_FUIAssetLoaderHelperInfo = new HelperInfo<FGUIAssetLoaderHelperBase>("FGUIAssetLoader");

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            FGUIComponent t = (FGUIComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                EditorGUILayout.PropertyField(m_EnableOpenUIFormSuccessEvent);
                EditorGUILayout.PropertyField(m_EnableOpenUIFormFailureEvent);
                EditorGUILayout.PropertyField(m_EnableOpenUIFormUpdateEvent);
                EditorGUILayout.PropertyField(m_EnableOpenUIFormDependencyAssetEvent);
                EditorGUILayout.PropertyField(m_EnableCloseUIFormCompleteEvent);
                EditorGUILayout.PropertyField(m_EnableUIFormOpenQueue);
                EditorGUILayout.PropertyField(m_UnloadUnusedUIPackageImmediately);
                EditorGUILayout.PropertyField(m_DesignResolutionX);
                EditorGUILayout.PropertyField(m_DesignResolutionY);
                EditorGUILayout.PropertyField(m_ScreenMatchMode);
                EditorGUILayout.PropertyField(m_UIGroups, true);
                EditorGUILayout.PropertyField(m_FGUIPackageMappingOnResources);
                EditorGUILayout.PropertyField(m_FGUIPackageMappingHotfix);
            }
            EditorGUI.EndDisabledGroup();

            float instanceAutoReleaseInterval = EditorGUILayout.DelayedFloatField("Instance Auto Release Interval", m_InstanceAutoReleaseInterval.floatValue);
            if (instanceAutoReleaseInterval != m_InstanceAutoReleaseInterval.floatValue)
            {
                if (EditorApplication.isPlaying)
                {
                    t.InstanceAutoReleaseInterval = instanceAutoReleaseInterval;
                }
                else
                {
                    m_InstanceAutoReleaseInterval.floatValue = instanceAutoReleaseInterval;
                }
            }

            int instanceCapacity = EditorGUILayout.DelayedIntField("Instance Capacity", m_InstanceCapacity.intValue);
            if (instanceCapacity != m_InstanceCapacity.intValue)
            {
                if (EditorApplication.isPlaying)
                {
                    t.InstanceCapacity = instanceCapacity;
                }
                else
                {
                    m_InstanceCapacity.intValue = instanceCapacity;
                }
            }

            float instanceExpireTime = EditorGUILayout.DelayedFloatField("Instance Expire Time", m_InstanceExpireTime.floatValue);
            if (instanceExpireTime != m_InstanceExpireTime.floatValue)
            {
                if (EditorApplication.isPlaying)
                {
                    t.InstanceExpireTime = instanceExpireTime;
                }
                else
                {
                    m_InstanceExpireTime.floatValue = instanceExpireTime;
                }
            }

            int instancePriority = EditorGUILayout.DelayedIntField("Instance Priority", m_InstancePriority.intValue);
            if (instancePriority != m_InstancePriority.intValue)
            {
                if (EditorApplication.isPlaying)
                {
                    t.InstancePriority = instancePriority;
                }
                else
                {
                    m_InstancePriority.intValue = instancePriority;
                }
            }

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_UIFormHelperInfo.Draw();
                m_FUIAssetLoaderHelperInfo.Draw();
            }
            EditorGUI.EndDisabledGroup();

            if (EditorApplication.isPlaying && IsPrefabInHierarchy(t.gameObject))
            {
                EditorGUILayout.LabelField("UI Group Count", t.UIGroupCount.ToString());
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
            m_EnableOpenUIFormSuccessEvent = serializedObject.FindProperty("m_EnableOpenUIFormSuccessEvent");
            m_EnableOpenUIFormFailureEvent = serializedObject.FindProperty("m_EnableOpenUIFormFailureEvent");
            m_EnableOpenUIFormUpdateEvent = serializedObject.FindProperty("m_EnableOpenUIFormUpdateEvent");
            m_EnableOpenUIFormDependencyAssetEvent = serializedObject.FindProperty("m_EnableOpenUIFormDependencyAssetEvent");
            m_EnableCloseUIFormCompleteEvent = serializedObject.FindProperty("m_EnableCloseUIFormCompleteEvent");
            m_InstanceAutoReleaseInterval = serializedObject.FindProperty("m_InstanceAutoReleaseInterval");
            m_InstanceCapacity = serializedObject.FindProperty("m_InstanceCapacity");
            m_InstanceExpireTime = serializedObject.FindProperty("m_InstanceExpireTime");
            m_InstancePriority = serializedObject.FindProperty("m_InstancePriority");
            m_EnableUIFormOpenQueue = serializedObject.FindProperty("m_EnableUIFormOpenQueue");
            m_UIGroups = serializedObject.FindProperty("m_UIGroups");
            m_UnloadUnusedUIPackageImmediately = serializedObject.FindProperty("m_UnloadUnusedUIPackageImmediately");
            m_FGUIPackageMappingOnResources = serializedObject.FindProperty("m_FGUIPackageMappingOnResources");
            m_FGUIPackageMappingHotfix = serializedObject.FindProperty("m_FGUIPackageMappingHotfix");
            m_DesignResolutionX = serializedObject.FindProperty("m_DesignResolutionX");
            m_DesignResolutionY = serializedObject.FindProperty("m_DesignResolutionY");
            m_ScreenMatchMode = serializedObject.FindProperty("m_ScreenMatchMode");

            m_UIFormHelperInfo.Init(serializedObject);
            m_FUIAssetLoaderHelperInfo.Init(serializedObject);

            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_UIFormHelperInfo.Refresh();
            m_FUIAssetLoaderHelperInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
