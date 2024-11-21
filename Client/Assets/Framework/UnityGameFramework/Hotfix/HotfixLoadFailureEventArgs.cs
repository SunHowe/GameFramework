using GameFramework;
using GameFramework.Event;

namespace Framework.UnityGameFramework
{
    /// <summary>
    /// 事件。
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
        /// 初始化事件新实例。
        /// </summary>
        public HotfixLoadFailureEventArgs()
        {
        }

        /// <summary>
        /// 创建事件。
        /// </summary>
        public static HotfixLoadFailureEventArgs Create()
        {
            return ReferencePool.Acquire<HotfixLoadFailureEventArgs>();
        }

        /// <summary>
        /// 清理事件。
        /// </summary>
        public override void Clear()
        {
        }
    }
}