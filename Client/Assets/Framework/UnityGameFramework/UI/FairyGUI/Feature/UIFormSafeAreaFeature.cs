using GameFramework.Event;
using UnityEngine;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// 安全区域屏幕界面适配器, 界面尺寸始终与安全区域尺寸相同且位置始终与安全区域位置相同。
    /// </summary>
    internal class UIFormSafeAreaFeature : Feature<FGUIFormLogic>
    {
        public override void Awake(IFeatureOwner featureOwner)
        {
            base.Awake(featureOwner);
            
            EventComponent.Instance.Subscribe(SafeAreaChangeEventArgs.EventId, OnSafeAreaChanged);
            UpdateSafeArea(SafeAreaComponent.Instance.SafeArea);
        }

        public override void Shutdown()
        {
            EventComponent.Instance.Unsubscribe(SafeAreaChangeEventArgs.EventId, OnSafeAreaChanged);
        }

        private void OnSafeAreaChanged(object sender, GameEventArgs e)
        {
            var eventArgs = (SafeAreaChangeEventArgs)e;
            UpdateSafeArea(eventArgs.SafeArea);
        }

        private void UpdateSafeArea(Rect safeArea)
        {
            Owner.ContentPane.SetSize(safeArea.width, safeArea.height);
            Owner.ContentPane.SetXY(safeArea.x, safeArea.y);
        }
    }
}