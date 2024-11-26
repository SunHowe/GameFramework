using System.IO;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Editor.ResourceTools;

namespace UnityGameFramework.Editor
{
    public static class EditorSettings
    {
        [ResourceEditorConfigPath]
        public static string ResourceEditorConfigPath => Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "GameMain/Configs/ResourceEditor.xml"));
        
        [BuildSettingsConfigPath]
        public static string BuildSettingsConfigPath => Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "GameMain/Configs/BuildSettings.xml"));
        
        [ResourceBuilderConfigPath]
        public static string ResourceBuilderConfigPath => Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "GameMain/Configs/ResourceBuilder.xml"));
        
        [ResourceCollectionConfigPath]
        public static string ResourceCollectionConfigPath => Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "GameMain/Configs/ResourceCollection.xml"));
    }
}