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
        public static void AddScreenAdaptor(this FGUIFormLogic fguiFormLogic, FGUIFormScreenAdaptorType adaptorType)
        {
            switch (adaptorType)
            {
                case FGUIFormScreenAdaptorType.Constant:
                    // 不用处理
                    break;
                case FGUIFormScreenAdaptorType.ConstantHorizontalCenter:
                    fguiFormLogic.AddFeature<FGUIFormConstantFeature>().Configure(isHorizontalCenter: true);
                    break;
                case FGUIFormScreenAdaptorType.ConstantVerticalCenter:
                    fguiFormLogic.AddFeature<FGUIFormConstantFeature>().Configure(isVerticalCenter: true);
                    break;
                case FGUIFormScreenAdaptorType.ConstantCenter:
                    fguiFormLogic.AddFeature<FGUIFormConstantFeature>().Configure(true, true);
                    break;
                case FGUIFormScreenAdaptorType.FullScreen:
                    fguiFormLogic.AddFeature<FGUIFormFullScreenFeature>();
                    break;
                case FGUIFormScreenAdaptorType.SafeAreaFullScreen:
                    fguiFormLogic.AddFeature<FGUIFormSafeAreaFeature>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(adaptorType), adaptorType, null);
            }
        }
    }
}