using System;
using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 挂件收集器静态绑定器生成组件。该组件将提供生成热更层静态绑定代码，并在运行时提供获取静态绑定对象的功能。
    /// </summary>
    [RequireComponent(typeof(WidgetsGather))]
    [AddComponentMenu("")]
    public sealed class WidgetsGatherGenerator : MonoBehaviour
    {
        /// <summary>
        /// 获取静态绑定器实例。
        /// </summary>
        public WidgetsGatherBase Gather => m_HasCreated ? m_Gather : CreateGather();
        
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

        private WidgetsGatherBase m_Gather;
        private WidgetsGather m_WidgetsGather;
        private bool m_HasCreated;

        private void Awake()
        {
            m_WidgetsGather = GetComponent<WidgetsGather>();
        }

        private WidgetsGatherBase CreateGather()
        {
            m_HasCreated = true;

            var runtimeType = Utility.Assembly.GetType(AssemblyName, TypeName);
            if (runtimeType == null)
            {
                return null;
            }

            var gather = (WidgetsGatherBase)Activator.CreateInstance(runtimeType);
            gather.Awake(m_WidgetsGather);
            
            return gather;
        }
    }
}