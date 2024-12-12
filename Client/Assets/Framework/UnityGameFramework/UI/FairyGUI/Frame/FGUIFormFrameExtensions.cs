namespace UnityGameFramework.Runtime.FairyGUI
{
    public static class FGUIFormFrameExtensions
    {
        /// <summary>
        /// 添加通用窗体框架。
        /// </summary>
        public static void AddFrameFeature(this FGUIFormLogic fguiFormLogic)
        {
            fguiFormLogic.AddFeature<FGUIFormFrameFeature>();
        }

    }
}