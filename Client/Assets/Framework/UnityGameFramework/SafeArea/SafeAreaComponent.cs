using GameFramework;
using GameFramework.Event;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 屏幕安全区域组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/SafeArea")]
    public sealed class SafeAreaComponent : GameFrameworkComponent<SafeAreaComponent>
    {
        /// <summary>
        /// 安全区域范围。
        /// </summary>
        public Rect SafeArea => m_SafeAreaHelper.SafeArea;
        
        /// <summary>
        /// 是否开启屏幕安全区域变化的事件通知。
        /// </summary>
        [SerializeField]
        private bool m_EnableSafeAreaChangeEvent = true;

        [SerializeField]
        private string m_SafeAreaHelperTypeName = "UnityGameFramework.Runtime.FairyGUI.DefaultFGUIFormHelper";

        [SerializeField]
        private SafeAreaHelperBase m_CustomSafeAreaHelper = null;

        private IEventManager m_EventManager = null;
        private ISafeAreaHelper m_SafeAreaHelper = null;

        protected override void Awake()
        {
            base.Awake();

            var safeAreaHelper = Helper.CreateHelper(m_SafeAreaHelperTypeName, m_CustomSafeAreaHelper);
            if (safeAreaHelper == null)
            {
                Log.Error("Can not create Safe Area helper.");
                return;
            }

            safeAreaHelper.name = "Safe Area Helper";
            Transform transform = safeAreaHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;

            m_SafeAreaHelper = safeAreaHelper;

            if (m_EnableSafeAreaChangeEvent)
            {
                m_SafeAreaHelper.OnSafeAreaChange += OnSafeAreaChange;
            }
        }

        private void Start()
        {
            if (m_EnableSafeAreaChangeEvent)
            {
                m_EventManager = GameFrameworkEntry.GetModule<IEventManager>();
            }
        }

        private void OnSafeAreaChange(object sender, SafeAreaChangeEventArgs e)
        {
            m_EventManager.Fire(this, SafeAreaChangeEventArgs.Create(e));
        }
    }
}