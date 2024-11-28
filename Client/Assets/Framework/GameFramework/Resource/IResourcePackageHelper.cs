namespace GameFramework.Resource
{
    /// <summary>
    /// 资源包辅助工具接口。
    /// </summary>
    public interface IResourcePackageHelper
    {
        /// <summary>
        /// 创建默认资源包。
        /// </summary>
        /// <param name="packageName">包名。</param>
        /// <param name="resourceMode">资源模式。</param>
        /// <param name="callbacks">加载回调。</param>
        void CreateDefaultResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks);
        
        /// <summary>
        /// 创建指定名字的资源包。
        /// </summary>
        /// <param name="packageName">包名。</param>
        /// <param name="resourceMode">资源模式。</param>
        /// <param name="callbacks">加载回调。</param>
        void CreateResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks);

        /// <summary>
        /// 销毁指定的资源包。
        /// </summary>
        /// <param name="resourcePackage">资源包实例。</param>
        void DestroyResourcePackage(IResourcePackage resourcePackage);
    }
}