using GameFramework;
using GameFramework.Event;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 热更新加载程序集失败事件。
    /// </summary>
    public sealed class HotfixLoadAssemblyFailureEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件编号。
        /// </summary>
        public static readonly int EventId = typeof(HotfixLoadAssemblyFailureEventArgs).GetHashCode();

        /// <summary>
        /// 获取事件编号。
        /// </summary>
        public override int Id => EventId;
        
        /// <summary>
        /// 程序集名字
        /// </summary>
        public string AssemblyName { get; set; }
        
        /// <summary>
        /// 程序集版本
        /// </summary>
        public string AssemblyVersion { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 初始化事件新实例。
        /// </summary>
        public HotfixLoadAssemblyFailureEventArgs()
        {
            AssemblyName = string.Empty;
            AssemblyVersion = string.Empty;
            ErrorMessage = string.Empty;
        }

        /// <summary>
        /// 创建事件。
        /// </summary>
        public static HotfixLoadAssemblyFailureEventArgs Create(string assemblyName, string assemblyVersion, string errorMessage)
        {
            var eventArgs = ReferencePool.Acquire<HotfixLoadAssemblyFailureEventArgs>();
            eventArgs.AssemblyName = assemblyName;
            eventArgs.AssemblyVersion = assemblyVersion;
            eventArgs.ErrorMessage = errorMessage;
            return eventArgs;
        }

        /// <summary>
        /// 清理事件。
        /// </summary>
        public override void Clear()
        {
            AssemblyName = string.Empty;
            AssemblyVersion = string.Empty;
            ErrorMessage = string.Empty;
        }
    }
}