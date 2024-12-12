namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FGUI自定义组件接口。
    /// </summary>
    public interface IFGUICustomComponent
    {
        /// <summary>
        /// 功能容器。在组件销毁时会跟着被销毁。
        /// </summary>
        FeatureContainer FeatureContainerOnInit { get; }
        
        /// <summary>
        /// 功能容器。在组件从舞台移除时会跟着被销毁。
        /// </summary>
        FeatureContainer FeatureContainerOnAddedToStage { get; }
    }
}