using Cysharp.Threading.Tasks;
using Luban;

namespace GameLogic
{
    /// <summary>
    /// 同步加载指定名字的配置表数据。
    /// </summary>
    public delegate ByteBuf LoadDataTableFunc(string dataTableName);
    
    /// <summary>
    /// 异步加载指定名字的配置表数据。
    /// </summary>
    public delegate UniTask<ByteBuf> LoadDataTableAsyncFunc(string dataTableName);
}