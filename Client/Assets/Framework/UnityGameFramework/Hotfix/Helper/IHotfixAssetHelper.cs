namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 热更新资源辅助工具接口。
    /// </summary>
    public interface IHotfixAssetHelper
    {
        /// <summary>
        /// 获取热更新程序集资源名。
        /// </summary>
        string GetAssemblyAssetName(AssemblyInfo assemblyInfo);
    }
}