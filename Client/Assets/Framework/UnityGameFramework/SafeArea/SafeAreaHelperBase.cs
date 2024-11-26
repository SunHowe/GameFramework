using System;
using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 安全区域辅助工具基类。
    /// </summary>
    public abstract class SafeAreaHelperBase : MonoBehaviour, ISafeAreaHelper
    {
        /// <summary>
        /// 安全区域范围变化事件。
        /// </summary>
        public event EventHandler<SafeAreaChangeEventArgs> OnSafeAreaChange;
        
        /// <summary>
        /// 安全区域范围。
        /// </summary>
        public Rect SafeArea { get; private set; }

        protected void SetSafeArea(Rect rect)
        {
            if (SafeArea == rect)
            {
                return;
            }
            
            SafeArea = rect;
            
            if (OnSafeAreaChange == null)
            {
                return;
            }

            var eventArgs = SafeAreaChangeEventArgs.Create(rect);
            OnSafeAreaChange.Invoke(this, eventArgs);
            ReferencePool.Release(eventArgs);
        }
    }
}