using System;
using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 可使用Dispose销毁的对象实例抽象类。
    /// </summary>
    public abstract class DisposableObjectBase : IReference, IDisposable
    {
        public abstract void Clear();

        public void Dispose()
        {
            ReferencePool.Release(this);
        }

        public static T Create<T>() where T: DisposableObjectBase, new()
        {
            return ReferencePool.Acquire<T>();
        }
    }
}