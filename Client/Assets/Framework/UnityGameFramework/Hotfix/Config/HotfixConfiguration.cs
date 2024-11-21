using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 热更新配置数据
    /// </summary>
    [CreateAssetMenu]
    public class HotfixConfiguration : ScriptableObject
    {
        [SerializeField]
        private AssemblyInfo[] m_HotfixAssemblies = null;

        [SerializeField]
        private HotfixAppTypeRef m_HotfixAppType = null;

        /// <summary>
        /// 热更新程序集信息列表
        /// </summary>
        public AssemblyInfo[] HotfixAssemblies => m_HotfixAssemblies;

        /// <summary>
        /// 热更新逻辑入口类型
        /// </summary>
        public HotfixAppTypeRef HotfixAppType => m_HotfixAppType;
    }
}