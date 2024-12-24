using System;
using GameFramework.Event;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 事件绑定器功能拓展函数。
    /// </summary>
    public static class EventBinderFeatureExtensions
    {
        /// <summary>
        /// 注册事件。会在Owner销毁时自动取消注册。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要订阅的事件处理回调函数。</param>
        public static void Subscribe(this FeatureContainer container, int id, EventHandler<GameEventArgs> handler)
        {
            container.AddFeature<EventBinderFeature>().Subscribe(id, handler);
        }
        
        /// <summary>
        /// 取消订阅事件处理回调函数。
        /// </summary>
        /// <param name="container"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要取消订阅的事件处理回调函数。</param>
        public static void Unsubscribe(this FeatureContainer container, int id, EventHandler<GameEventArgs> handler)
        {
            var feature = container.GetFeature<EventBinderFeature>();
            if (feature == null)
            {
                return;
            }
            
            feature.Unsubscribe(id, handler);
        }
        
        /// <summary>
        /// 注册事件。会在Owner销毁时自动取消注册。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要订阅的事件处理回调函数。</param>
        public static void Subscribe(this IFeatureContainerOwner owner, int id, EventHandler<GameEventArgs> handler)
        {
            owner.FeatureContainer.Subscribe(id, handler);
        }
        
        /// <summary>
        /// 取消订阅事件处理回调函数。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要取消订阅的事件处理回调函数。</param>
        public static void Unsubscribe(this IFeatureContainerOwner owner, int id, EventHandler<GameEventArgs> handler)
        {
            owner.FeatureContainer.Unsubscribe(id, handler);
        }

        /// <summary>
        /// 派发事件(线程安全)
        /// </summary>
        public static void Fire(this IFeatureContainerOwner owner, GameEventArgs eventArgs)
        {
            EventComponent.Instance.Fire(owner, eventArgs);
        }

        /// <summary>
        /// 派发事件(线程安全)
        /// </summary>
        public static void Fire(this FeatureContainer container, GameEventArgs eventArgs)
        {
            EventComponent.Instance.Fire(container, eventArgs);
        }

        /// <summary>
        /// 立即派发事件(非线程安全)
        /// </summary>
        public static void FireNow(this IFeatureContainerOwner owner, GameEventArgs eventArgs)
        {
            EventComponent.Instance.FireNow(owner, eventArgs);
        }

        /// <summary>
        /// 立即派发事件(非线程安全)
        /// </summary>
        public static void FireNow(this FeatureContainer container, GameEventArgs eventArgs)
        {
            EventComponent.Instance.FireNow(container, eventArgs);
        }
    }
}