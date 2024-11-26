using FairyGUI;
using UnityEngine;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// 固定尺寸界面适配器, 界面尺寸不随屏幕尺寸变化, 提供位置适配功能。
    /// </summary>
    internal class UIFormConstantFeature : Feature<FGUIFormLogic>
    {
        private bool m_IsHorizontalCenter;
        private bool m_IsVerticalCenter;

        public override void Shutdown()
        {
            var uiRoot = GRoot.inst;

            if (m_IsHorizontalCenter)
            {
                Owner.ContentPane.RemoveRelation(uiRoot, RelationType.Center_Center);
                m_IsHorizontalCenter = false;
            }

            if (m_IsVerticalCenter)
            {
                Owner.ContentPane.RemoveRelation(uiRoot, RelationType.Middle_Middle);
                m_IsVerticalCenter = false;
            }
        }

        /// <summary>
        /// 设置适配器。
        /// </summary>
        public void Configure(bool isHorizontalCenter = false, bool isVerticalCenter = false)
        {
            if (m_IsHorizontalCenter == isHorizontalCenter && m_IsVerticalCenter == isVerticalCenter)
            {
                return;
            }

            var uiRoot = GRoot.inst;

            var xy = Vector2.zero;

            if (m_IsHorizontalCenter)
            {
                xy.x = (uiRoot.width - Owner.ContentPane.width) / 2;
            }

            if (m_IsVerticalCenter)
            {
                xy.y = (uiRoot.height - Owner.ContentPane.height) / 2;
            }

            Owner.ContentPane.SetXY(xy.x, xy.y, true);

            if (m_IsHorizontalCenter != isHorizontalCenter)
            {
                if (isHorizontalCenter)
                {
                    Owner.ContentPane.AddRelation(uiRoot, RelationType.Center_Center);
                }
                else
                {
                    Owner.ContentPane.RemoveRelation(uiRoot, RelationType.Center_Center);
                }
            }

            if (m_IsVerticalCenter != isVerticalCenter)
            {
                if (isVerticalCenter)
                {
                    Owner.ContentPane.AddRelation(uiRoot, RelationType.Middle_Middle);
                }
                else
                {
                    Owner.ContentPane.RemoveRelation(uiRoot, RelationType.Middle_Middle);
                }
            }

            m_IsHorizontalCenter = isHorizontalCenter;
            m_IsVerticalCenter = isVerticalCenter;
        }
    }
}