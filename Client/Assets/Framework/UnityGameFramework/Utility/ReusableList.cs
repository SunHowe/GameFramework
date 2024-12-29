using System;
using System.Collections.Generic;
using GameFramework;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 通过引用池维护的可复用列表。
    /// </summary>
    public sealed class ReusableList<T> : List<T>, IDisposable, IReference
    {
        /// <summary>
        /// 销毁实例。
        /// </summary>
        public void Dispose()
        {
            ReferencePool.Release(this);
        }

        /// <summary>
        /// 构造实例。
        /// </summary>
        public static ReusableList<T> Create()
        {
            return ReferencePool.Acquire<ReusableList<T>>();
        }
    }
}