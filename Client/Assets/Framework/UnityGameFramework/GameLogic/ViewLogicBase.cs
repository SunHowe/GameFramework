using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 视图逻辑抽象类。
    /// </summary>
    public abstract class ViewLogicBase : IGameLogic, IFeatureContainerOwner
    {
        public FeatureContainer FeatureContainer => m_FeatureContainer ??= new FeatureContainer(this);
        private FeatureContainer m_FeatureContainer;
        
        public void Awake()
        {
            m_FeatureContainer?.Awake();
            OnAwake();
        }

        public void Shutdown()
        {
            OnShutdown();
            m_FeatureContainer.Shutdown();
        }

        protected abstract void OnAwake();
        protected abstract void OnShutdown();
    }
}