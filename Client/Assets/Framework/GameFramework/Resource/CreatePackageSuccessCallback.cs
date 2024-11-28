namespace GameFramework.Resource
{
    /// <summary>
    /// 创建资源包成功回调
    /// <param name="packageName">创建的资源包名。</param>
    /// <param name="resourceMode">资源包资源模式。</param>
    /// <param name="resourcePackage">资源包实例。</param>
    /// <param name="userData">用户数据。</param>
    /// </summary>
    public delegate void CreatePackageSuccessCallback(string packageName, ResourceMode resourceMode, IResourcePackage resourcePackage, object userData);
}