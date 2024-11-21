using UnityEngine;
using UnityGameFramework.Runtime;

namespace Framework.UnityGameFramework
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Hotfix")]
    public sealed class HotfixComponent : GameFrameworkComponent
    {
        [SerializeField] 
        private string[] m_HotfixAssemblyNames = null;
        
        [SerializeField]
        private string m_HotfixLogicEntryTypeName = null;
        
        [SerializeField]
        private string m_HotfixLogicEntryMethodName = null;
        
        
        private EventComponent m_EventComponent = null;
    }
}