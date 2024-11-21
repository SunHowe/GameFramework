using System.Reflection;
using GameFramework;
using GameFramework.Event;

namespace Framework.UnityGameFramework
{
    /// <summary>
    /// 热更新程序集加载成功事件。
    /// </summary>
    public sealed class HotfixLoadSuccessEventArgs : GameEventArgs
    {
        /// <summary>
        /// 事件编号。
        /// </summary>
        public static readonly int EventId = typeof(HotfixLoadSuccessEventArgs).GetHashCode();

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
        /// 程序集实例
        /// </summary>
        public Assembly Assembly { get; set; }
        
        /// <summary>
        /// 已加载成功数量
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 初始化事件新实例。
        /// </summary>
        public HotfixLoadSuccessEventArgs()
        {
            Clear();
        }

        /// <summary>
        /// 创建事件。
        /// </summary>
        public static HotfixLoadSuccessEventArgs Create(string assemblyName, string assemblyVersion, Assembly assembly, int successCount, int totalCount)
        {
            var eventArgs = ReferencePool.Acquire<HotfixLoadSuccessEventArgs>();
            
            eventArgs.AssemblyName = assemblyName;
            eventArgs.AssemblyVersion = assemblyVersion;
            eventArgs.Assembly = assembly;
            eventArgs.SuccessCount = successCount;
            eventArgs.TotalCount = totalCount;

            return eventArgs;
        }

        /// <summary>
        /// 清理事件。
        /// </summary>
        public override void Clear()
        {
            AssemblyName = null;
            AssemblyVersion = null;
            Assembly = null;
            SuccessCount = 0;
            TotalCount = 0;
        }
    }
}