namespace GameFramework.Resource
{
    /// <summary>
    /// 资源模式。
    /// </summary>
    public enum ResourceMode
    {
        /// <summary>
        /// 未指定。
        /// </summary>
        Unspecified = 0,
        
        /// <summary>
        /// 单机模式（不可更新）
        /// </summary>
        Package,
        
        /// <summary>
        /// 可更新（预下载模式）。
        /// </summary>
        Updatable,
        
        /// <summary>
        /// 可更新（边玩边下模式）。
        /// </summary>
        UpdatableWhilePlaying,
    }
}