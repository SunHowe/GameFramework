namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 功能持有者。功能持有者可以持有一个功能容器，可供各模块自行组织使用一些子功能组件。
    /// </summary>
    public interface IFeatureOwner
    {
        /// <summary>
        /// 功能容器。需要功能持有者维护其生命周期。
        /// </summary>
        FeatureContainer FeatureContainer { get; set; }
    }
}