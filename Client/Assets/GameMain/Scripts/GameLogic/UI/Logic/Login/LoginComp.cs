namespace GameLogic.UI.Login
{
    public partial class LoginComp
    {
        private void OnInitialize()
        {
        }

        private void OnDispose()
        {
        }

        partial void OnAddedToStage()
        {
            LoginButton.text = "登录游戏"; // todo i18n
            ServerButton.text = "选择服务器"; // todo i18n
        }
    }
}