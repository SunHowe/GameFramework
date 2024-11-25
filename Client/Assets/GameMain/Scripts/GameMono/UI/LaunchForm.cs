using FairyGUI;
using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;

namespace GameMono.UI
{
    /// <summary>
    /// 启动界面逻辑。
    /// </summary>
    public class LaunchForm : FGUIFormLogic
    {
        
        
        public LaunchForm(GComponent contentPane) : base(contentPane)
        {
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Log.Info("LaunchForm OnOpen");
        }
    }
}