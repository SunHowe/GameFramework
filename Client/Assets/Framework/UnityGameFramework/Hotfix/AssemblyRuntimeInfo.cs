using System.Reflection;

namespace Framework.UnityGameFramework
{
    /// <summary>
    /// 程序集运行时信息
    /// </summary>
    public class AssemblyRuntimeInfo
    {
        public readonly string AssemblyName;
        public readonly string AssemblyVersion;
        public readonly Assembly Assembly;

        public AssemblyRuntimeInfo(string assemblyName, string assemblyVersion, Assembly assembly)
        {
            AssemblyName = assemblyName;
            AssemblyVersion = assemblyVersion;
            Assembly = assembly;
        }
    }
}