using System.Collections.Generic;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 容器拓展方法。
    /// </summary>
    public static class CollectionExtensions
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

        /// <summary>
        /// 安全的访问容器的接口。若下标越界则返回默认值。
        /// </summary>
        public static T SafeGet<T>(this List<T> list, int index)
        {
            if (index < 0 || index >= list.Count)
            {
                return default(T);
            }
            
            return list[index];
        }

        /// <summary>
        /// 安全的访问容器的接口。若下标越界则返回默认值。
        /// </summary>
        public static T SafeGet<T>(this IList<T> list, int index)
        {
            if (index < 0 || index >= list.Count)
            {
                return default(T);
            }
            
            return list[index];
        }
    }
}