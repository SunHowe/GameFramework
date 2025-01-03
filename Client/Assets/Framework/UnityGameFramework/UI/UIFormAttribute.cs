﻿using System;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 界面标签，用于标注界面逻辑，以进行界面与逻辑类型的绑定以及提供通过反射方便打开指定界面的功能。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class UIFormAttribute : Attribute
    {
        /// <summary>
        /// 界面资源名称。
        /// </summary>
        public string UIFormAssetName { get; }
        
        /// <summary>
        /// 界面分组名称。
        /// </summary>
        public string UIGroupName { get; }

        /// <summary>
        /// 是否暂停被覆盖的界面。
        /// </summary>
        public bool PauseCoveredUIForm { get; }

        /// <summary>
        /// 是否允许多界面实例。若不允许多界面实例，则会重新激活之前已打开的界面。
        /// </summary>
        public bool AllowMultiple { get; }

        /// <summary>
        /// 加载界面资源的优先级。
        /// </summary>
        public int Priority { get; }

        public UIFormAttribute(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm = false, bool allowMultiple = false, int priority = 0)
        {
            UIFormAssetName = uiFormAssetName;
            UIGroupName = uiGroupName;
            PauseCoveredUIForm = pauseCoveredUIForm;
            AllowMultiple = allowMultiple;
            Priority = priority;
        }
    }
}