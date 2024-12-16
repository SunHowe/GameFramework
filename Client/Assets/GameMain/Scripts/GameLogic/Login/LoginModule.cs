using System.Net;
using Cysharp.Threading.Tasks;
using GameFramework;

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

            // 创建大厅玩法。
            GameModule.Instance.StartGame<LobbyGame>();
            
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
            
            // 销毁所有玩法。
            GameModule.Instance.StopAllGame();
            
            // TODO 断开链接
            return 0;
        }
        
        protected override void OnAwake()
        {
            State = LoginState.None;
        }

        protected override void OnShutdown()
        {
        }
    }
}