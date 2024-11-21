using System;
using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 类型引用 用于序列化到数据中 提供编辑器和运行时工具进行方便的设置/获取Type
    /// </summary>
    [Serializable]
    public abstract class TypeRef
    {
        /// <summary>
        /// 类型的完整名字
        /// </summary>
        [SerializeField]
        private string m_FullName;

        /// <summary>
        /// 程序集名字
        /// </summary>
        [SerializeField]
        private string m_AssemblyName;

        /// <summary>
        /// 基类与接口约束
        /// </summary>
        public virtual Type BaseType { get; } = null;

        /// <summary>
        /// 获取运行时类型
        /// </summary>
        public Type GetRuntimeType()
        {
            return Utility.Assembly.GetType(m_AssemblyName, m_FullName);
        }
    }

    /// <summary>
    /// 类型引用的单泛型支持 用于约束基类
    /// </summary>
    [Serializable]
    public abstract class TypeRef<T> : TypeRef
    {
        public override Type BaseType => typeof(T);
    }
}