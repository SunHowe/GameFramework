using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FairyGUI;
using FairyGUI.CodeGenerator;
using FairyGUI.CodeGenerator.Core;
using FairyGUI.CodeGenerator.Scriban;
using UnityEditor;

namespace UnityGameFramework.Editor.FairyGUI
{
    /// <summary>
    /// FairyGUI 代码生成器。
    /// </summary>
    public static class FairyGUICodeGenerator
    {
        private static FairyGUICodeGenerateSetting s_Setting;
        
        public static void GenerateCode(string uiAssetsRoot, FairyGUICodeGenerateSetting setting)
        {
            s_Setting = setting;
            var filter = new UIComponentFilter(setting.Namespace);

            if (!Directory.Exists(setting.UIBindingCodeDirectory))
                Directory.CreateDirectory(setting.UIBindingCodeDirectory);

            // 记录此时的绑定文件列表
            var bindingFiles = Directory.GetFiles(setting.UIBindingCodeDirectory, "*.cs", SearchOption.AllDirectories);

            #region [生成UI绑定代码]

            // 每次都重新生成
            if (Directory.Exists(setting.UIBindingCodeDirectory))
                Directory.Delete(setting.UIBindingCodeDirectory, true);
            Directory.CreateDirectory(setting.UIBindingCodeDirectory);

            UICodeGenerator.Generate(uiAssetsRoot, "_fui.bytes", new ScribanCodeGenerator(GetBindingCodeExportSettings), filter);

            #endregion

            #region [生成UI逻辑代码]

            // 一个文件只生成一次
            if (!Directory.Exists(setting.UILogicCodeDirectory))
                Directory.CreateDirectory(setting.UILogicCodeDirectory);

            UICodeGenerator.Generate(uiAssetsRoot, "_fui.bytes", new ScribanCodeGenerator(GetLogicCodeExportSettings), filter);

            #endregion

            // 重新获取绑定文件列表 将不存在的绑定文件对应的逻辑文件删除
            var newBindingFiles = Directory.GetFiles(setting.UIBindingCodeDirectory, "*.cs", SearchOption.AllDirectories);
            foreach (var bindingFile in bindingFiles)
            {
                if (newBindingFiles.Contains(bindingFile))
                    continue;

                var logicFile = bindingFile.Replace(setting.UIBindingCodeDirectory, setting.UILogicCodeDirectory);
                if (System.IO.File.Exists(logicFile))
                    System.IO.File.Delete(logicFile);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        private sealed class UIComponentFilter : IUIComponentFilter
        {
            private readonly string m_Namespace;

            public UIComponentFilter(string ns)
            {
                m_Namespace = ns;
            }

            public string Filter(UIComponent component)
            {
                var exportType = GetExportType(component);
                if (exportType == UIComponentExportType.None)
                    return component.ExtensionType.FullName;

                return m_Namespace + "." + component.PackageName + "." + component.Name;
            }
        }

        private static bool GetLogicCodeExportSettings(UIComponent component, out string templatePath, out string outputPath)
        {
            templatePath = string.Empty;
            outputPath = s_Setting.UILogicCodeDirectory + "/" + component.PackageName + "/" + component.Name + ".cs";

            // 如果已经存在，则不再生成
            if (System.IO.File.Exists(outputPath))
                return false;

            var exportType = GetExportType(component);
            switch (exportType)
            {
                case UIComponentExportType.None:
                    return false;
                case UIComponentExportType.UIForm:
                    templatePath = s_Setting.ScribanTemplateDirectory + "/UIForm.tpl";
                    return true;
                case UIComponentExportType.UIComponent:
                    templatePath = s_Setting.ScribanTemplateDirectory + "/UIComponent.tpl";
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static bool GetBindingCodeExportSettings(UIComponent component, out string templatePath, out string outputPath)
        {
            templatePath = string.Empty;
            outputPath = s_Setting.UIBindingCodeDirectory + "/" + component.PackageName + "/" + component.Name + ".cs";

            var exportType = GetExportType(component);
            switch (exportType)
            {
                case UIComponentExportType.None:
                    return false;
                case UIComponentExportType.UIForm:
                    templatePath = s_Setting.ScribanTemplateDirectory + "/UIForm.Binding.tpl";
                    return true;
                case UIComponentExportType.UIComponent:
                    templatePath = s_Setting.ScribanTemplateDirectory + "/UIComponent.Binding.tpl";
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static UIComponentExportType GetExportType(UIComponent component)
        {
            // 以Form结尾的组件导出为UIForm
            if (component.Name.EndsWith("Form"))
                return UIComponentExportType.UIForm;

            // 以Component结尾的组件导出为UIComponent
            if (component.Name.EndsWith("Component"))
                return UIComponentExportType.UIComponent;

            // 当子节点中存在任意非拓展组件类型支持的自定义名字时 导出为UIComponent
            var extendTypeSupportNames = ExtendTypeSupportNames[component.ExtensionType];

            bool ExistsNotSupportName(UIComponentNode child)
            {
                return !extendTypeSupportNames.Contains(child.Name) && !Regex.IsMatch(child.Name, @"^n[0-9]+$");
            }

            if (component.Nodes.Any(ExistsNotSupportName))
                return UIComponentExportType.UIComponent;

            return UIComponentExportType.None;
        }

        private enum UIComponentExportType
        {
            None,
            UIForm,
            UIComponent,
        }

        private static readonly Dictionary<System.Type, List<string>> ExtendTypeSupportNames = new Dictionary<System.Type, List<string>>
        {
            { typeof(GButton), new List<string> { "icon", "title" } },
            { typeof(GLabel), new List<string> { "icon", "title" } },
            { typeof(GComboBox), new List<string> { "icon", "title" } },
            { typeof(GProgressBar), new List<string> { "title", "bar", "bar_v", "ani" } },
            { typeof(GSlider), new List<string> { "title", "bar", "bar_v", "grip" } },
            { typeof(GScrollBar), new List<string> { "grip", "bar", "arrow1", "arrow2" } },
            { typeof(GComponent), new List<string>() }
        };
    }
}