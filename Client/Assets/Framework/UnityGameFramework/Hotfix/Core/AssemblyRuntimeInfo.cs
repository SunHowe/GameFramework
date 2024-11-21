using System.Reflection;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 程序集运行时信息
    /// </summary>
    public class AssemblyRuntimeInfo
    {
        public readonly string Name;
        public readonly string Version;
        public readonly Assembly Assembly;

        public AssemblyRuntimeInfo(string name, string version, Assembly assembly)
        {
            Name = name;
            Version = version;
            Assembly = assembly;
        }
    }
}