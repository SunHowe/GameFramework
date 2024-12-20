using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// UniTask拓展方法。
    /// </summary>
    public static class UniTaskExtensions
    {
        /// <summary>
        /// 等待UniTask完成，如果被取消则抛出异常。
        /// </summary>
        public static async UniTask ThrowIfCancellationRequested(this UniTask uniTask, CancellationToken token)
        {
            await uniTask;
            token.ThrowIfCancellationRequested();
        }
        
        /// <summary>
        /// 等待UniTask完成，如果被取消则抛出异常。
        /// </summary>
        public static async UniTask<T> ThrowIfCancellationRequested<T>(this UniTask<T> uniTask, CancellationToken token)
        {
            var o = await uniTask;
            token.ThrowIfCancellationRequested();
            return o;
        }
    }
}