using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;
using GameMono.UI;

namespace GameLogic.UI.{{ package_name }}
{
    [UIForm(URL, nameof(UIGroupType.Main))]
    public partial class {{ name }} : FGUIFormLogic
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            RegisterComponents(); // 绑定UI组件
            RegisterUIEvents();   // 注册UI事件
            RegisterFeatures();   // 注册UI功能
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
            this.AddScreenAdaptor(UIFormScreenAdaptorType.Constant); // 设置屏幕适配器
        }
    }
}