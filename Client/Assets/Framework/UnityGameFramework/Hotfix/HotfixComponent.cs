using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Framework.UnityGameFramework
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Hotfix")]
    public sealed class HotfixComponent : GameFrameworkComponent
    {
        [SerializeField]
        private HotfixConfigurationResourceRef m_HotfixConfigResourceRef = null;

        private EventComponent m_EventComponent = null;
        private ResourceComponent m_ResourceComponent = null;
        
        /// <summary>
        /// 已加载的热更新程序集信息
        /// </summary>
        private static readonly Dictionary<string, AssemblyRuntimeInfo> s_LoadedAssemblies = new Dictionary<string, AssemblyRuntimeInfo>();

        private void Start()
        {
            m_EventComponent = GameEntry.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }
            
            m_ResourceComponent = GameEntry.GetComponent<ResourceComponent>();
            if (m_ResourceComponent == null)
            {
                Log.Fatal("Resource component is invalid.");
                return;
            }
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        public void LoadHotfix()
        {
        }
    }
}