using FairyGUI;
using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;

namespace GameMono.UI
{
    /// <summary>
    /// 启动界面逻辑。
    /// </summary>
    [UIForm("ui://Launch/LaunchForm", "Main")]
    public class LaunchForm : FGUIFormLogic
    {
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Log.Info("LaunchForm OnOpen");
        }
    }
}