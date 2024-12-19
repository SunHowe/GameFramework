using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    public partial class ObjectPoolComponent
    {
        private readonly Queue<Transform> m_TransformRootPool = new Queue<Transform>();

        /// <summary>
        /// 获取一个对象池根节点实例。
        /// </summary>
        public Transform AcquireTransformRoot(string rootName)
        {
            Transform transformRoot;
            if (m_TransformRootPool.Count > 0)
            {
                transformRoot = m_TransformRootPool.Dequeue();
            }
            else
            {
                var transformRootGameObject = new GameObject();
                transformRoot = transformRootGameObject.transform;
                transformRoot.SetParent(transform);
                transformRootGameObject.SetActive(false);
            }
#if UNITY_EDITOR
            transform.name = rootName;
#endif
            return transformRoot;
        }

        /// <summary>
        /// 归还对象池根节点实例。
        /// </summary>
        public void ReleaseTransformRoot(Transform transformRoot)
        {
            m_TransformRootPool.Enqueue(transformRoot);
        }
    }
}