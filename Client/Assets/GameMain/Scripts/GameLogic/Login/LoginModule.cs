using System.Net;
using Cysharp.Threading.Tasks;
using GameFramework;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 登录模块。
    /// </summary>
    public sealed class LoginModule : GameLogicBase<LoginModule>
    {
        /// <summary>
        /// 登录状态。
        /// </summary>
        public LoginState State { get; private set; }
        
        /// <summary>
        /// 请求登录服务器。
        /// </summary>
        public async UniTask<int> RequestLogin(IPEndPoint server, string account, string password)
        {
            if (State != LoginState.None)
            {
                // 重复的登录请求。
                return (int)GameErrorCode.LoginRequestDuplicate;
            }
            
            // TODO 登录流程实现。
            State = LoginState.OnGame;
            
            // 初始化平台模块。
            GameLogicComponent.Instance.AddGameLogic<LobbyModule>();
            
            return 0;
        }

        /// <summary>
        /// 请求登出服务器。
        /// </summary>
        public async UniTask<int> RequestLogout()
        {
            if (State == LoginState.None)
            {
                // 未登录。
                return 0;
            }

            State = LoginState.None;
            
            // 销毁平台模块。
            GameLogicComponent.Instance.RemoveGameLogic(LobbyModule.Instance);
            
            // TODO 断开链接
            return 0;
        }
        
        protected override void OnAwake()
        {
            State = LoginState.None;
            
            // 添加登录子模块。
        }

        protected override void OnShutdown()
        {
        }
    }
}