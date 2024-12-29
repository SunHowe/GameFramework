using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 功能容器拥有者实例。用于添加到某个GameObject上，让其拥有管理功能的能力。
    /// </summary>
    public sealed class FeatureOwnerObject : MonoBehaviour, IFeatureContainerOwner
    {
        public FeatureContainer FeatureContainer { get; private set; }

        private void Awake()
        {
            FeatureContainer = new FeatureContainer(this);
        }

        private void OnDestroy()
        {
            FeatureContainer.Shutdown();
            FeatureContainer = null;
        }
    }
}