
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using Cysharp.Threading.Tasks;
using GameLogic;
using GameFramework.DataTable;
using System.Collections.Generic;

namespace GameLogic
{
    public partial class DataTableModule
    {
        public item.TbItem TbItem { get; private set; }
        
        private List<IDataTable> m_PreloadDataTables;
        
        public void Init(DataTableLoadMode mode)
        {
            var isPreloadAll = mode == DataTableLoadMode.AsyncLoad || mode == DataTableLoadMode.SyncLoad;
            TbItem = new item.TbItem(this, "item_tbitem", LoadDataTable, LoadDataTableAsync);
            
            if (isPreloadAll)
            {
                m_PreloadDataTables = new List<IDataTable> 
                {
                    TbItem,
                };
            }
            else
            {
                m_PreloadDataTables = GetPreloadDataTables();
            }
        }
        
        public void Preload()
        {
            foreach (var dataTable in m_PreloadDataTables)
            {
                dataTable.Load(false);
            }
            
            foreach (var dataTable in m_PreloadDataTables)
            {
                dataTable.ResolveRef();
            }
        }
        
        public async UniTask PreloadAsync()
        {
            await UniTask.WhenAll(m_PreloadDataTables.Select(dataTable => dataTable.LoadAsync(false)));
            
            foreach (var dataTable in m_PreloadDataTables)
            {
                dataTable.ResolveRef();
            }
        }
    }

}

