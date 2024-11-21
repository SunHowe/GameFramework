using GameFramework;
using GameFramework.Event;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 热更新加载完成事件。
    /// </summary>
    public sealed class HotfixLoadCompleteEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件编号。
        /// </summary>
        public static readonly int EventId = typeof(HotfixLoadCompleteEventArgs).GetHashCode();

        /// <summary>
        /// 获取事件编号。
        /// </summary>
        public override int Id => EventId;

        /// <summary>
        /// 初始化事件新实例。
        /// </summary>
        public HotfixLoadCompleteEventArgs()
        {
        }

        /// <summary>
        /// 创建事件。
        /// </summary>
        public static HotfixLoadCompleteEventArgs Create()
        {
            var eventArgs = ReferencePool.Acquire<HotfixLoadCompleteEventArgs>();

            return eventArgs;
        }

        /// <summary>
        /// 清理事件。
        /// </summary>
        public override void Clear()
        {
        }
    }
}