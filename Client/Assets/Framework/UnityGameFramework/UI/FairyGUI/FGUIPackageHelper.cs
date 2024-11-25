using System;
using System.Collections.Generic;
using FairyGUI.Dynamic;
using UnityEngine;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// 默认的FairyGUI包辅助工具。
    /// </summary>
    public sealed class FGUIPackageHelper : MonoBehaviour, IUIPackageHelper
    {
        /// <summary>
        /// 启动所用的UI资源包名列表 与PackageIds一一对应。
        /// </summary>
        private string[] m_PackageNames;

        /// <summary>
        /// 启动所用的UI资源包ID列表 与PackageNames一一对应。
        /// </summary> 
        private string[] m_PackageIds;

        /// <summary>
        /// 包id到包名的映射字典。
        /// </summary>
        private readonly Dictionary<string, string> m_PackageIdToPackageNameDict = new();

        private void Awake()
        {
            // 启动时添加默认的映射配置
            AddPackageMapping(m_PackageNames, m_PackageIds);
        }

        /// <summary>
        /// 通过包id获取包名。
        /// </summary>
        public string GetPackageNameById(string id)
        {
            return m_PackageIdToPackageNameDict.TryGetValue(id, out var packageName) ? packageName : string.Empty;
        }

        /// <summary>
        /// 添加包映射关系。
        /// </summary>
        public void AddPackageMapping(string[] packageNames, string[] packageIds)
        {
            if (packageNames == null || packageIds == null)
            {
                return;
            }

            var count = Math.Min(packageNames.Length, packageIds.Length);

            for (var i = 0; i < count; i++)
            {
                m_PackageIdToPackageNameDict.Add(packageIds[i], packageNames[i]);
            }
        }

        /// <summary>
        /// 添加包映射关系。
        /// </summary>
        public void AddPackageMapping(UIPackageMapping mapping)
        {
            AddPackageMapping(mapping.PackageNames, mapping.PackageIds);
        }

        /// <summary>
        /// 清理映射关系。
        /// </summary>
        public void ClearPackageMapping()
        {
            m_PackageIdToPackageNameDict.Clear();

            // 清空后添加默认的映射配置
            AddPackageMapping(m_PackageNames, m_PackageIds);
        }
    }
}