using System;
using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 挂件收集器静态绑定器生成组件。该组件将提供生成热更层静态绑定代码，并在运行时提供获取静态绑定对象的功能。
    /// </summary>
    [RequireComponent(typeof(ObjectCollector))]
    [AddComponentMenu("")]
    public sealed class ObjectCollectorGenerator : MonoBehaviour
    {
        /// <summary>
        /// 获取静态绑定器实例。
        /// </summary>
        public ObjectCollectorBase ObjectCollector => m_HasCreated ? m_GenerateObjectCollector : CreateObjectCollector();

        /// <summary>
        /// 命名空间。
        /// </summary>
        public string Namespace;
        
        /// <summary>
        /// 静态绑定器类型名。
        /// </summary>
        public string TypeName;

        /// <summary>
        /// 程序集名字。目前限定必须是GameLogic
        /// </summary>
        public string AssemblyName;

        /// <summary>
        /// 生成静态绑定器代码文件的目录名。目前强制约束必须生成在GameLogic目录下。所以这里是相对于GameLogic的相对路径。
        /// </summary>
        public string GenerateDirectoryName;

        private ObjectCollectorBase m_GenerateObjectCollector;
        private ObjectCollector m_ObjectCollector;
        private bool m_HasCreated;

        private void Awake()
        {
            m_ObjectCollector = GetComponent<ObjectCollector>();
        }

        private ObjectCollectorBase CreateObjectCollector()
        {
            m_HasCreated = true;

            var typeName = TypeName;
            if (!string.IsNullOrEmpty(Namespace))
            {
                typeName = Utility.Text.Format("{0}.{1}", Namespace, typeName);
            }
            
            var runtimeType = Utility.Assembly.GetHotfixType(AssemblyName, typeName);
            if (runtimeType == null)
            {
                return null;
            }

            var gather = (ObjectCollectorBase)Activator.CreateInstance(runtimeType);
            gather.Awake(m_ObjectCollector);
            
            return gather;
        }
    }
}