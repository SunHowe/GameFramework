using GameFramework.DataTable;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 数据表组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/DataTable")]
    public class DataTableComponent : GameFrameworkComponent<DataTableComponent>
    {
        [SerializeField]
        private DataTableLoadMode m_LoadMode;

        /// <summary>
        /// 采用的加载模式。
        /// </summary>
        public DataTableLoadMode LoadMode
        {
            get { return m_LoadMode; }
            set { m_LoadMode = value; }
        }
    }
}