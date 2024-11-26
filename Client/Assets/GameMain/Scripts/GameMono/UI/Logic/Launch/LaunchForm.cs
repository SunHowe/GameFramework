using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;
using GameMono.UI;

namespace GameMono.UI.Launch
{
    [UIForm(URL, nameof(UIGroupType.Main))]
    public partial class LaunchForm : FGUIFormLogic
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            RegisterComponents(); // 绑定UI组件
            RegisterUIEvents();   // 注册UI事件
            RegisterFeatures();   // 注册UI功能
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            
            Log.Info("LaunchForm OnOpen");
        }

        /// <summary>
        /// 注册UI事件
        /// </summary>
        private void RegisterUIEvents()
        {
        }

        /// <summary>
        /// 注册UI功能。
        /// </summary>
        private void RegisterFeatures()
        {
            this.AddFrameFeature(); // 添加通用窗体框架
            this.AddScreenAdaptor(FGUIFormScreenAdaptorType.FullScreen); // 设置屏幕适配器
        }
    }
}