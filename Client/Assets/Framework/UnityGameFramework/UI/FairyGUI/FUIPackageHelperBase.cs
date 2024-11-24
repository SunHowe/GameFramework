using FairyGUI.Dynamic;
using UnityEngine;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// UIPackage辅助工具基类。
    /// </summary>
    public abstract class FUIPackageHelperBase : MonoBehaviour, IUIPackageHelper
    {
        /// <summary>
        /// 通过包id获取包名。
        /// </summary>
        public abstract string GetPackageNameById(string id);
    }
}