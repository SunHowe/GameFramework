using System;
using FairyGUI;
using GameFramework.UI;
using UnityEngine;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FairyGUI界面。
    /// </summary>
    public sealed class FGUIForm : IUIForm
    {
        private int m_SerialId;
        private string m_UIFormAssetName;
        private IUIGroup m_UIGroup;
        private int m_DepthInUIGroup;
        private bool m_PauseCoveredUIForm;
        private FGUIFormLogic m_UIFormLogic;
        
        private readonly GComponent m_ContentPane;
        
        /// <summary>
        /// 获取界面序列编号。
        /// </summary>
        public int SerialId
        {
            get
            {
                return m_SerialId;
            }
        }

        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        public string UIFormAssetName
        {
            get
            {
                return m_UIFormAssetName;
            }
        }

        /// <summary>
        /// 获取界面实例。
        /// </summary>
        public object Handle
        {
            get
            {
                return ContentPane;
            }
        }

        /// <summary>
        /// 界面对应的FairyGUI组件。
        /// </summary>
        public GComponent ContentPane
        {
            get
            {
                return m_ContentPane;
            }
        }

        /// <summary>
        /// 对应的GameObject实例。
        /// </summary>
        public GameObject GameObject
        {
            get
            {
                return m_ContentPane.displayObject.gameObject;
            }
        }

        /// <summary>
        /// 获取界面所属的界面组。
        /// </summary>
        public IUIGroup UIGroup
        {
            get
            {
                return m_UIGroup;
            }
        }

        /// <summary>
        /// 获取界面深度。
        /// </summary>
        public int DepthInUIGroup
        {
            get
            {
                return m_DepthInUIGroup;
            }
        }

        /// <summary>
        /// 获取是否暂停被覆盖的界面。
        /// </summary>
        public bool PauseCoveredUIForm
        {
            get
            {
                return m_PauseCoveredUIForm;
            }
        }

        /// <summary>
        /// 获取界面逻辑。
        /// </summary>
        public FGUIFormLogic Logic
        {
            get
            {
                return m_UIFormLogic;
            }
        }

        public FGUIForm(GComponent contentPane)
        {
            m_ContentPane = contentPane;
            m_ContentPane.data = this;
        }

        /// <summary>
        /// 初始化界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroup">界面所处的界面组。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isNewInstance">是否是新实例。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void OnInit(int serialId, string uiFormAssetName, IUIGroup uiGroup, bool pauseCoveredUIForm, bool isNewInstance, object userData)
        {
            m_SerialId = serialId;
            m_UIFormAssetName = uiFormAssetName;
            m_UIGroup = uiGroup;
            m_DepthInUIGroup = 0;
            m_PauseCoveredUIForm = pauseCoveredUIForm;

            if (!isNewInstance)
            {
                return;
            }
            
            // 绑定界面逻辑
            var formLogicType = FGUIComponent.Instance.GetUIFormLogicType(uiFormAssetName);
            if (formLogicType == null)
            {
                Log.Error("UI form '{0}' can not get UI form logic type.", uiFormAssetName);
                return;
            }
            
            // 构造界面逻辑实例
            m_UIFormLogic = Activator.CreateInstance(formLogicType) as FGUIFormLogic;
            if (m_UIFormLogic == null)
            {
                Log.Error("UI form '{0}' can not get UI form logic.", uiFormAssetName);
                return;
            }
            m_UIFormLogic.ContentPane = m_ContentPane;

            try
            {
                m_UIFormLogic.Init(userData);
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnInit with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面回收。
        /// </summary>
        public void OnRecycle()
        {
            try
            {
                m_UIFormLogic.Recycle();
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnRecycle with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }

            m_SerialId = 0;
            m_DepthInUIGroup = 0;
            m_PauseCoveredUIForm = true;
        }

        /// <summary>
        /// 界面打开。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public void OnOpen(object userData)
        {
            ((FGUIGroupHelper)UIGroup.Helper).GroupRoot.AddChild(m_ContentPane);
            
            try
            {
                m_UIFormLogic.Open(userData);
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnOpen with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面关闭。
        /// </summary>
        /// <param name="isShutdown">是否是关闭界面管理器时触发。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void OnClose(bool isShutdown, object userData)
        {
            try
            {
                m_UIFormLogic.Close(isShutdown, userData);
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnClose with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }
            
            ((FGUIGroupHelper)UIGroup.Helper).GroupRoot.RemoveChild(m_ContentPane);
        }

        /// <summary>
        /// 界面暂停。
        /// </summary>
        public void OnPause()
        {
            try
            {
                m_UIFormLogic.OnPause();
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnPause with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面暂停恢复。
        /// </summary>
        public void OnResume()
        {
            try
            {
                m_UIFormLogic.OnResume();
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnResume with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面遮挡。
        /// </summary>
        public void OnCover()
        {
            try
            {
                m_UIFormLogic.OnCover();
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnCover with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面遮挡恢复。
        /// </summary>
        public void OnReveal()
        {
            try
            {
                m_UIFormLogic.OnReveal();
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnReveal with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面激活。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public void OnRefocus(object userData)
        {
            try
            {
                m_UIFormLogic.OnRefocus(userData);
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnRefocus with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            try
            {
                m_UIFormLogic.OnUpdate(elapseSeconds, realElapseSeconds);
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnUpdate with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <param name="uiGroupDepth">界面组深度。</param>
        /// <param name="depthInUIGroup">界面在界面组中的深度。</param>
        public void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            m_DepthInUIGroup = depthInUIGroup;
            m_ContentPane.sortingOrder = depthInUIGroup + 1;
            
            try
            {
                m_UIFormLogic.OnDepthChanged(uiGroupDepth, depthInUIGroup);
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnDepthChanged with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }
        }
    }
}