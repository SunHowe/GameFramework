using System.Collections.Generic;
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
        
        /// <summary>
        /// 异步加载指定配置表数据。
        /// </summary>
        private UniTask<ByteBuf> LoadDataTableAsync(string dataTableName)
        {
            return ResourceComponent.Instance.LoadBinaryAsync(Utility.Text.Format(DATA_TABLE_ASSET_PATH, dataTableName))
                .ContinueWith(bytes => new ByteBuf(bytes));
        }

        /// <summary>
        /// 同步加载指定配置表数据。
        /// </summary>
        private ByteBuf LoadDataTable(string dataTableName)
        {
            return new ByteBuf(ResourceComponent.Instance.LoadBinary(Utility.Text.Format(DATA_TABLE_ASSET_PATH, dataTableName)));
        }

        /// <summary>
        /// 在使用懒加载模式时，可以在这里指定需要预加载的配置表。
        /// </summary>
        private List<IDataTable> GetPreloadDataTables()
        {
            var dataTables = new List<IDataTable>();
            
            return dataTables;
        }
        
        protected override void OnAwake()
        {
        }

        protected override void OnShutdown()
        {
        }
    }
}