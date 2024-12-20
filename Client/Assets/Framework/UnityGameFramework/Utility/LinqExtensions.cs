using System.Collections.Generic;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// Linq拓展方法。
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// 将自己添加到指定的List容器。
        /// </summary>
        public static T AddTo<T>(this T item, List<T> buffer)
        {
            buffer.Add(item);
            return item;
        }

        /// <summary>
        /// 将自己添加到指定的IList容器。
        /// </summary>
        public static T AddTo<T>(this T item, IList<T> buffer)
        {
            buffer.Add(item);
            return item;
        }

        /// <summary>
        /// 将自己添加到指定的ICollection容器。
        /// </summary>
        public static T AddTo<T>(this T item, ICollection<T> buffer)
        {
            buffer.Add(item);
            return item;
        }
    }
}