using System;
using GameFramework;
using GameFramework.Event;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 事件组件拓展方法。
    /// </summary>
    public static class EventComponentExtensions
    {
        /// <summary>
        /// 订阅事件处理回调函数，并返回可用于解除绑定的IDisposable实例。
        /// </summary>
        /// <param name="component">事件组件</param>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要订阅的事件处理回调函数。</param>
        public static IDisposable SubscribeAsDisposable(this EventComponent component, int id, EventHandler<GameEventArgs> handler)
        {
            component.Subscribe(id, handler);
            
            var disposable = ReferencePool.Acquire<EventDisposable>();
            disposable.Handler = handler;
            disposable.Id = id;
            return disposable;
        }

        private sealed class EventDisposable : IDisposable, IReference
        {
            public int Id { get; set; }
            public EventHandler<GameEventArgs> Handler { get; set; }

            public void Dispose()
            {
                if (Handler == null)
                {
                    return;
                }

                EventComponent.Instance.Unsubscribe(Id, Handler);

                Id = 0;
                Handler = null;
                
                ReferencePool.Release(this);
            }

            public void Clear()
            {
            }
        }
    }
}