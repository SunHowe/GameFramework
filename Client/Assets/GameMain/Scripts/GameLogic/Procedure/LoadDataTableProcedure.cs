using Cysharp.Threading.Tasks;
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
            LoadDataTableAsync().Forget();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            
            if (m_IsComplete)
                ChangeState<LoginProcedure>(procedureOwner);
        }

        private async UniTask LoadDataTableAsync()
        {
            await DataTableModule.Instance.LoadAsync();
            
            m_IsComplete = true;
            
            foreach (var item in DataTableModule.Instance.TbItem.DataList)
            {
                Log.Info(item.ToString());
            }
        }
    }
}