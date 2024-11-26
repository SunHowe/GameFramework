using Cysharp.Threading.Tasks;
using GameFramework;
using Luban;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 数据表模块。
    /// </summary>
    public sealed partial class DataTableModule : GameLogicBase<DataTableModule>
    {
        private const string DATA_TABLE_ASSET_PATH = "Assets/GameMain/DataTable/{0}.bytes";
        
        private UniTask<ByteBuf> LoadDataTableAsync(string outputDataTableName)
        {
            return ResourceComponent.Instance.LoadBinaryAsync(Utility.Text.Format(DATA_TABLE_ASSET_PATH, outputDataTableName))
                .ContinueWith(bytes => new ByteBuf(bytes));
        }
        
        protected override void OnAwake()
        {
        }

        protected override void OnShutdown()
        {
        }
    }
}