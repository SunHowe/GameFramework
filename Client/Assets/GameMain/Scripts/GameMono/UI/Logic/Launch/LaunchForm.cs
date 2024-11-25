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
    }
}