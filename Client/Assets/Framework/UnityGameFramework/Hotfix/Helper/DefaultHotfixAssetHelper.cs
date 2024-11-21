using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 默认的热更新辅助工具实现。
    /// </summary>
    public sealed class DefaultHotfixAssetHelper : HotfixAssetHelperBase
    {
        /// <summary>
        /// 获取热更新程序集资源名。
        /// </summary>
        public override string GetAssemblyAssetName(AssemblyInfo assemblyInfo)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Hotfix/{0}.dll", assemblyInfo.Name);
        }
    }
}