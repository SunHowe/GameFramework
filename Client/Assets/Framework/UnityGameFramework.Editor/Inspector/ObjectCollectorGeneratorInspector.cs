﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Scriban;
using Scriban.Runtime;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace UnityGameFramework.Editor
{
    [CustomEditor(typeof(ObjectCollectorGenerator))]
    internal sealed class ObjectCollectorGeneratorInspector : GameFrameworkInspector
    {
        private const string ASSEMBLY_NAME = "GameLogic";
        private const string DEFAULT_NAMESPACE = "GameLogic";
        private const string SCRIBAN_FILE_PATH = "../Scriban/GameLogic/ObjectCollector.tpl";
        private const string GAME_LOGIC_DIRECTORY_PATH = "Assets/GameMain/Scripts/GameLogic";

        public static void SetMissingDefaultValue(ObjectCollectorGenerator t)
        {
            t.AssemblyName = Assembly.Load(ASSEMBLY_NAME).FullName; // 强制设置程序集名。

            if (string.IsNullOrEmpty(t.TypeName))
            {
                // 默认使用GameObject的名字进行生成
                t.TypeName = $"{t.gameObject.name.ToUpperCamelCase()}Objects";
            }

            if (string.IsNullOrEmpty(t.Namespace))
            {
                t.Namespace = DEFAULT_NAMESPACE;
            }
        }

        /// <summary>
        /// 生成绑定代码。
        /// </summary>
        public static void GenerateCode(ObjectCollectorGenerator t)
        {
            var objectCollector = t.GetComponent<ObjectCollector>();
            if (objectCollector == null)
            {
                return;
            }

            if (objectCollector.ObjectList == null || objectCollector.ObjectNameList == null)
            {
                return;
            }

            var count = Mathf.Min(objectCollector.ObjectList.Count, objectCollector.ObjectNameList.Count);
            if (count <= 0)
            {
                return;
            }

            var nodes = new List<Dictionary<string, string>>();
            for (var i = 0; i < count; i++)
            {
                var objectName = objectCollector.ObjectNameList[i];
                var objectInstance = objectCollector.ObjectList[i];

                var node = new Dictionary<string, string>();
                node["name"] = objectName;
                node["field_name"] = objectName.ToUpperCamelCase();
                node["type_full_name"] = objectInstance.GetType().FullName;
                
                nodes.Add(node);
            }

            // 使用Scriban进行创建代码。
            var scriptObject = new ScriptObject();
            scriptObject.Import(new Dictionary<string, object>
            {
                ["nodes"] = nodes,
                ["name"] = t.TypeName,
                ["namespace"] = t.Namespace,
            });

            var context = new TemplateContext();
            context.PushGlobal(scriptObject);

            var template = Template.Parse(File.ReadAllText(Path.GetFullPath(SCRIBAN_FILE_PATH)));
            var output = template.Render(context);

            var outputPath = GetOutputFilePath(t);
            var directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(outputPath, output, Encoding.UTF8);
            
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 删除绑定代码。
        /// </summary>
        public static void DeleteCode(ObjectCollectorGenerator t)
        {
            var filePath = GetOutputFilePath(t);
            if (!File.Exists(filePath))
            {
                return;
            }
            
            File.Delete(filePath);
            AssetDatabase.Refresh();
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            var t = (ObjectCollectorGenerator)target;
            var isModify = false;

            string filePath;

            using (GameFrameworkEditorGUIUtility.MakeDisabledGroupScope(EditorApplication.isPlayingOrWillChangePlaymode))
            {
                using var _ = GameFrameworkEditorGUIUtility.MakeVerticalScope();

                using (GameFrameworkEditorGUIUtility.MakeVerticalScope("box"))
                {
                    EditorGUILayout.LabelField("程序集", t.AssemblyName);

                    var typeName = EditorGUILayout.TextField("类名", t.TypeName);
                    if (typeName != t.TypeName)
                    {
                        t.TypeName = typeName;
                        isModify = true;
                    }
                    
                    var namespaceName = EditorGUILayout.TextField("命名空间", t.Namespace);
                    if (namespaceName != t.Namespace)
                    {
                        t.Namespace = namespaceName;
                        isModify = true;
                    }

                    var generateDirectoryName = EditorGUILayout.TextField("生成目录", t.GenerateDirectoryName);
                    if (generateDirectoryName != t.GenerateDirectoryName)
                    {
                        t.GenerateDirectoryName = generateDirectoryName;
                        isModify = true;
                    }

                    filePath = GetOutputFilePath(t);
                    EditorGUILayout.HelpBox($"生成路径是基于GameLogic目录的相对路径。\n 目前生成文件路径:\n{filePath}", MessageType.Info);
                }

                using (GameFrameworkEditorGUIUtility.MakeVerticalScope("box"))
                {
                    if (GUILayout.Button("生成代码"))
                    {
                        ObjectCollectorGeneratorInspector.GenerateCode(t);
                    }

                    if (File.Exists(filePath))
                    {
                        if (GUILayout.Button("打开代码"))
                        {
                            // 使用默认的C#编辑器打开文件
                            AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath(filePath, typeof(TextAsset)));
                        }

                        if (GUILayout.Button("删除代码"))
                        {
                            ObjectCollectorGeneratorInspector.DeleteCode(t);
                        }
                    }
                }
            }

            if (isModify)
            {
                EditorUtility.SetDirty(t);
            }

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }

        private static string GetOutputFilePath(ObjectCollectorGenerator t)
        {
            if (string.IsNullOrEmpty(t.GenerateDirectoryName))
            {
                return Path.Combine(GAME_LOGIC_DIRECTORY_PATH, t.TypeName + ".cs");
            }
            
            return Path.Combine(GAME_LOGIC_DIRECTORY_PATH, t.GenerateDirectoryName, t.TypeName + ".cs");
        }
    }
}