using System.IO;
using GameFramework;
using YooAsset;

namespace UnityGameFramework.Runtime
{
    public partial class YooAssetResourcePackageHelper
    {
        private sealed class RemoteServices : IRemoteServices
        {
            private readonly string m_CDNHostServer;
            private readonly string m_FallbackCDNHostServer;

            public RemoteServices(string cdnHostServer, string fallbackCdnHostServer)
            {
                m_CDNHostServer = cdnHostServer;
                m_FallbackCDNHostServer = fallbackCdnHostServer;
            }

            public string GetRemoteMainURL(string fileName)
            {
                return Utility.Path.GetRemotePath(Path.Combine(m_CDNHostServer, fileName));
            }

            public string GetRemoteFallbackURL(string fileName)
            {
                return Utility.Path.GetRemotePath(Path.Combine(m_FallbackCDNHostServer, fileName));
            }
        }
    }
}