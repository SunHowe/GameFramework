using FairyGUI;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FGUI界面 通用窗体框架的功能拓展。
    /// </summary>
    public static class FGUIFormFrameExtensions
    {
        /// <summary>
        /// 添加通用窗体框架。
        /// </summary>
        public static void AddFrameFeature(this FGUIFormLogic fguiFormLogic)
        {
            fguiFormLogic.FeatureContainerOnInit.AddFeature<FGUIFormFrameFeature>();
        }

        /// <summary>
        /// 为界面设置通用窗体框架的关闭按钮。
        /// </summary>
        public static void SetCloseButton(this FGUIFormLogic fguiFormLogic, GObject closeButton)
        {
            fguiFormLogic.FeatureContainerOnInit.AddFeature<FGUIFormFrameFeature>().CloseButton = closeButton;
        }

        /// <summary>
        /// 为界面设置通用窗体框架的返回按钮。
        /// </summary>
        public static void SetBackButton(this FGUIFormLogic fguiFormLogic, GObject backButton)
        {
            fguiFormLogic.FeatureContainerOnInit.AddFeature<FGUIFormFrameFeature>().BackButton = backButton;
        }

        /// <summary>
        /// 为界面设置通用窗体框架设置关闭按钮点击回调函数。
        /// </summary>
        public static void SetCloseButtonClickCallback(this FGUIFormLogic fguiFormLogic, EventCallback1 callback1)
        {
            fguiFormLogic.FeatureContainerOnInit.AddFeature<FGUIFormFrameFeature>().CloseButtonClickCallback = callback1;
        }

        /// <summary>
        /// 为界面设置通用窗体框架设置返回按钮点击回调函数。
        /// </summary>
        public static void SetBackButtonClickCallback(this FGUIFormLogic fguiFormLogic, EventCallback1 callback1)
        {
            fguiFormLogic.FeatureContainerOnInit.AddFeature<FGUIFormFrameFeature>().BackButtonClickCallback = callback1;
        }
    }
}