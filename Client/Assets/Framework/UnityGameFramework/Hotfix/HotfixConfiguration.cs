using UnityEngine;

namespace Framework.UnityGameFramework
{
    /// <summary>
    /// 热更新配置数据
    /// </summary>
    [CreateAssetMenu]
    public class HotfixConfiguration : ScriptableObject
    {
        [SerializeField]
        private string[] m_HotfixAssemblyNames = null;

        [SerializeField]
        private string[] m_HotfixAssemblyVersions = null;

        [SerializeField]
        private string m_HotfixLogicEntryTypeName = null;

        [SerializeField]
        private string m_HotfixLogicEntryMethodName = null;

        [SerializeField]
        private string m_HotfixAssemblyLoadPathPrefix = null;
        
        /// <summary>
        /// 热更新程序集名字列表
        /// </summary>
        public string[] HotfixAssemblyNames => m_HotfixAssemblyNames;
        
        /// <summary>
        /// 热更新程序集版本列表
        /// </summary>
        public string[] HotfixAssemblyVersions => m_HotfixAssemblyVersions;
        
        /// <summary>
        /// 热更新逻辑入口类型名字
        /// </summary>
        public string HotfixLogicEntryTypeName => m_HotfixLogicEntryTypeName;
        
        /// <summary>
        /// 热更新逻辑入口方法名字
        /// </summary>
        public string HotfixLogicEntryMethodName => m_HotfixLogicEntryMethodName;
        
        /// <summary>
        /// 热更新程序集加载路径前缀
        /// </summary>
        public string HotfixAssemblyLoadPathPrefix => m_HotfixAssemblyLoadPathPrefix;
    }
}