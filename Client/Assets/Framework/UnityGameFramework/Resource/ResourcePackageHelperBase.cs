using GameFramework.Resource;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 资源包辅助工具实例基类。
    /// </summary>
    public abstract class ResourcePackageHelperBase : MonoBehaviour, IResourcePackageHelper
    {
        /// <summary>
        /// 创建指定名字的资源包。
        /// </summary>
        /// <param name="packageName">包名。</param>
        /// <param name="resourceMode">资源模式。</param>
        /// <param name="callbacks">加载回调。</param>
        public abstract void CreateResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks);

        /// <summary>
        /// 销毁指定的资源包。
        /// </summary>
        /// <param name="resourcePackage">资源包实例。</param>
        public abstract void DestroyResourcePackage(IResourcePackage resourcePackage);
    }
}