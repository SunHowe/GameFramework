using System.Collections.Generic;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源包接口。
    /// </summary>
    public interface IResourcePackage
    {
        /// <summary>
        /// 资源包名字。
        /// </summary>
        string PackageName { get; }
        
        /// <summary>
        /// 资源包采用的资源模式。
        /// </summary>
        ResourceMode ResourceMode { get; }
        
        /// <summary>
        /// 获取资源包下所有资源的资源标识。
        /// </summary>
        /// <returns>资源包下所有资源的资源标识。</returns>
        string[] GetAssetNames();
        
        /// <summary>
        /// 获取资源包下所有资源的资源标识。
        /// </summary>
        /// <param name="results">将资源标识写到该list中。</param>
        void GetAssetNames(List<string> results);
    }
}