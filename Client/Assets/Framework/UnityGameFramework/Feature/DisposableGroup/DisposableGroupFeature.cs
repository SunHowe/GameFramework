namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// DisposableGroup 功能。
    /// </summary>
    public sealed class DisposableGroupFeature : Feature
    {
        /// <summary>
        /// 可批量调用IDisposable.Dispose()的容器。
        /// </summary>
        public DisposableGroup DisposableGroup => m_DisposableGroup ??= new DisposableGroup();
        
        private DisposableGroup m_DisposableGroup;
        
        public override void Shutdown()
        {
            m_DisposableGroup?.Dispose();
        }
    }
}