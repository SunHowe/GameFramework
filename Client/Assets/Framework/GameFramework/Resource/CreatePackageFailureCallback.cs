namespace GameFramework.Resource
{
    /// <summary>
    /// 创建资源包失败回调
    /// <param name="packageName">创建的资源包名。</param>
    /// <param name="resourceMode">资源包资源模式。</param>
    /// <param name="status">资源包状态。</param>
    /// <param name="userData">用户数据。</param>
    /// </summary>
    public delegate void CreatePackageFailureCallback(string packageName, ResourceMode resourceMode, CreatePackageStatus status, object userData);
}