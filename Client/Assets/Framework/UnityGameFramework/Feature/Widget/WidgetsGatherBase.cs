namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 挂件收集器静态绑定器抽象类。
    /// </summary>
    public abstract class WidgetsGatherBase
    {
        /// <summary>
        /// 初始化静态绑定器。
        /// </summary>
        /// <param name="gather">组件收集器实例。</param>
        protected internal abstract void Awake(WidgetsGather gather);
    }
}