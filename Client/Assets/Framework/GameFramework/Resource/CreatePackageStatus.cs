namespace GameFramework.Resource
{
    /// <summary>
    /// 创建资源包状态。
    /// </summary>
    public enum CreatePackageStatus : byte
    {
        /// <summary>
        /// 创建资源包成功。
        /// </summary>
        Success,

        /// <summary>
        /// 资源包不存在。
        /// </summary>
        NotExist,

        /// <summary>
        /// 重复创建。
        /// </summary>
        DuplicateCreate,
    }
}