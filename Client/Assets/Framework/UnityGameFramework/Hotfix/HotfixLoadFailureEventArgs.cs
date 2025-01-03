﻿using GameFramework;
using GameFramework.Event;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 热更新加载失败事件。
    /// </summary>
    public sealed class HotfixLoadFailureEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件编号。
        /// </summary>
        public static readonly int EventId = typeof(HotfixLoadFailureEventArgs).GetHashCode();

        /// <summary>
        /// 获取事件编号。
        /// </summary>
        public override int Id => EventId;

        /// <summary>
        /// 发生失败的阶段
        /// </summary>
        public HotfixLoadStep LoadStep { get; set; }
        
        /// <summary>
        /// 错误信息描述
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 初始化事件新实例。
        /// </summary>
        public HotfixLoadFailureEventArgs()
        {
            Clear();
        }

        /// <summary>
        /// 创建事件。
        /// </summary>
        public static HotfixLoadFailureEventArgs Create(HotfixLoadStep step, string errorMessage)
        {
            var eventArgs = ReferencePool.Acquire<HotfixLoadFailureEventArgs>();
            eventArgs.LoadStep = step;
            eventArgs.ErrorMessage = errorMessage;
            return eventArgs;
        }

        /// <summary>
        /// 清理事件。
        /// </summary>
        public override void Clear()
        {
            LoadStep = HotfixLoadStep.None;
            ErrorMessage = null;
        }
    }
}