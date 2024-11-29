using System;
using Cysharp.Threading.Tasks;
using GameFramework.DataTable;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 加载配置表流程。
    /// </summary>
    public sealed class LoadDataTableProcedure : ProcedureBase
    {
        private bool m_IsComplete;
        
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_IsComplete = false;

            var mode = DataTableComponent.Instance.LoadMode;
            DataTableModule.Instance.Init(mode);
            
            switch (mode)
            {
                case DataTableLoadMode.AsyncLoad:
                case DataTableLoadMode.LazyLoadAndPreloadAsync:
                    // 异步预加载配置。
                    DataTableModule.Instance.PreloadAsync().ContinueWith(() => m_IsComplete = true).Forget();
                    break;
                case DataTableLoadMode.LazyLoadAndPreloadSync:
                case DataTableLoadMode.SyncLoad:
                    // 同步预加载配置。
                    DataTableModule.Instance.Preload();
                    m_IsComplete = true;
                    break;
                case DataTableLoadMode.LazyLoad:
                    // 不做预加载。
                    m_IsComplete = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            
            if (m_IsComplete)
                ChangeState<LoginProcedure>(procedureOwner);
        }
    }
}