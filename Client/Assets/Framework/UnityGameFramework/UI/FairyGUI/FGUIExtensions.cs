using System;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FGUI 扩展方法。
    /// </summary>
    public static class FGUIExtensions
    {
        /// <summary>
        /// 添加屏幕适配器。
        /// </summary>
        public static void AddScreenAdaptor(this FGUIFormLogic fguiFormLogic, UIFormScreenAdaptorType adaptorType)
        {
            switch (adaptorType)
            {
                case UIFormScreenAdaptorType.Constant:
                    // 不用处理
                    break;
                case UIFormScreenAdaptorType.ConstantHorizontalCenter:
                    fguiFormLogic.AddFeature<UIFormConstantFeature>().Configure(isHorizontalCenter: true);
                    break;
                case UIFormScreenAdaptorType.ConstantVerticalCenter:
                    fguiFormLogic.AddFeature<UIFormConstantFeature>().Configure(isVerticalCenter: true);
                    break;
                case UIFormScreenAdaptorType.ConstantCenter:
                    fguiFormLogic.AddFeature<UIFormConstantFeature>().Configure(true, true);
                    break;
                case UIFormScreenAdaptorType.FullScreen:
                    fguiFormLogic.AddFeature<UIFormFullScreenFeature>();
                    break;
                case UIFormScreenAdaptorType.SafeAreaFullScreen:
                    fguiFormLogic.AddFeature<UIFormSafeAreaFeature>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(adaptorType), adaptorType, null);
            }
        }
    }
}