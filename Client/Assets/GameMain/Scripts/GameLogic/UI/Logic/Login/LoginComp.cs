using Cysharp.Threading.Tasks;

namespace GameLogic.UI.Login
{
    public partial class LoginComp
    {
        private void OnInitialize()
        {
            LoginButton.onClick.Add(LoginServer);
        }

        private void OnDispose()
        {
        }

        partial void OnAddedToStage()
        {
            LoginButton.text = "登录游戏"; // todo i18n
            ServerButton.text = "选择服务器"; // todo i18n
        }

        private void LoginServer()
        {
            // 登录服务器。
            LoginModule.Instance.RequestLogin(default, default, default).Forget();
        }
    }
}