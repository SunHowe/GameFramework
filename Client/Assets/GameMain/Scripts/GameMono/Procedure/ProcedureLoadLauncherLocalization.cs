﻿using Cysharp.Threading.Tasks;
using GameFramework;
using GameFramework.Fsm;
using GameFramework.Procedure;
using Luban;
using UnityGameFramework.Runtime;

namespace GameMono
{
    /// <summary>
    /// 加载启动器本地化文本的流程。
    /// </summary>
    public sealed class ProcedureLoadLauncherLocalization : ProcedureBase
    {
        /// <summary>
        /// 本地化文本加载路径。
        /// </summary>
        private const string LOCALIZATION_ASSET_PATH = "Assets/GameLauncher/Localization/{0}.bytes";

        private bool m_IsComplete;
        
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_IsComplete = false;
            
            // 加载本地化文本配置。
            var assetPath = Utility.Text.Format(LOCALIZATION_ASSET_PATH, LocalizationComponent.Instance.Language);
            ResourceComponent.Instance.LoadBinaryAsync(assetPath)
                .ContinueWith(OnLoadFinished)
                .Forget();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (!m_IsComplete)
            {
                return;
            }
            
            ChangeState<ProcedureCreateDefaultPackage>(procedureOwner);
            
            Log.Info(LocalizationComponent.Instance.GetString("launch_demo_text_1"));
        }

        private void OnLoadFinished(byte[] bytes)
        {
            var byteBuf = new ByteBuf(bytes);
            LocalizationComponent.Instance.AddRawString(byteBuf);

            m_IsComplete = true;
        }
    }
}