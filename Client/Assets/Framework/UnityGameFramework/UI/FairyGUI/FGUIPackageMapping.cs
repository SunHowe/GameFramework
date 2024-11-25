using UnityEngine;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FairyGUI包名映射工具。
    /// </summary>
    [CreateAssetMenu]
    public sealed class FGUIPackageMapping : ScriptableObject
    {
        /// <summary>
        /// 包名列表 与PackageIds一一对应。
        /// </summary>
        public string[] PackageNames;

        /// <summary>
        /// 包ID列表 与PackageNames一一对应。
        /// </summary> 
        public string[] PackageIds;
    }
}