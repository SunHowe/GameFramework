//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework
{
    public static partial class Utility
    {
        /// <summary>
        /// 程序集相关的实用函数。
        /// </summary>
        public static class Assembly
        {
            private static readonly Dictionary<string, Type> s_CachedTypes = new Dictionary<string, Type>(StringComparer.Ordinal);
            private static readonly Dictionary<string, System.Reflection.Assembly> s_AssemblyDict;
            private static readonly Dictionary<string, System.Reflection.Assembly> s_HotfixAssemblyDict;

            static Assembly()
            {
                s_AssemblyDict = AppDomain.CurrentDomain.GetAssemblies().ToDictionary(assembly => assembly.FullName, StringComparer.Ordinal);
                s_HotfixAssemblyDict = new Dictionary<string, System.Reflection.Assembly>(StringComparer.Ordinal);
            }

            /// <summary>
            /// 清空缓存。
            /// </summary>
            public static void ClearCache()
            {
                s_CachedTypes.Clear();
            }

            /// <summary>
            /// 清空热更新程序集。
            /// </summary>
            public static void ClearHotfixAssemblies()
            {
                s_HotfixAssemblyDict.Clear();
            }

            /// <summary>
            /// 添加热更新程序集实例 用于动态加载的程序集
            /// </summary>
            /// <param name="assembly">程序集实例</param>
            public static void AddHotfix(System.Reflection.Assembly assembly)
            {
                s_HotfixAssemblyDict[assembly.FullName] = assembly;
            }

            /// <summary>
            /// 获取所有程序集。
            /// </summary>
            public static System.Reflection.Assembly[] GetAssemblies()
            {
                var results = new System.Reflection.Assembly[s_AssemblyDict.Count + s_HotfixAssemblyDict.Count];
                s_AssemblyDict.Values.CopyTo(results, 0);
                s_HotfixAssemblyDict.Values.CopyTo(results, s_AssemblyDict.Count);
                return results;
            }

            /// <summary>
            /// 获取已加载的程序集中的所有类型。
            /// </summary>
            /// <returns>已加载的程序集中的所有类型。</returns>
            public static Type[] GetTypes()
            {
                List<Type> results = new List<Type>();
                foreach (System.Reflection.Assembly assembly in s_AssemblyDict.Values)
                {
                    results.AddRange(assembly.GetTypes());
                }

                return results.ToArray();
            }

            /// <summary>
            /// 获取已加载的程序集中的所有类型。
            /// </summary>
            /// <param name="results">已加载的程序集中的所有类型。</param>
            public static void GetTypes(List<Type> results)
            {
                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (System.Reflection.Assembly assembly in s_AssemblyDict.Values)
                {
                    results.AddRange(assembly.GetTypes());
                }
            }

            /// <summary>
            /// 获取已加载的程序集中的指定类型。
            /// </summary>
            /// <param name="assemblyName">程序集名</param>
            /// <param name="typeName">要获取的类型名。</param>
            /// <returns>已加载的程序集中的指定类型。</returns>
            public static Type GetType(string assemblyName, string typeName)
            {
                if (string.IsNullOrEmpty(assemblyName))
                {
                    return GetType(typeName);
                }
                
                if (string.IsNullOrEmpty(typeName))
                {
                    throw new GameFrameworkException("Type name is invalid.");
                }

                Type type = null;
                if (s_CachedTypes.TryGetValue(typeName, out type))
                {
                    return type;
                }

                if (!s_AssemblyDict.TryGetValue(assemblyName, out var assembly))
                {
                    return null;
                }
                
                type = Type.GetType(Text.Format("{0}, {1}", typeName, assembly.FullName));
                if (type != null)
                {
                    s_CachedTypes.Add(typeName, type);
                }
                
                return type;
            }

            /// <summary>
            /// 获取已加载的程序集中的指定类型。
            /// </summary>
            /// <param name="typeName">要获取的类型名。</param>
            /// <returns>已加载的程序集中的指定类型。</returns>
            public static Type GetType(string typeName)
            {
                if (string.IsNullOrEmpty(typeName))
                {
                    throw new GameFrameworkException("Type name is invalid.");
                }

                Type type = null;
                if (s_CachedTypes.TryGetValue(typeName, out type))
                {
                    return type;
                }

                type = Type.GetType(typeName);
                if (type != null)
                {
                    s_CachedTypes.Add(typeName, type);
                    return type;
                }

                foreach (System.Reflection.Assembly assembly in s_AssemblyDict.Values)
                {
                    type = Type.GetType(Text.Format("{0}, {1}", typeName, assembly.FullName));
                    if (type != null)
                    {
                        s_CachedTypes.Add(typeName, type);
                        return type;
                    }
                }

                return null;
            }

            /// <summary>
            /// 获取热更新程序集列表。
            /// </summary>
            public static System.Reflection.Assembly[] GetHotfixAssemblies()
            {
                return s_HotfixAssemblyDict.Values.ToArray();
            }

            /// <summary>
            /// 获取已加载的热更新程序集中的所有类型。
            /// </summary>
            /// <returns>已加载的热更新程序集中的所有类型。</returns>
            public static Type[] GetHotfixTypes()
            {
                List<Type> results = new List<Type>();
                foreach (System.Reflection.Assembly assembly in s_HotfixAssemblyDict.Values)
                {
                    results.AddRange(assembly.GetTypes());
                }

                return results.ToArray();
            }

            /// <summary>
            /// 获取已加载的热更新程序集中的所有类型。
            /// </summary>
            /// <param name="results">已加载的热更新程序集中的所有类型。</param>
            public static void GetHotfixTypes(List<Type> results)
            {
                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (System.Reflection.Assembly assembly in s_HotfixAssemblyDict.Values)
                {
                    results.AddRange(assembly.GetTypes());
                }
            }

            /// <summary>
            /// 获取已加载的热更新程序集中的指定类型。
            /// </summary>
            /// <param name="assemblyName">程序集名</param>
            /// <param name="typeName">要获取的类型名。</param>
            /// <returns>已加载的热更新程序集中的指定类型。</returns>
            public static Type GetHotfixType(string assemblyName, string typeName)
            {
                if (string.IsNullOrEmpty(assemblyName))
                {
                    return GetType(typeName);
                }
                
                if (string.IsNullOrEmpty(typeName))
                {
                    throw new GameFrameworkException("Type name is invalid.");
                }

                Type type = null;
                if (s_CachedTypes.TryGetValue(typeName, out type))
                {
                    return type;
                }

                if (!s_HotfixAssemblyDict.TryGetValue(assemblyName, out var assembly))
                {
                    return null;
                }
                
                type = Type.GetType(Text.Format("{0}, {1}", typeName, assembly.FullName));
                if (type != null)
                {
                    s_CachedTypes.Add(typeName, type);
                }
                
                return type;
            }

            /// <summary>
            /// 获取已加载的热更新程序集中的指定类型。
            /// </summary>
            /// <param name="typeName">要获取的类型名。</param>
            /// <returns>已加载的热更新程序集中的指定类型。</returns>
            public static Type GetHotfixType(string typeName)
            {
                if (string.IsNullOrEmpty(typeName))
                {
                    throw new GameFrameworkException("Type name is invalid.");
                }

                Type type = null;
                if (s_CachedTypes.TryGetValue(typeName, out type))
                {
                    return type;
                }

                type = Type.GetType(typeName);
                if (type != null)
                {
                    s_CachedTypes.Add(typeName, type);
                    return type;
                }

                foreach (System.Reflection.Assembly assembly in s_HotfixAssemblyDict.Values)
                {
                    type = Type.GetType(Text.Format("{0}, {1}", typeName, assembly.FullName));
                    if (type != null)
                    {
                        s_CachedTypes.Add(typeName, type);
                        return type;
                    }
                }

                return null;
            }
        }
    }
}
