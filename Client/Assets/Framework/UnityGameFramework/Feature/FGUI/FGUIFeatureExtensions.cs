using UnityGameFramework.Runtime.FairyGUI;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// FGUI界面功能拓展方法。
    /// </summary>
    public static class FGUIFeatureExtensions
    {
        /// <summary>
        /// 通过界面逻辑类型打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <returns>界面的序列编号。</returns>
        public static int OpenUIForm<T>(this IFeatureContainerOwner owner) where T : FGUIFormLogic
        {
            return FGUIComponent.Instance.OpenUIForm<T>();
        }

        /// <summary>
        /// 通过界面逻辑类型打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public static int OpenUIForm<T>(this IFeatureContainerOwner owner, object userData) where T : FGUIFormLogic
        {
            return FGUIComponent.Instance.OpenUIForm<T>(userData);
        }
        
        /// <summary>
        /// 通过界面逻辑类型打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <returns>界面的序列编号。</returns>
        public static int OpenUIForm<T>(this FeatureContainer owner) where T : FGUIFormLogic
        {
            return FGUIComponent.Instance.OpenUIForm<T>();
        }

        /// <summary>
        /// 通过界面逻辑类型打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public static int OpenUIForm<T>(this FeatureContainer owner, object userData) where T : FGUIFormLogic
        {
            return FGUIComponent.Instance.OpenUIForm<T>(userData);
        }
    }
}