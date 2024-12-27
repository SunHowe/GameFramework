using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 对象收集器功能。
    /// </summary>
    public sealed class ObjectCollectorFeature : Feature
    {
        /// <summary>
        /// 对象收集器。
        /// </summary>
        private ObjectCollector m_ObjectCollector;
        
        /// <summary>
        /// 设置对象收集器。
        /// </summary>
        public void SetObjectCollector(ObjectCollector objectCollector)
        {
            m_ObjectCollector = objectCollector;
        }

        /// <summary>
        /// 获取指定名字的挂件。
        /// </summary>
        public Object GetObject(string objectName)
        {
            if (m_ObjectCollector == null)
            {
                Log.Error("ObjectCollector is invalid.");
                return null;
            }

            return m_ObjectCollector.GetObject(objectName);
        }

        /// <summary>
        /// 获取指定名字指定类型的对象。
        /// </summary>
        public T Get<T>(string objectName) where T : Object
        {
            if (m_ObjectCollector == null)
            {
                Log.Error("ObjectCollector is invalid.");
                return null;
            }

            return m_ObjectCollector.Get<T>(objectName);
        }
        
        public override void Awake(object featureOwner, FeatureContainer featureContainer)
        {
            base.Awake(featureOwner, featureContainer);

            // 如果持有者是MonoBehaviour 则尝试从持有者上获取挂载节点的组件。
            if (featureOwner is not MonoBehaviour monoBehaviour)
            {
                return;
            }
            
            var objectCollector = monoBehaviour.GetComponent<ObjectCollector>();
            if (objectCollector != null)
            {
                SetObjectCollector(objectCollector);
            }
        }

        public override void Shutdown()
        {
            m_ObjectCollector = null;
        }
    }
}