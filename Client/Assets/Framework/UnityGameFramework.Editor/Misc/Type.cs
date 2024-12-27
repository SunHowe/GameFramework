//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using GameFramework;
using System.Collections.Generic;
using System.Reflection;

namespace UnityGameFramework.Editor
{
    /// <summary>
    /// 类型相关的实用函数。
    /// </summary>
    internal static class Type
    {
        public enum AssemblyType
        {
            /// <summary>
            /// 不可热更的运行时程序集。
            /// </summary>
            Runtime,

            /// <summary>
            /// 可热更的运行时程序集。
            /// </summary>
            Hotfix,

            /// <summary>
            /// 编辑器程序集。
            /// </summary>
            Editor,
        }

        private static readonly Dictionary<AssemblyType, string[]> AssemblyNames = new Dictionary<AssemblyType, string[]>()
        {
            [AssemblyType.Runtime] = new string[]
            {
                "UnityGameFramework.Runtime",
                "GameMono",
            },
            [AssemblyType.Hotfix] = new string[]
            {
                "GameLogic",
            },
            [AssemblyType.Editor] = new string[]
            {
                "UnityGameFramework.Editor",
            },
        };

        /// <summary>
        /// 获取指定类型的程序集实例列表。
        /// </summary>
        internal static Assembly[] GetAssemblies(params AssemblyType[] assemblyTypes)
        {
            var assemblies = new List<Assembly>();

            foreach (var assemblyType in assemblyTypes)
            {
                var assemblyNames = AssemblyNames[assemblyType];
                foreach (var assemblyName in assemblyNames)
                {
                    var assembly = Assembly.Load(assemblyName);
                    if (assembly != null)
                    {
                        assemblies.Add(assembly);
                    }
                }
            }
            
            return assemblies.ToArray();
        }

        /// <summary>
        /// 获取配置路径。
        /// </summary>
        /// <typeparam name="T">配置类型。</typeparam>
        /// <returns>配置路径。</returns>
        internal static string GetConfigurationPath<T>() where T : ConfigPathAttribute
        {
            foreach (System.Type type in Utility.Assembly.GetTypes())
            {
                if (!type.IsAbstract || !type.IsSealed)
                {
                    continue;
                }

                foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
                {
                    if (fieldInfo.FieldType == typeof(string) && fieldInfo.IsDefined(typeof(T), false))
                    {
                        return (string)fieldInfo.GetValue(null);
                    }
                }

                foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
                {
                    if (propertyInfo.PropertyType == typeof(string) && propertyInfo.IsDefined(typeof(T), false))
                    {
                        return (string)propertyInfo.GetValue(null, null);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 在运行时程序集中获取指定基类的所有子类的名称。
        /// </summary>
        /// <param name="typeBase">基类类型。</param>
        /// <returns>指定基类的所有子类的名称。</returns>
        internal static string[] GetRuntimeTypeNames(System.Type typeBase)
        {
            return GetTypeNames(typeBase, AssemblyType.Runtime);
        }

        /// <summary>
        /// 在运行时或编辑器程序集中获取指定基类的所有子类的名称。
        /// </summary>
        /// <param name="typeBase">基类类型。</param>
        /// <returns>指定基类的所有子类的名称。</returns>
        internal static string[] GetRuntimeOrEditorTypeNames(System.Type typeBase)
        {
            return GetTypeNames(typeBase, AssemblyType.Runtime, AssemblyType.Editor);
        }

        /// <summary>
        /// 在运行时程序集中获取指定基类的所有子类。
        /// </summary>
        /// <param name="typeBase">基类类型。</param>
        /// <param name="assemblyTypes">程序集类型。</param>
        /// <returns>指定基类的所有子类。</returns>
        public static System.Type[] GetTypes(System.Type typeBase, params AssemblyType[] assemblyTypes)
        {
            List<System.Type> matchTypes = new List<System.Type>();

            foreach (var assemblyType in assemblyTypes)
            {
                foreach (var assemblyName in AssemblyNames[assemblyType])
                {
                    Assembly assembly = null;
                    try
                    {
                        assembly = Assembly.Load(assemblyName);
                    }
                    catch
                    {
                        continue;
                    }

                    if (assembly == null)
                    {
                        continue;
                    }

                    System.Type[] types = assembly.GetTypes();
                    foreach (System.Type type in types)
                    {
                        if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                        {
                            matchTypes.Add(type);
                        }
                    }
                }
            }

            matchTypes.Sort((t1, t2) => string.Compare(t1.FullName, t2.FullName, StringComparison.Ordinal));
            return matchTypes.ToArray();
        }

        private static string[] GetTypeNames(System.Type typeBase, params AssemblyType[] assemblyTypes)
        {
            List<string> typeNames = new List<string>();
            
            foreach (var assemblyType in assemblyTypes)
            {
                foreach (var assemblyName in AssemblyNames[assemblyType])
                {
                    Assembly assembly = null;
                    try
                    {
                        assembly = Assembly.Load(assemblyName);
                    }
                    catch
                    {
                        continue;
                    }

                    if (assembly == null)
                    {
                        continue;
                    }

                    System.Type[] types = assembly.GetTypes();
                    foreach (System.Type type in types)
                    {
                        if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                        {
                            typeNames.Add(type.FullName);
                        }
                    }
                }
            }

            typeNames.Sort();
            return typeNames.ToArray();
        }
    }
}