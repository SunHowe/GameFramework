using System;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 事件绑定器功能。在功能被销毁时会自动解除通过绑定器绑定的事件。
    /// </summary>
    public sealed class EventBinderFeature : Feature
    {
        private EventComponent m_EventComponent;

        private readonly List<EventBindInfo> m_BindInfoList = new List<EventBindInfo>();
        
        public override void Awake(object featureOwner, FeatureContainer featureContainer)
        {
            base.Awake(featureOwner, featureContainer);
            m_EventComponent = EventComponent.Instance;
        }

        public override void Shutdown()
        {
            if (m_EventComponent == EventComponent.Instance)
            {
                // 解除事件绑定。
                foreach (var bindInfo in m_BindInfoList)
                {
                    m_EventComponent.Unsubscribe(bindInfo.Id, bindInfo.Handler);
                }
            }
            
            m_BindInfoList.Clear();
        }

        /// <summary>
        /// 订阅事件处理回调函数。
        /// </summary>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要订阅的事件处理回调函数。</param>
        public void Subscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventComponent.Subscribe(id, handler);
            m_BindInfoList.Add(EventBindInfo.Create(id, handler));
        }

        /// <summary>
        /// 取消订阅事件处理回调函数。
        /// </summary>
        /// <param name="id">事件类型编号。</param>
        /// <param name="handler">要取消订阅的事件处理回调函数。</param>
        public void Unsubscribe(int id, EventHandler<GameEventArgs> handler)
        {
            var index = m_BindInfoList.FindIndex(item => item.Id == id && item.Handler == handler);
            if (index < 0)
            {
                return;
            }

            var bindInfo = m_BindInfoList[index];
            m_BindInfoList.RemoveAt(index);
            ReferencePool.Release(bindInfo);
            
            m_EventComponent.Unsubscribe(id, handler);
        }

        private sealed class EventBindInfo : IReference
        {
            public int Id { get; private set; }
            public EventHandler<GameEventArgs> Handler { get; private set; }
            
            public void Clear()
            {
                Id = 0;
                Handler = null;
            }

            public static EventBindInfo Create(int id, EventHandler<GameEventArgs> handler)
            {
                EventBindInfo eventBindInfo = ReferencePool.Acquire<EventBindInfo>();
                eventBindInfo.Id = id;
                eventBindInfo.Handler = handler;
                return eventBindInfo;
            }
        }
    }
}