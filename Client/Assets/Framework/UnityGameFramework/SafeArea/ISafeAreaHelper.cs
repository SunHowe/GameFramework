using System;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 屏幕安全区域数据辅助工具。
    /// </summary>
    public interface ISafeAreaHelper
    {
        /// <summary>
        /// 安全区域范围变化事件。
        /// </summary>
        event EventHandler<SafeAreaChangeEventArgs> OnSafeAreaChange;
        
        /// <summary>
        /// 安全区域范围。
        /// </summary>
        Rect SafeArea { get; }
    }
}