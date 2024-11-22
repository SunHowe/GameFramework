using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 热更新程序集应用实例抽象类。热更新程序集中应有一个类从该类派生，在热更新加载完成后用于启动热更新逻辑。
    /// </summary>
    public abstract class HotfixAppBase : GameLogicBase<HotfixAppBase>
    {
    }
}