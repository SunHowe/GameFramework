using GameFramework;
using GameFramework.Event;

namespace GameMono
{
    /// <summary>
    /// 启动流程完成事件。
    /// </summary>
    public sealed class LaunchFinishEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件编号。
        /// </summary>
        public static readonly int EventId = typeof(LaunchFinishEventArgs).GetHashCode();

        /// <summary>
        /// 获取事件编号。
        /// </summary>
        public override int Id => EventId;

        /// <summary>
        /// 初始化事件新实例。
        /// </summary>
        public LaunchFinishEventArgs()
        {
        }

        /// <summary>
        /// 创建事件。
        /// </summary>
        public static LaunchFinishEventArgs Create()
        {
            var eventArgs = ReferencePool.Acquire<LaunchFinishEventArgs>();

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