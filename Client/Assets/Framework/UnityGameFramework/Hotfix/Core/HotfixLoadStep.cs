namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 热更新加载阶段定义。
    /// </summary>
    public enum HotfixLoadStep
    {
        /// <summary>
        /// 未加载热更新
        /// </summary>
        None,
        
        /// <summary>
        /// 加载热更新配置
        /// </summary>
        LoadConfig,
        
        /// <summary>
        /// 加载热更新程序集文件
        /// </summary>
        LoadAssemblyFile,
        
        /// <summary>
        /// 加载热更新程序集
        /// </summary>
        LoadAssembly,
        
        /// <summary>
        /// 启动热更新逻辑
        /// </summary>
        LaunchHotfixEntry,
        
        /// <summary>
        /// 热更新加载完成
        /// </summary>
        Complete,
    }
}