using FairyGUI;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FairyGUI界面逻辑基类。
    /// </summary>
    public abstract class FGUIFormLogic
    {
        private bool m_Available = false;
        private bool m_Visible = false;
        private FGUIForm m_UIForm = null;
        
        private GComponent m_ContentPane = null;
        private FeatureContainer m_FeatureContainerOnInit = null;
        private FeatureContainer m_FeatureContainerOnOpen = null;

        /// <summary>
        /// 获取界面。
        /// </summary>
        public FGUIForm UIForm
        {
            get
            {
                return m_UIForm;
            }
        }

        /// <summary>
        /// 获取或设置界面名称。
        /// </summary>
        public string Name
        {
            get
            {
                return m_ContentPane.name;
            }
            set
            {
                m_ContentPane.name = value;
            }
        }

        /// <summary>
        /// 获取界面是否可用。
        /// </summary>
        public bool Available
        {
            get
            {
                return m_Available;
            }
        }

        /// <summary>
        /// 获取或设置界面是否可见。
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
                    Log.Warning("UI form '{0}' is not available.", Name);
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
        /// 获取界面FairyGUI组件实例。
        /// </summary>
        public GComponent ContentPane
        {
            get
            {
                return m_ContentPane;
            }
            internal set
            {
                m_ContentPane = value;
            }
        }

        /// <summary>
        /// 功能容器。在OnRecycle阶段销毁。
        /// </summary>
        public FeatureContainer FeatureContainerOnInit => m_FeatureContainerOnInit ??= new FeatureContainer(this);
        
        /// <summary>
        /// 界面打开期间才生效的功能容器。在OnClose阶段销毁。
        /// </summary>
        public FeatureContainer FeatureContainerOnOpen => m_FeatureContainerOnOpen ??= new FeatureContainer(this);

        /// <summary>
        /// 界面初始化。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        internal void Init(object userData)
        {
            m_UIForm = m_ContentPane.data as FGUIForm;
            m_FeatureContainerOnInit?.Awake();
            OnInit(userData);
        }

        /// <summary>
        /// 界面回收。
        /// </summary>
        internal void Recycle()
        {
            OnRecycle();
            m_FeatureContainerOnInit?.Shutdown();
        }
        
        /// <summary>
        /// 界面打开。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        internal void Open(object userData)
        {
            m_FeatureContainerOnOpen?.Awake();
            
            OnOpen(userData);
        }

        /// <summary>
        /// 界面关闭。
        /// </summary>
        /// <param name="isShutdown">是否是关闭界面管理器时触发。</param>
        /// <param name="userData">用户自定义数据。</param>
        internal void Close(bool isShutdown, object userData)
        {
            OnClose(isShutdown, userData);
            
            m_FeatureContainerOnOpen?.Shutdown();
        }

        /// <summary>
        /// 界面初始化。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected virtual void OnInit(object userData)
        {
        }

        /// <summary>
        /// 界面回收。
        /// </summary>
        protected virtual void OnRecycle()
        {
        }

        /// <summary>
        /// 界面打开。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected virtual void OnOpen(object userData)
        {
            m_Available = true;
            Visible = true;
        }

        /// <summary>
        /// 界面关闭。
        /// </summary>
        /// <param name="isShutdown">是否是关闭界面管理器时触发。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected virtual void OnClose(bool isShutdown, object userData)
        {
            Visible = false;
            m_Available = false;
        }

        /// <summary>
        /// 界面暂停。
        /// </summary>
        protected internal virtual void OnPause()
        {
            Visible = false;
        }

        /// <summary>
        /// 界面暂停恢复。
        /// </summary>
        protected internal virtual void OnResume()
        {
            Visible = true;
        }

        /// <summary>
        /// 界面遮挡。
        /// </summary>
        protected internal virtual void OnCover()
        {
        }

        /// <summary>
        /// 界面遮挡恢复。
        /// </summary>
        protected internal virtual void OnReveal()
        {
        }

        /// <summary>
        /// 界面激活。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected internal virtual void OnRefocus(object userData)
        {
        }

        /// <summary>
        /// 界面轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <param name="uiGroupDepth">界面组深度。</param>
        /// <param name="depthInUIGroup">界面在界面组中的深度。</param>
        protected internal virtual void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
        }

        /// <summary>
        /// 设置界面的可见性。
        /// </summary>
        /// <param name="visible">界面的可见性。</param>
        protected virtual void InternalSetVisible(bool visible)
        {
            m_ContentPane.visible = visible;
        }
    }
}