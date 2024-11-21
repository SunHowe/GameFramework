using GameFramework;
using GameFramework.Event;

namespace Framework.UnityGameFramework
{
    /// <summary>
    /// 热更新程序集加载失败事件。
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
        /// 程序集名
        /// </summary>
        public string AssemblyName { get; set; }
        
        /// <summary>
        /// 程序集版本号
        /// </summary>
        public string AssemblyVersion { get; set; }

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
        public static HotfixLoadFailureEventArgs Create(string assemblyName)
        {
            var eventArgs = ReferencePool.Acquire<HotfixLoadFailureEventArgs>();
            
            eventArgs.AssemblyName = assemblyName;
            
            return eventArgs;
        }

        /// <summary>
        /// 清理事件。
        /// </summary>
        public override void Clear()
        {
            AssemblyName = string.Empty;
        }
    }
}