using System;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// UI界面绑定信息。
    /// </summary>
    public class UIFormBindingInfo
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

        /// <summary>
        /// 界面逻辑类型
        /// </summary>
        public Type FormLogicType { get; }

        public UIFormBindingInfo(UIFormAttribute attribute, Type formLogicType)
        {
            UIFormAssetName = attribute.UIFormAssetName;
            UIGroupName = attribute.UIGroupName;
            PauseCoveredUIForm = attribute.PauseCoveredUIForm;
            AllowMultiple = attribute.AllowMultiple;
            Priority = attribute.Priority;
            FormLogicType = formLogicType;
        }
    }
}