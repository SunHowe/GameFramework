//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using GameFramework;
using GameFramework.ObjectPool;
using GameFramework.Resource;
using GameFramework.UI;
using UnityEngine;

namespace UnityGameFramework.Runtime.UGUI
{
    /// <summary>
    /// 基于UGUI的界面组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/UGUI")]
    public sealed class UGUIComponent : GameFrameworkComponent<UGUIComponent>
    {
        private const int DefaultPriority = 0;

        private IUIManager m_UIManager = null;
        private EventComponent m_EventComponent = null;

        private readonly List<IUIForm> m_InternalUIFormResults = new List<IUIForm>();

        [SerializeField]
        private bool m_EnableOpenUIFormSuccessEvent = true;

        [SerializeField]
        private bool m_EnableOpenUIFormFailureEvent = true;

        [SerializeField]
        private bool m_EnableOpenUIFormUpdateEvent = false;

        [SerializeField]
        private bool m_EnableOpenUIFormDependencyAssetEvent = false;

        [SerializeField]
        private bool m_EnableCloseUIFormCompleteEvent = true;

        [SerializeField]
        private float m_InstanceAutoReleaseInterval = 60f;

        [SerializeField]
        private int m_InstanceCapacity = 16;

        [SerializeField]
        private float m_InstanceExpireTime = 60f;

        [SerializeField]
        private int m_InstancePriority = 0;

        [SerializeField]
        private bool m_EnableUIFormOpenQueue;

        [SerializeField]
        private Transform m_InstanceRoot = null;

        [SerializeField]
        private string m_UIFormHelperTypeName = "UnityGameFramework.Runtime.UGUI.DefaultUGUIFormHelper";

        [SerializeField]
        private UIFormHelperBase m_CustomUIFormHelper = null;

        [SerializeField]
        private string m_UIFormAssetHelperTypeName = "UnityGameFramework.Runtime.UGUI.DefaultUGUIFormAssetHelper";
        
        [SerializeField]
        private UIFormAssetHelperBase m_CustomUIFormAssetHelper = null;

        [SerializeField]
        private string m_UIGroupHelperTypeName = "UnityGameFramework.Runtime.UGUI.DefaultUGUIGroupHelper";

        [SerializeField]
        private UIGroupHelperBase m_CustomUIGroupHelper = null;

        [SerializeField]
        private UIGroup[] m_UIGroups = null;

        private readonly Dictionary<string, UIFormBindingInfo> m_UIFormBindingInfoDict = new Dictionary<string, UIFormBindingInfo>();
        private readonly Dictionary<Type, UIFormBindingInfo> m_UIFormBindingInfoByLogicTypeDict = new Dictionary<Type, UIFormBindingInfo>();

        /// <summary>
        /// 获取界面组数量。
        /// </summary>
        public int UIGroupCount
        {
            get
            {
                return m_UIManager.UIGroupCount;
            }
        }

        /// <summary>
        /// 获取或设置界面实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        public float InstanceAutoReleaseInterval
        {
            get
            {
                return m_UIManager.InstanceAutoReleaseInterval;
            }
            set
            {
                m_UIManager.InstanceAutoReleaseInterval = m_InstanceAutoReleaseInterval = value;
            }
        }

        /// <summary>
        /// 获取或设置界面实例对象池的容量。
        /// </summary>
        public int InstanceCapacity
        {
            get
            {
                return m_UIManager.InstanceCapacity;
            }
            set
            {
                m_UIManager.InstanceCapacity = m_InstanceCapacity = value;
            }
        }

        /// <summary>
        /// 获取或设置界面实例对象池对象过期秒数。
        /// </summary>
        public float InstanceExpireTime
        {
            get
            {
                return m_UIManager.InstanceExpireTime;
            }
            set
            {
                m_UIManager.InstanceExpireTime = m_InstanceExpireTime = value;
            }
        }

        /// <summary>
        /// 获取或设置界面实例对象池的优先级。
        /// </summary>
        public int InstancePriority
        {
            get
            {
                return m_UIManager.InstancePriority;
            }
            set
            {
                m_UIManager.InstancePriority = m_InstancePriority = value;
            }
        }

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            m_UIManager = GameFrameworkEntry.GetModule<IUIManager>();
            if (m_UIManager == null)
            {
                Log.Fatal("UI manager is invalid.");
                return;
            }

            if (m_EnableOpenUIFormSuccessEvent)
            {
                m_UIManager.OpenUIFormSuccess += OnOpenUIFormSuccess;
            }

            m_UIManager.OpenUIFormFailure += OnOpenUIFormFailure;

            if (m_EnableOpenUIFormUpdateEvent)
            {
                m_UIManager.OpenUIFormUpdate += OnOpenUIFormUpdate;
            }

            if (m_EnableOpenUIFormDependencyAssetEvent)
            {
                m_UIManager.OpenUIFormDependencyAsset += OnOpenUIFormDependencyAsset;
            }

            if (m_EnableCloseUIFormCompleteEvent)
            {
                m_UIManager.CloseUIFormComplete += OnCloseUIFormComplete;
            }
        }

        private void Start()
        {
            BaseComponent baseComponent = GameEntry.GetComponent<BaseComponent>();
            if (baseComponent == null)
            {
                Log.Fatal("Base component is invalid.");
                return;
            }

            m_EventComponent = GameEntry.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }

            m_UIManager.SetObjectPoolManager(GameFrameworkEntry.GetModule<IObjectPoolManager>());
            m_UIManager.InstanceAutoReleaseInterval = m_InstanceAutoReleaseInterval;
            m_UIManager.InstanceCapacity = m_InstanceCapacity;
            m_UIManager.InstanceExpireTime = m_InstanceExpireTime;
            m_UIManager.InstancePriority = m_InstancePriority;

            if (m_EnableUIFormOpenQueue)
            {
                m_UIManager.EnableOpenUIQueueMode();
            }

            UIFormHelperBase uiFormHelper = Helper.CreateHelper(m_UIFormHelperTypeName, m_CustomUIFormHelper);
            if (uiFormHelper == null)
            {
                Log.Error("Can not create UI form helper.");
                return;
            }

            uiFormHelper.name = "UI Form Helper";
            Transform transform = uiFormHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;

            m_UIManager.SetUIFormHelper(uiFormHelper);
            
            UIFormAssetHelperBase uiFormAssetHelper = Helper.CreateHelper(m_UIFormAssetHelperTypeName, m_CustomUIFormAssetHelper);
            if (uiFormAssetHelper == null)
            {
                Log.Error("Can not create UI form asset helper.");
                return;
            }
            
            uiFormAssetHelper.name = "UI Form Asset Helper";
            transform = uiFormAssetHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;
            
            m_UIManager.SetUIFormAssetHelper(uiFormAssetHelper);

            if (m_InstanceRoot == null)
            {
                m_InstanceRoot = new GameObject("UI Form Instances").transform;
                m_InstanceRoot.SetParent(gameObject.transform);
                m_InstanceRoot.localScale = Vector3.one;
            }

            m_InstanceRoot.gameObject.layer = LayerMask.NameToLayer("UI");

            for (int i = 0; i < m_UIGroups.Length; i++)
            {
                if (!AddUIGroup(m_UIGroups[i].Name, m_UIGroups[i].Depth))
                {
                    Log.Warning("Add UI group '{0}' failure.", m_UIGroups[i].Name);
                    continue;
                }
            }
        }

        /// <summary>
        /// 是否存在界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>是否存在界面组。</returns>
        public bool HasUIGroup(string uiGroupName)
        {
            return m_UIManager.HasUIGroup(uiGroupName);
        }

        /// <summary>
        /// 获取界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>要获取的界面组。</returns>
        public IUIGroup GetUIGroup(string uiGroupName)
        {
            return m_UIManager.GetUIGroup(uiGroupName);
        }

        /// <summary>
        /// 获取所有界面组。
        /// </summary>
        /// <returns>所有界面组。</returns>
        public IUIGroup[] GetAllUIGroups()
        {
            return m_UIManager.GetAllUIGroups();
        }

        /// <summary>
        /// 获取所有界面组。
        /// </summary>
        /// <param name="results">所有界面组。</param>
        public void GetAllUIGroups(List<IUIGroup> results)
        {
            m_UIManager.GetAllUIGroups(results);
        }

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>是否增加界面组成功。</returns>
        public bool AddUIGroup(string uiGroupName)
        {
            return AddUIGroup(uiGroupName, 0);
        }

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="depth">界面组深度。</param>
        /// <returns>是否增加界面组成功。</returns>
        public bool AddUIGroup(string uiGroupName, int depth)
        {
            if (m_UIManager.HasUIGroup(uiGroupName))
            {
                return false;
            }

            UIGroupHelperBase uiGroupHelper = Helper.CreateHelper(m_UIGroupHelperTypeName, m_CustomUIGroupHelper, UIGroupCount);
            if (uiGroupHelper == null)
            {
                Log.Error("Can not create UI group helper.");
                return false;
            }

            uiGroupHelper.name = Utility.Text.Format("UI Group - {0}", uiGroupName);
            uiGroupHelper.gameObject.layer = LayerMask.NameToLayer("UI");
            Transform transform = uiGroupHelper.transform;
            transform.SetParent(m_InstanceRoot);
            transform.localScale = Vector3.one;

            return m_UIManager.AddUIGroup(uiGroupName, depth, uiGroupHelper);
        }

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>是否存在界面。</returns>
        public bool HasUIForm(int serialId)
        {
            return m_UIManager.HasUIForm(serialId);
        }

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>是否存在界面。</returns>
        public bool HasUIForm(string uiFormAssetName)
        {
            return m_UIManager.HasUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>要获取的界面。</returns>
        public UGUIForm GetUIForm(int serialId)
        {
            return (UGUIForm)m_UIManager.GetUIForm(serialId);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>要获取的界面。</returns>
        public UGUIForm GetUIForm(string uiFormAssetName)
        {
            return (UGUIForm)m_UIManager.GetUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>要获取的界面。</returns>
        public UGUIForm[] GetUIForms(string uiFormAssetName)
        {
            IUIForm[] uiForms = m_UIManager.GetUIForms(uiFormAssetName);
            UGUIForm[] uiFormImpls = new UGUIForm[uiForms.Length];
            for (int i = 0; i < uiForms.Length; i++)
            {
                uiFormImpls[i] = (UGUIForm)uiForms[i];
            }

            return uiFormImpls;
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="results">要获取的界面。</param>
        public void GetUIForms(string uiFormAssetName, List<UGUIForm> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
                return;
            }

            results.Clear();
            m_UIManager.GetUIForms(uiFormAssetName, m_InternalUIFormResults);
            foreach (IUIForm uiForm in m_InternalUIFormResults)
            {
                results.Add((UGUIForm)uiForm);
            }
        }

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <returns>所有已加载的界面。</returns>
        public UGUIForm[] GetAllLoadedUIForms()
        {
            IUIForm[] uiForms = m_UIManager.GetAllLoadedUIForms();
            UGUIForm[] uiFormImpls = new UGUIForm[uiForms.Length];
            for (int i = 0; i < uiForms.Length; i++)
            {
                uiFormImpls[i] = (UGUIForm)uiForms[i];
            }

            return uiFormImpls;
        }

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <param name="results">所有已加载的界面。</param>
        public void GetAllLoadedUIForms(List<UGUIForm> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
                return;
            }

            results.Clear();
            m_UIManager.GetAllLoadedUIForms(m_InternalUIFormResults);
            foreach (IUIForm uiForm in m_InternalUIFormResults)
            {
                results.Add((UGUIForm)uiForm);
            }
        }

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <returns>所有正在加载界面的序列编号。</returns>
        public int[] GetAllLoadingUIFormSerialIds()
        {
            return m_UIManager.GetAllLoadingUIFormSerialIds();
        }

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <param name="results">所有正在加载界面的序列编号。</param>
        public void GetAllLoadingUIFormSerialIds(List<int> results)
        {
            m_UIManager.GetAllLoadingUIFormSerialIds(results);
        }

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>是否正在加载界面。</returns>
        public bool IsLoadingUIForm(int serialId)
        {
            return m_UIManager.IsLoadingUIForm(serialId);
        }

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>是否正在加载界面。</returns>
        public bool IsLoadingUIForm(string uiFormAssetName)
        {
            return m_UIManager.IsLoadingUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 是否是合法的界面。
        /// </summary>
        /// <param name="uiForm">界面。</param>
        /// <returns>界面是否合法。</returns>
        public bool IsValidUIForm(UGUIForm uiForm)
        {
            return m_UIManager.IsValidUIForm(uiForm);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, DefaultPriority, false, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, priority, false, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, DefaultPriority, pauseCoveredUIForm, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, object userData)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, DefaultPriority, false, userData);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority, object userData)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, priority, false, userData);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, object userData)
        {
            return OpenUIForm(uiFormAssetName, uiGroupName, DefaultPriority, pauseCoveredUIForm, userData);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm, object userData)
        {
            return m_UIManager.OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, userData);
        }
        

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, DefaultPriority, false, null);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, int priority)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, priority, false, null);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, DefaultPriority, pauseCoveredUIForm, null);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, object userData)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, DefaultPriority, false, userData);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, null);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, int priority, object userData)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, priority, false, userData);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, object userData)
        {
            return OpenSingletonUIForm(uiFormAssetName, uiGroupName, DefaultPriority, pauseCoveredUIForm, userData);
        }

        /// <summary>
        /// 打开单例型界面，若界面未打开，则打开新的实例；若界面已处于打开状态，则将其激活。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenSingletonUIForm(string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm, object userData)
        {
            IUIForm form = m_UIManager.GetUIForm(uiFormAssetName);
            if (form != null)
            {
                m_UIManager.RefocusUIForm(form, userData);
                return form.SerialId;
            }
            
            return m_UIManager.OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, userData);
        }

        /// <summary>
        /// 打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName)
        {
            return OpenUIForm(uiFormAssetName, null);
        }

        /// <summary>
        /// 打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm(string uiFormAssetName, object userData)
        {
            var bindingInfo = GetUIFormBindingInfo(uiFormAssetName);
            if (bindingInfo == null)
            {
                Log.Error("Cannot find binding info of UIForm '{0}'.", uiFormAssetName);
                return 0;
            }

            if (bindingInfo.AllowMultiple)
            {
                return OpenUIForm(bindingInfo.UIFormAssetName, bindingInfo.UIGroupName, bindingInfo.Priority, bindingInfo.PauseCoveredUIForm, userData);
            }
            else
            {
                return OpenSingletonUIForm(bindingInfo.UIFormAssetName, bindingInfo.UIGroupName, bindingInfo.Priority, bindingInfo.PauseCoveredUIForm, userData);
            }
        }

        /// <summary>
        /// 通过界面逻辑类型打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm<T>() where T : UGUIFormLogic
        {
            return OpenUIForm<T>(null);
        }

        /// <summary>
        /// 通过界面逻辑类型打开界面，要求该界面已经完成界面信息绑定。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public int OpenUIForm<T>(object userData) where T : UGUIFormLogic
        {
            var bindingInfo = GetUIFormBindingInfo(typeof(T));
            if (bindingInfo == null)
            {
                Log.Error("Cannot find binding info of UIForm '{0}'.", typeof(T).Name);
                return 0;
            }

            if (bindingInfo.AllowMultiple)
            {
                return OpenUIForm(bindingInfo.UIFormAssetName, bindingInfo.UIGroupName, bindingInfo.Priority, bindingInfo.PauseCoveredUIForm, userData);
            }
            else
            {
                return OpenSingletonUIForm(bindingInfo.UIFormAssetName, bindingInfo.UIGroupName, bindingInfo.Priority, bindingInfo.PauseCoveredUIForm, userData);
            }
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        public void CloseUIForm(int serialId)
        {
            m_UIManager.CloseUIForm(serialId);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseUIForm(int serialId, object userData)
        {
            m_UIManager.CloseUIForm(serialId, userData);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        public void CloseUIForm(UGUIForm uiForm)
        {
            m_UIManager.CloseUIForm(uiForm);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseUIForm(UGUIForm uiForm, object userData)
        {
            m_UIManager.CloseUIForm(uiForm, userData);
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        public void CloseAllLoadedUIForms()
        {
            m_UIManager.CloseAllLoadedUIForms();
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseAllLoadedUIForms(object userData)
        {
            m_UIManager.CloseAllLoadedUIForms(userData);
        }

        /// <summary>
        /// 关闭所有正在加载的界面。
        /// </summary>
        public void CloseAllLoadingUIForms()
        {
            m_UIManager.CloseAllLoadingUIForms();
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        public void RefocusUIForm(UGUIForm uiForm)
        {
            m_UIManager.RefocusUIForm(uiForm);
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void RefocusUIForm(UGUIForm uiForm, object userData)
        {
            m_UIManager.RefocusUIForm(uiForm, userData);
        }

        /// <summary>
        /// 设置界面是否被加锁。
        /// </summary>
        /// <param name="uiForm">要设置是否被加锁的界面。</param>
        /// <param name="locked">界面是否被加锁。</param>
        public void SetUIFormInstanceLocked(UGUIForm uiForm, bool locked)
        {
            if (uiForm == null)
            {
                Log.Warning("UI form is invalid.");
                return;
            }

            m_UIManager.SetUIFormInstanceLocked(uiForm.gameObject, locked);
        }

        /// <summary>
        /// 设置界面的优先级。
        /// </summary>
        /// <param name="uiForm">要设置优先级的界面。</param>
        /// <param name="priority">界面优先级。</param>
        public void SetUIFormInstancePriority(UGUIForm uiForm, int priority)
        {
            if (uiForm == null)
            {
                Log.Warning("UI form is invalid.");
                return;
            }

            m_UIManager.SetUIFormInstancePriority(uiForm.gameObject, priority);
        }

        /// <summary>
        /// 通过反射进行界面逻辑类型与界面信息的绑定。需要界面逻辑类型标注了UIFormAttribute特性。
        /// </summary>
        /// <param name="uiFormLogicType">界面逻辑类型。</param>
        public void RegisterUIFormBinding(Type uiFormLogicType)
        {
            var attribute = uiFormLogicType.GetCustomAttribute<UIFormAttribute>(false);
            if (attribute == null)
            {
                Log.Error("UIFormAttribute is invalid.");
                return;
            }

            var bindingInfo = new UIFormBindingInfo(attribute, uiFormLogicType);
            m_UIFormBindingInfoDict.Add(attribute.UIFormAssetName, bindingInfo);
            m_UIFormBindingInfoByLogicTypeDict.Add(uiFormLogicType, bindingInfo);
        }

        /// <summary>
        /// 通过反射进行界面逻辑类型与界面信息的绑定。需要界面逻辑类型标注了UIFormAttribute特性。
        /// </summary>
        public void RegisterUIFormBinding<T>() where T : UGUIFormLogic
        {
            RegisterUIFormBinding(typeof(T));
        }

        /// <summary>
        /// 获取界面绑定信息。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>绑定信息实例。</returns>
        public UIFormBindingInfo GetUIFormBindingInfo(string uiFormAssetName)
        {
            return m_UIFormBindingInfoDict.TryGetValue(uiFormAssetName, out var bindingInfo) ? bindingInfo : null;
        }

        /// <summary>
        /// 通过逻辑类型获取界面绑定信息。
        /// </summary>
        /// <param name="uiFormLogicType">界面逻辑类型。</param>
        /// <returns>绑定信息实例。</returns>
        public UIFormBindingInfo GetUIFormBindingInfo(Type uiFormLogicType)
        {
            return m_UIFormBindingInfoByLogicTypeDict.TryGetValue(uiFormLogicType, out var bindingInfo) ? bindingInfo : null;
        }

        /// <summary>
        /// 通过逻辑类型获取界面资源名。
        /// </summary>
        /// <typeparam name="T">界面逻辑类型。</typeparam>
        /// <returns>界面资源名。</returns>
        public string GetUIFormAssetName<T>() where T : UGUIFormLogic
        {
            return GetUIFormBindingInfo(typeof(T))?.UIFormAssetName ?? string.Empty;
        }

        /// <summary>
        /// 获取界面逻辑类型。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>逻辑类型。</returns>
        public Type GetFormLogicType(string uiFormAssetName)
        {
            return GetUIFormBindingInfo(uiFormAssetName)?.FormLogicType;
        }

        private void OnOpenUIFormSuccess(object sender, GameFramework.UI.OpenUIFormSuccessEventArgs e)
        {
            m_EventComponent.Fire(this, OpenUIFormSuccessEventArgs.Create(e));
        }

        private void OnOpenUIFormFailure(object sender, GameFramework.UI.OpenUIFormFailureEventArgs e)
        {
            Log.Warning("Open UI form failure, asset name '{0}', UI group name '{1}', pause covered UI form '{2}', error message '{3}'.", e.UIFormAssetName, e.UIGroupName, e.PauseCoveredUIForm, e.ErrorMessage);
            if (m_EnableOpenUIFormFailureEvent)
            {
                m_EventComponent.Fire(this, OpenUIFormFailureEventArgs.Create(e));
            }
        }

        private void OnOpenUIFormUpdate(object sender, GameFramework.UI.OpenUIFormUpdateEventArgs e)
        {
            m_EventComponent.Fire(this, OpenUIFormUpdateEventArgs.Create(e));
        }

        private void OnOpenUIFormDependencyAsset(object sender, GameFramework.UI.OpenUIFormDependencyAssetEventArgs e)
        {
            m_EventComponent.Fire(this, OpenUIFormDependencyAssetEventArgs.Create(e));
        }

        private void OnCloseUIFormComplete(object sender, GameFramework.UI.CloseUIFormCompleteEventArgs e)
        {
            m_EventComponent.Fire(this, CloseUIFormCompleteEventArgs.Create(e));
        }
    }
}
