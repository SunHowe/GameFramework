using Cysharp.Threading.Tasks;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 热更新应用程序。
    /// </summary>
    public class HotfixApp : HotfixAppBase
    {
        protected override void OnAwake()
        {
            GameLogicComponent.Instance.AddGameLogic<DataTableModule>();

            DataTableModule.Instance.LoadAsync().ContinueWith(() =>
            {
                foreach (var item in DataTableModule.Instance.TbItem.DataList)
                {
                    Log.Info(item.ToString());
                }
            }).Forget();
        }

        protected override void OnShutdown()
        {
        }
    }
}