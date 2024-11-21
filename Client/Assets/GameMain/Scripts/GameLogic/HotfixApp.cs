using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 热更新应用程序。
    /// </summary>
    public class HotfixApp : HotfixAppBase
    {
        public override void Awake()
        {
            Log.Info("HotfixApp Awake");
        }

        public override void Shutdown()
        {
            Log.Info("HotfixApp Shutdown");
        }
    }
}