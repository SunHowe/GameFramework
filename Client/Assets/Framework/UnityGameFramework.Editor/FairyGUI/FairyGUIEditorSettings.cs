using System.Collections.Generic;

namespace UnityGameFramework.Editor.FairyGUI
{
    /// <summary>
    /// FairyGUI 编辑器设置。
    /// </summary>
    public class FairyGUIEditorSettings
    {
        public static FairyGUIEditorSettings Instance { get; } = new();
        
        /// <summary>
        /// 资源目录设置 TODO 目前先不做编辑器 直接写死在代码里
        /// </summary>
        public readonly List<FairyGUIResDirectorySetting> ResDirectories = new()
        {
            new FairyGUIResDirectorySetting
            {
                DirectoryRoot = "Assets/GameMain/Resources/UI/",
                PackageMappingOutputPath = "Assets/GameMain/Resources/UI/PackageMapping.asset",
                CodeGenerateSetting = new FairyGUICodeGenerateSetting
                {
                    Namespace = "GameMono.UI",
                    ScribanTemplateDirectory = "../Scriban/GameMono/",
                    UIBindingCodeDirectory = "Assets/GameMain/Scripts/GameMono/UI/Binding/",
                    UILogicCodeDirectory = "Assets/GameMain/Scripts/GameMono/UI/Logic/",
                },
            },
            new FairyGUIResDirectorySetting
            {
                DirectoryRoot = "Assets/GameMain/Res/UI/",
                PackageMappingOutputPath = "Assets/GameMain/Res/UI/PackageMapping.asset",
                CodeGenerateSetting = new FairyGUICodeGenerateSetting
                {
                    Namespace = "GameLogic.UI",
                    ScribanTemplateDirectory = "../Scriban/GameLogic/",
                    UIBindingCodeDirectory = "Assets/GameMain/Scripts/GameLogic/UI/Binding/",
                    UILogicCodeDirectory = "Assets/GameMain/Scripts/GameLogic/UI/Logic/",
                }
            },
        };
    }
}