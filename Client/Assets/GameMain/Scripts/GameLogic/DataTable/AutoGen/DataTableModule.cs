
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using Cysharp.Threading.Tasks;

namespace GameLogic
{
    public partial class DataTableModule
    {
        public item.TbItem TbItem { get; private set; }
        
        public async UniTask LoadAsync()
        {
            var tasks = new System.Collections.Generic.List<UniTask>
            {
                LoadDataTableAsync("item_tbitem").ContinueWith(buf => TbItem = new item.TbItem(buf)),
            };
            await UniTask.WhenAll(tasks);
            
            ResolveRef();
        }
        
        private void ResolveRef()
        {
            TbItem.ResolveRef(this);
        }
    }

}

