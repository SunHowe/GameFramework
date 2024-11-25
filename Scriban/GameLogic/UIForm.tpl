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
        }

        /// <summary>
        /// 注册UI事件
        /// </summary>
        private void RegisterUIEvents()
        {
        }
    }
}