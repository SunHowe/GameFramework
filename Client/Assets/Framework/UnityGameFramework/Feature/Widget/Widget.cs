using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 挂件组件。挂载该组件的节点可以被挂件收集器面板的按钮自动收集，并允许指定名字和组件类型。
    /// </summary>
    public sealed class Widget : MonoBehaviour
    {
        /// <summary>
        /// 挂件名字。
        /// </summary>
        public string WidgetName;
        
        /// <summary>
        /// 挂件类型名。
        /// </summary>
        public string WidgetTypeName;
    }
}