using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 热更新辅助工具抽象类。
    /// </summary>
    public abstract class HotfixAssetHelperBase : MonoBehaviour, IHotfixAssetHelper
    {
        /// <summary>
        /// 获取热更新程序集资源名。
        /// </summary>
        public abstract string GetAssemblyAssetName(AssemblyInfo assemblyInfo);
    }
}