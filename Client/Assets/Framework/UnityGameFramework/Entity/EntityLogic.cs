//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 实体逻辑基类。
    /// </summary>
    public abstract class EntityLogic : IFeatureContainerOwner
    {
        private bool m_Available = false;
        private bool m_Visible = false;
        private Entity m_Entity = null;
        private GameObject m_GameObject = null;
        private Transform m_CachedTransform = null;
        private int m_OriginalLayer = 0;
        private Transform m_OriginalTransform = null;
        private FeatureContainer m_FeatureContainer;

        /// <summary>
        /// 获取实体。
        /// </summary>
        public Entity Entity
        {
            get
            {
                return m_Entity;
            }
        }

        /// <summary>
        /// 获取或设置实体名称。
        /// </summary>
        public string Name
        {
            get
            {
                return m_GameObject.name;
            }
            set
            {
                m_GameObject.name = value;
            }
        }

        /// <summary>
        /// 获取实体是否可用。
        /// </summary>
        public bool Available
        {
            get
            {
                return m_Available;
            }
        }

        /// <summary>
        /// 获取或设置实体是否可见。
        /// </summary>
        public bool Visible
        {
            get
            {
                return m_Available && m_Visible;
            }
            set
            {
                if (!m_Available)
                {
                    Log.Warning("Entity '{0}' is not available.", Name);
                    return;
                }

                if (m_Visible == value)
                {
                    return;
                }

                m_Visible = value;
                InternalSetVisible(value);
            }
        }

        /// <summary>
        /// 获取已缓存的 Transform。
        /// </summary>
        public Transform CachedTransform
        {
            get
            {
                return m_CachedTransform;
            }
        }

        public FeatureContainer FeatureContainer => m_FeatureContainer ??= new FeatureContainer(this);

        /// <summary>
        /// 实体初始化。
        /// </summary>
        /// <param name="entity">实体对象。</param>
        /// <param name="userData">用户自定义数据。</param>
        internal void Init(Entity entity, object userData)
        {
            m_Entity = entity;
            m_GameObject = (GameObject)entity.Handle;
            m_CachedTransform = m_GameObject.transform;
            m_OriginalLayer = m_GameObject.layer;
            m_OriginalTransform = CachedTransform.parent;
            m_FeatureContainer?.Awake();
            
            OnInit(userData);
        }

        /// <summary>
        /// 实体回收。
        /// </summary>
        internal void Recycle()
        {
            OnRecycle();
            m_FeatureContainer?.Shutdown();
        }

        /// <summary>
        /// 实体显示。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        internal void Show(object userData)
        {
            m_Available = true;
            Visible = true;
            
            OnShow(userData);
        }

        /// <summary>
        /// 实体隐藏。
        /// </summary>
        /// <param name="isShutdown">是否是关闭实体管理器时触发。</param>
        /// <param name="userData">用户自定义数据。</param>
        internal void Hide(bool isShutdown, object userData)
        {
            OnHide(isShutdown, userData);
            
            m_GameObject.SetLayerRecursively(m_OriginalLayer);
            Visible = false;
            m_Available = false;
        }
        
        /// <summary>
        /// 实体初始化回调。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected virtual void OnInit(object userData)
        {
        }

        /// <summary>
        /// 实体回收回调。
        /// </summary>
        protected virtual void OnRecycle()
        {
        }

        /// <summary>
        /// 实体显示回调。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected virtual void OnShow(object userData)
        {
        }

        /// <summary>
        /// 实体隐藏回调。
        /// </summary>
        /// <param name="isShutdown">是否是关闭实体管理器时触发。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected virtual void OnHide(bool isShutdown, object userData)
        {
        }

        /// <summary>
        /// 实体附加子实体。
        /// </summary>
        /// <param name="childEntity">附加的子实体。</param>
        /// <param name="parentTransform">被附加父实体的位置。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected internal virtual void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
        }

        /// <summary>
        /// 实体解除子实体。
        /// </summary>
        /// <param name="childEntity">解除的子实体。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected internal virtual void OnDetached(EntityLogic childEntity, object userData)
        {
        }

        /// <summary>
        /// 实体附加子实体。
        /// </summary>
        /// <param name="parentEntity">被附加的父实体。</param>
        /// <param name="parentTransform">被附加父实体的位置。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected internal virtual void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            CachedTransform.SetParent(parentTransform);
        }

        /// <summary>
        /// 实体解除子实体。
        /// </summary>
        /// <param name="parentEntity">被解除的父实体。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected internal virtual void OnDetachFrom(EntityLogic parentEntity, object userData)
        {
            CachedTransform.SetParent(m_OriginalTransform);
        }

        /// <summary>
        /// 实体轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 设置实体的可见性。
        /// </summary>
        /// <param name="visible">实体的可见性。</param>
        protected virtual void InternalSetVisible(bool visible)
        {
            m_GameObject.SetActive(visible);
        }
    }
}
