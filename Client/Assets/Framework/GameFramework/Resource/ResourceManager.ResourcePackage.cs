using System.Collections.Generic;

namespace GameFramework.Resource
{
    /// <summary>
    /// 资源管理器。
    /// </summary>
    internal partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        private string m_DefaultPackageName;
        private string m_DefaultPackageNameWhileCreate;
        private readonly Dictionary<string, IResourcePackage> m_ResourcePackageDict;
        private readonly List<IResourcePackage> m_ResourcePackages;
        private readonly Dictionary<string, CreatePackagePackInfo> m_ResourcePackagesWhileCreate;

        private readonly CreatePackageCallbacks m_CreateDefaultPackageCallbacks;
        private readonly CreatePackageCallbacks m_CreatePackageCallbacks;

        public IResourcePackage DefaultPackage { get; private set; }

        public IResourcePackage[] GetResourcePackages()
        {
            var result = new IResourcePackage[m_ResourcePackageDict.Count];
            m_ResourcePackages.CopyTo(result, 0);
            return result;
        }

        public void GetResourcePackages(List<IResourcePackage> result)
        {
            result.Clear();
            result.AddRange(m_ResourcePackages);
        }

        public void CreateDefaultResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks)
        {
            CreateDefaultResourcePackage(packageName, resourceMode, callbacks, null);
        }

        public void CreateDefaultResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks, object userData)
        {
            if (m_ResourcePackageHelper == null)
            {
                throw new GameFrameworkException("Resource package helper is invalid.");
            }

            if (!string.IsNullOrEmpty(m_DefaultPackageName) || !string.IsNullOrEmpty(m_DefaultPackageNameWhileCreate))
            {
                if (callbacks.CreatePackageFailureCallback != null)
                {
                    callbacks.CreatePackageFailureCallback(packageName, resourceMode, CreatePackageStatus.DuplicateCreateDefault, userData);
                    return;
                }

                throw new GameFrameworkException("Duplicate create default resource package.");
            }

            if (m_ResourcePackageDict.ContainsKey(packageName) || m_ResourcePackagesWhileCreate.ContainsKey(packageName))
            {
                if (callbacks.CreatePackageFailureCallback != null)
                {
                    callbacks.CreatePackageFailureCallback(packageName, resourceMode, CreatePackageStatus.DuplicateCreate, userData);
                    return;
                }

                throw new GameFrameworkException("Duplicate create resource package.");
            }

            m_DefaultPackageNameWhileCreate = packageName;

            var packInfo = CreatePackagePackInfo.Create(packageName, resourceMode, callbacks, userData);
            m_ResourcePackagesWhileCreate.Add(packageName, packInfo);

            m_ResourcePackageHelper.CreateResourcePackage(packageName, resourceMode, m_CreateDefaultPackageCallbacks);
        }

        public void CreateResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks)
        {
            CreateResourcePackage(packageName, resourceMode, callbacks, null);
        }

        public void CreateResourcePackage(string packageName, ResourceMode resourceMode, CreatePackageCallbacks callbacks, object userData)
        {
            if (m_ResourcePackageHelper == null)
            {
                throw new GameFrameworkException("Resource package helper is invalid.");
            }

            if (m_ResourcePackageDict.ContainsKey(packageName) || m_ResourcePackagesWhileCreate.ContainsKey(packageName))
            {
                if (callbacks.CreatePackageFailureCallback != null)
                {
                    callbacks.CreatePackageFailureCallback(packageName, resourceMode, CreatePackageStatus.DuplicateCreate, userData);
                    return;
                }

                throw new GameFrameworkException("Duplicate create resource package.");
            }

            var packInfo = CreatePackagePackInfo.Create(packageName, resourceMode, callbacks, userData);
            m_ResourcePackagesWhileCreate.Add(packageName, packInfo);

            m_ResourcePackageHelper.CreateResourcePackage(packageName, resourceMode, m_CreatePackageCallbacks);
        }

        public void DestroyResourcePackage(string packageName)
        {
            if (m_ResourcePackageHelper == null)
            {
                throw new GameFrameworkException("Resource package helper is invalid.");
            }

            if (m_DefaultPackageName == packageName || m_DefaultPackageNameWhileCreate == packageName)
            {
                throw new GameFrameworkException("Can not destroy default resource package.");
            }

            if (m_ResourcePackageDict.TryGetValue(packageName, out var package))
            {
                m_ResourcePackageDict.Remove(packageName);
                m_ResourcePackages.Remove(package);
                m_ResourcePackageHelper.DestroyResourcePackage(package);
            }
            else if (m_ResourcePackagesWhileCreate.TryGetValue(packageName, out var packInfo))
            {
                m_ResourcePackagesWhileCreate.Remove(packageName);
                if (packInfo.Callbacks.CreatePackageFailureCallback != null)
                {
                    packInfo.Callbacks.CreatePackageFailureCallback(packInfo.PackageName, packInfo.ResourceMode, CreatePackageStatus.Cancel, packInfo.UserData);
                }
                ReferencePool.Release(packInfo);
            }
        }

        public IResourcePackage GetResourcePackage(string packageName)
        {
            return m_ResourcePackageDict.TryGetValue(packageName, out var package) ? package : null;
        }
        
        public string[] GetAssetNames()
        {
            var result = new List<string>();
            GetAssetNames(result);
            return result.ToArray();
        }

        public void GetAssetNames(List<string> results)
        {
            foreach (var package in m_ResourcePackages)
            {
                package.GetAssetNames(results);
            }
        }

        /// <summary>
        /// 创建默认资源包成功回调。
        /// </summary>
        private void OnCreateDefaultPackageSuccess(string packageName, ResourceMode resourceMode, IResourcePackage resourcePackage, object userData)
        {
            if (m_DefaultPackageNameWhileCreate != packageName)
            {
                GameFrameworkLog.Fatal(Utility.Text.Format("Default package name is not match. {0} {1}", m_DefaultPackageNameWhileCreate, packageName));
                return;
            }

            if (!m_ResourcePackagesWhileCreate.TryGetValue(packageName, out var packInfo))
            {
                GameFrameworkLog.Fatal("Can not find default package pack info.");
                return;
            }
            
            m_DefaultPackageName = packageName;
            m_DefaultPackageNameWhileCreate = string.Empty;
            
            m_ResourcePackageDict.Add(packageName, resourcePackage);
            m_ResourcePackages.Add(resourcePackage);
            DefaultPackage = resourcePackage;

            m_ResourcePackagesWhileCreate.Remove(packageName);
            packInfo.Callbacks.CreatePackageSuccessCallback(packageName, resourceMode, resourcePackage, packInfo.UserData);
            ReferencePool.Release(packInfo);
        }

        /// <summary>
        /// 创建默认资源包失败回调。
        /// </summary>
        private void OnCreateDefaultPackageFailure(string packageName, ResourceMode resourceMode, CreatePackageStatus status, object userData)
        {
            if (m_DefaultPackageNameWhileCreate != packageName)
            {
                GameFrameworkLog.Fatal(Utility.Text.Format("Default package name is not match. {0} {1}", m_DefaultPackageNameWhileCreate, packageName));
                return;
            }

            if (!m_ResourcePackagesWhileCreate.TryGetValue(packageName, out var packInfo))
            {
                GameFrameworkLog.Fatal("Can not find default package pack info.");
                return;
            }
            
            m_DefaultPackageNameWhileCreate = string.Empty;
            
            m_ResourcePackagesWhileCreate.Remove(packageName);
            if (packInfo.Callbacks.CreatePackageFailureCallback != null)
            {
                packInfo.Callbacks.CreatePackageFailureCallback(packageName, resourceMode, status, packInfo.UserData);
            }
            
            ReferencePool.Release(packInfo);
        }

        /// <summary>
        /// 创建资源包成功回调。
        /// </summary>
        private void OnCreatePackageSuccess(string packageName, ResourceMode resourceMode, IResourcePackage resourcePackage, object userData)
        {
            if (!m_ResourcePackagesWhileCreate.TryGetValue(packageName, out var packInfo))
            {
                // 被取消的加载。
                return;
            }
            
            m_ResourcePackagesWhileCreate.Remove(packageName);
            m_ResourcePackageDict.Add(packageName, resourcePackage);
            m_ResourcePackages.Add(resourcePackage);

            packInfo.Callbacks.CreatePackageSuccessCallback(packageName, resourceMode, resourcePackage, packInfo.UserData);
            ReferencePool.Release(packInfo);
        }

        /// <summary>
        /// 创建资源包失败回调。
        /// </summary>
        private void OnCreatePackageFailure(string packageName, ResourceMode resourceMode, CreatePackageStatus status, object userData)
        {
            if (!m_ResourcePackagesWhileCreate.TryGetValue(packageName, out var packInfo))
            {
                // 被取消的加载。
                return;
            }
            
            m_ResourcePackagesWhileCreate.Remove(packageName);

            if (packInfo.Callbacks.CreatePackageFailureCallback != null)
            {
                packInfo.Callbacks.CreatePackageFailureCallback(packageName, resourceMode, status, packInfo.UserData);
            }
            
            ReferencePool.Release(packInfo);
        }
    }
}