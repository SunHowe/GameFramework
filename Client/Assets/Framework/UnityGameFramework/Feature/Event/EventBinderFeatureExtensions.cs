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
        /// <param name="owner"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要订阅的事件处理回调函数。</param>
        public static void Subscribe(this IFeatureOwner owner, int id, EventHandler<GameEventArgs> handler)
        {
            owner.AddFeature<EventBinderFeature>().Subscribe(id, handler);
        }
        
        /// <summary>
        /// 取消订阅事件处理回调函数。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要取消订阅的事件处理回调函数。</param>
        public static void Unsubscribe(this IFeatureOwner owner, int id, EventHandler<GameEventArgs> handler)
        {
            owner.AddFeature<EventBinderFeature>().Unsubscribe(id, handler);
        }
    }
}