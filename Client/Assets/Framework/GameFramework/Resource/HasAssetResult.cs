namespace GameFramework.Resource
{
    /// <summary>
    /// 判断是否有资源的返回结果定义。
    /// </summary>
    public enum HasAssetResult
    {
        /// <summary>
        /// 当前已加载的资源包中不存在该资源。
        /// </summary>
        NotExist,
        
        /// <summary>
        /// 未下载该资源。
        /// </summary>
        NotReady,
        
        /// <summary>
        /// 已下载的资源。
        /// </summary>
        ReadyAsset,
        
        /// <summary>
        /// 已下载的二进制资源。
        /// </summary>
        ReadyBinary,
    }
}