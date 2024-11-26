using FairyGUI;
using GameFramework.Event;
using UnityEngine;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// 全屏界面适配器, 界面尺寸随屏幕尺寸变化, 提供安全区域适配功能 节点名固定为safeArea。
    /// </summary>
    internal class UIFormFullScreenFeature : Feature<FGUIFormLogic>
    {
        private GObject m_SafeAreaObject;
        
        public override void Awake(IFeatureOwner featureOwner)
        {
            base.Awake(featureOwner);

            var uiRoot = GRoot.inst;
            m_SafeAreaObject = Owner.ContentPane.GetChild("safeArea");

            Owner.ContentPane.size = uiRoot.size;
            Owner.ContentPane.xy = Vector2.zero;
            Owner.ContentPane.AddRelation(uiRoot, RelationType.Size);

            if (m_SafeAreaObject == null)
            {
                return;
            }
            
            m_SafeAreaObject.relations.ClearAll(); // 清除所有关联 由框架根据安全区域重新设置
            EventComponent.Instance.Subscribe(SafeAreaChangeEventArgs.EventId, OnSafeAreaChanged);
            
            UpdateSafeArea(SafeAreaComponent.Instance.SafeArea);
        }

        public override void Shutdown()
        {
            Owner.ContentPane.RemoveRelation(GRoot.inst, RelationType.Size);

            if (m_SafeAreaObject != null)
            {
                EventComponent.Instance.Unsubscribe(SafeAreaChangeEventArgs.EventId, OnSafeAreaChanged);
            }
        }

        private void OnSafeAreaChanged(object sender, GameEventArgs e)
        {
            var eventArgs = (SafeAreaChangeEventArgs)e;
            UpdateSafeArea(eventArgs.SafeArea);
        }

        private void UpdateSafeArea(Rect safeArea)
        {
            if (m_SafeAreaObject == null)
                return;

            m_SafeAreaObject.SetSize(safeArea.width, safeArea.height);
            m_SafeAreaObject.SetXY(safeArea.x, safeArea.y);
        }
    }
}