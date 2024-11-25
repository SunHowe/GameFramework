using FairyGUI;
using GameFramework.UI;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// 默认的FairyGUI界面辅助器。
    /// </summary>
    public sealed class DefaultFGUIFormHelper : UIFormHelperBase
    {
        /// <summary>
        /// 实例化界面。
        /// </summary>
        /// <param name="uiFormAsset">要实例化的界面资源。</param>
        /// <returns>实例化后的界面。</returns>
        public override object InstantiateUIForm(object uiFormAsset)
        {
            return uiFormAsset;// 这里实际上已经是构造完成的界面GComponent实例本身了。
        }

        /// <summary>
        /// 创建界面。
        /// </summary>
        /// <param name="uiFormInstance">界面实例。</param>
        /// <param name="uiGroup">界面所属的界面组。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面。</returns>
        public override IUIForm CreateUIForm(object uiFormInstance, IUIGroup uiGroup, object userData)
        {
            GComponent gComponent = uiFormInstance as GComponent;
            if (gComponent == null)
            {
                Log.Error("FUI form instance is invalid.");
                return null;
            }

            if (gComponent.data is FGUIForm fuiForm)
            {
                return fuiForm;
            }
            
            // 如果界面实例没有绑定FUIForm，则创建一个
            return new FGUIForm(gComponent);
        }

        /// <summary>
        /// 释放界面。
        /// </summary>
        /// <param name="uiFormAsset">要释放的界面资源。</param>
        /// <param name="uiFormInstance">要释放的界面实例。</param>
        public override void ReleaseUIForm(object uiFormAsset, object uiFormInstance)
        {
            GComponent gComponent = uiFormAsset as GComponent;
            if (gComponent == null)
            {
                return;
            }
            
            gComponent.Dispose();
        }
    }
}