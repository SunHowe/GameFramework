using System;
using GameFramework.Event;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FGUI界面 事件绑定器拓展。
    /// </summary>
    public static class FGUIFormEventBinderExtensions
    {
        /// <summary>
        /// 注册事件。会在界面销毁时自动取消注册。
        /// </summary>
        /// <param name="fguiFormLogic"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要订阅的事件处理回调函数。</param>
        public static void SubscribeOnInit(this FGUIFormLogic fguiFormLogic, int id, EventHandler<GameEventArgs> handler)
        {
            fguiFormLogic.FeatureContainerOnInit.Subscribe(id, handler);
        }
        
        /// <summary>
        /// 取消订阅事件处理回调函数。
        /// </summary>
        /// <param name="fguiFormLogic"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要取消订阅的事件处理回调函数。</param>
        public static void UnsubscribeOnInit(this FGUIFormLogic fguiFormLogic, int id, EventHandler<GameEventArgs> handler)
        {
            fguiFormLogic.FeatureContainerOnInit.Unsubscribe(id, handler);
        }
        
        /// <summary>
        /// 注册事件。会在界面关闭时自动取消注册。
        /// </summary>
        /// <param name="fguiFormLogic"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要订阅的事件处理回调函数。</param>
        public static void SubscribeOnOpen(this FGUIFormLogic fguiFormLogic, int id, EventHandler<GameEventArgs> handler)
        {
            fguiFormLogic.FeatureContainerOnOpen.Subscribe(id, handler);
        }
        
        /// <summary>
        /// 取消订阅事件处理回调函数。
        /// </summary>
        /// <param name="fguiFormLogic"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要取消订阅的事件处理回调函数。</param>
        public static void UnsubscribeOnOpen(this FGUIFormLogic fguiFormLogic, int id, EventHandler<GameEventArgs> handler)
        {
            fguiFormLogic.FeatureContainerOnOpen.Unsubscribe(id, handler);
        }
        
        /// <summary>
        /// 注册事件。会在组件销毁时自动取消注册。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要订阅的事件处理回调函数。</param>
        public static void SubscribeOnInit(this IFGUICustomComponent owner, int id, EventHandler<GameEventArgs> handler)
        {
            owner.FeatureContainerOnInit.Subscribe(id, handler);
        }
        
        /// <summary>
        /// 取消订阅事件处理回调函数。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要取消订阅的事件处理回调函数。</param>
        public static void UnsubscribeOnInit(this IFGUICustomComponent owner, int id, EventHandler<GameEventArgs> handler)
        {
            owner.FeatureContainerOnInit.Unsubscribe(id, handler);
        }
        
        /// <summary>
        /// 注册事件。会在组件从舞台移除时自动取消注册。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要订阅的事件处理回调函数。</param>
        public static void SubscribeOnAddedToStage(this IFGUICustomComponent owner, int id, EventHandler<GameEventArgs> handler)
        {
            owner.FeatureContainerOnAddedToStage.Subscribe(id, handler);
        }
        
        /// <summary>
        /// 取消订阅事件处理回调函数。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要取消订阅的事件处理回调函数。</param>
        public static void UnsubscribeOnAddedToStage(this IFGUICustomComponent owner, int id, EventHandler<GameEventArgs> handler)
        {
            owner.FeatureContainerOnAddedToStage.Unsubscribe(id, handler);
        }
    }
}