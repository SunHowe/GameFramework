namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 对象收集器静态绑定器抽象类。
    /// </summary>
    public abstract class ObjectCollectorBase
    {
        /// <summary>
        /// 初始化静态绑定器。
        /// </summary>
        /// <param name="collector">组件收集器实例。</param>
        protected internal abstract void Awake(ObjectCollector collector);
    }
}