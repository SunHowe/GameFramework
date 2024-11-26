using FairyGUI;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// 界面基础框架功能。
    /// </summary>
    public sealed class FGUIFormFrameFeature : Feature<FGUIFormLogic>
    {
        /// <summary>
        /// 界面的框架组件 约定节点名为frame的组件为框架组件
        /// </summary>
        public GComponent Frame { get; private set; }

        /// <summary>
        /// 界面的关闭按钮 当它被点击时会调用OnCloseButtonClick方法 约定框架组件下名为closeButton的组件为默认的关闭按钮
        /// </summary>
        public GObject CloseButton
        {
            get => m_CloseButton;
            set
            {
                if (m_CloseButton == value)
                    return;
                
                if (m_CloseButton != null)
                    m_CloseButton.onClick.Remove(OnCloseButtonClick);

                m_CloseButton = value;
                
                if (m_CloseButton != null)
                    m_CloseButton.onClick.Add(OnCloseButtonClick);
            }
        }

        public GObject BackButton
        {
            get => m_BackButton;
            set
            {
                if (m_BackButton == value)
                    return;
                
                if (m_BackButton != null)
                    m_BackButton.onClick.Remove(OnBackButtonClick);

                m_BackButton = value;
                
                if (m_BackButton != null)
                    m_BackButton.onClick.Add(OnBackButtonClick);
            }
        }
        
        public EventCallback1 CloseButtonClickCallback { get; set; }
        public EventCallback1 BackButtonClickCallback { get; set; }

        private GObject m_CloseButton;
        private GObject m_BackButton;

        public override void Awake(IFeatureOwner featureOwner)
        {
            base.Awake(featureOwner);
            
            Frame = Owner.ContentPane.GetChild("frame") as GComponent;
            if (Frame == null)
            {
                return;
            }
            
            CloseButton = Frame.GetChild("closeButton");
            BackButton = Frame.GetChild("backButton");
        }
        
        public override void Shutdown()
        {
            CloseButton = null;
            BackButton = null;
            Frame = null;
            CloseButtonClickCallback = null;
            BackButtonClickCallback = null;
        }

        /// <summary>
        /// 关闭按钮被点击时调用 默认实现为关闭界面
        /// </summary>
        public void OnCloseButtonClick(EventContext context)
        {
            if (CloseButtonClickCallback != null)
            {
                CloseButtonClickCallback(context);
                return;
            }

            Owner.CloseForm();
        }

        /// <summary>
        /// 返回按钮被点击时调用 默认实现为触发关闭按钮被点击的事件
        /// </summary>
        public void OnBackButtonClick(EventContext context)
        {
            if (BackButtonClickCallback != null)
            {
                BackButtonClickCallback(context);
                return;
            }
            
            OnCloseButtonClick(context);
        }
    }
}