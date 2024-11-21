using UnityEngine;
using UnityEngine.Serialization;

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

        [FormerlySerializedAs("m_HotfixLogicEntryTypeName")]
        [SerializeField]
        private string m_HotfixAppTypeName = null;
        
        /// <summary>
        /// 热更新程序集信息列表
        /// </summary>
        public AssemblyInfo[] HotfixAssemblies => m_HotfixAssemblies;
        
        /// <summary>
        /// 热更新逻辑入口类型名字
        /// </summary>
        public string HotfixAppTypeName => m_HotfixAppTypeName;
    }
}