using GameFramework;
using GameFramework.Event;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 热更新加载阶段变更事件。
    /// </summary>
    public sealed class HotfixLoadStepChangedEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件编号。
        /// </summary>
        public static readonly int EventId = typeof(HotfixLoadStepChangedEventArgs).GetHashCode();

        /// <summary>
        /// 获取事件编号。
        /// </summary>
        public override int Id => EventId;
        
        /// <summary>
        /// 当前加载阶段
        /// </summary>
        public HotfixLoadStep LoadStep { get; set; }

        /// <summary>
        /// 初始化事件新实例。
        /// </summary>
        public HotfixLoadStepChangedEventArgs()
        {
            LoadStep = HotfixLoadStep.None;
        }

        /// <summary>
        /// 创建事件。
        /// </summary>
        public static HotfixLoadStepChangedEventArgs Create(HotfixLoadStep step)
        {
            var eventArgs = ReferencePool.Acquire<HotfixLoadStepChangedEventArgs>();
            eventArgs.LoadStep = step;
            return eventArgs;
        }

        /// <summary>
        /// 清理事件。
        /// </summary>
        public override void Clear()
        {
            LoadStep = HotfixLoadStep.None;
        }
    }
}