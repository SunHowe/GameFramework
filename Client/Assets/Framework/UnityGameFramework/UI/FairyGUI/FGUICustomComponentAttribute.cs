using System;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// 用于标注FairyGUI自定义组件类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class FGUICustomComponentAttribute : Attribute
    {
        /// <summary>
        /// 绑定的URL。
        /// </summary>
        public string URL { get; }

        public FGUICustomComponentAttribute(string url)
        {
            URL = url;
        }
    }
}