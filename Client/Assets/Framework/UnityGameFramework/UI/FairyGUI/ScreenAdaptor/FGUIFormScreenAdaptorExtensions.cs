using System;

namespace UnityGameFramework.Runtime.FairyGUI
{
    public static class FGUIFormScreenAdaptorExtensions
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
                    fguiFormLogic.FeatureContainerOnInit.AddFeature<FGUIFormConstantFeature>().Configure(isHorizontalCenter: true);
                    break;
                case FGUIFormScreenAdaptorType.ConstantVerticalCenter:
                    fguiFormLogic.FeatureContainerOnInit.AddFeature<FGUIFormConstantFeature>().Configure(isVerticalCenter: true);
                    break;
                case FGUIFormScreenAdaptorType.ConstantCenter:
                    fguiFormLogic.FeatureContainerOnInit.AddFeature<FGUIFormConstantFeature>().Configure(true, true);
                    break;
                case FGUIFormScreenAdaptorType.FullScreen:
                    fguiFormLogic.FeatureContainerOnInit.AddFeature<FGUIFormFullScreenFeature>();
                    break;
                case FGUIFormScreenAdaptorType.SafeAreaFullScreen:
                    fguiFormLogic.FeatureContainerOnInit.AddFeature<FGUIFormSafeAreaFeature>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(adaptorType), adaptorType, null);
            }
        }
    }
}