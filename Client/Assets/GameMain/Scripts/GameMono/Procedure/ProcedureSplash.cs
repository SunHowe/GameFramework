using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using UnityGameFramework.Runtime;

namespace GameMono
{
    /// <summary>
    /// 闪屏流程。
    /// </summary>
    public class ProcedureSplash : ProcedureBase
    {
        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            
            // TODO: 播放闪屏动画
            
            // 初始化一些基础工具。

            if (BaseComponent.Instance.EditorResourceMode)
            {
                // 编辑器模式
                Log.Info("Use EditorResourceMode.");
                ChangeState<ProcedureLaunchGameLogic>(procedureOwner);
            }
            else if (ResourceComponent.Instance.ResourceMode == ResourceMode.Package)
            {
                // 单机模式
                Log.Info("Use PackageResourceMode.");
                ChangeState<ProcedureInitResource>(procedureOwner);
            }
            else
            {
                // 可更新模式
                Log.Info("Use UpdatableResourceMode.");
                ChangeState<ProcedureCheckVersion>(procedureOwner);
            }
        }
    }
}