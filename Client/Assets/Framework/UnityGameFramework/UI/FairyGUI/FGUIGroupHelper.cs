using FairyGUI;
using GameFramework.UI;

namespace UnityGameFramework.Runtime.FairyGUI
{
    /// <summary>
    /// FairyGUI界面组辅助器。
    /// </summary>
    public sealed class FGUIGroupHelper : IUIGroupHelper
    {
        private readonly GComponent m_GroupRoot;

        /// <summary>
        /// 界面组根节点的FairyGUI组件。
        /// </summary>
        public GComponent GroupRoot
        {
            get
            {
                return m_GroupRoot;
            }
        }

        public FGUIGroupHelper(string name, int depth)
        {
            var gRoot = GRoot.inst;
            
            m_GroupRoot = new GComponent();
            m_GroupRoot.name = name;
#if UNITY_EDITOR
            m_GroupRoot.gameObjectName = name;
#endif
            m_GroupRoot.MakeFullScreen();
            m_GroupRoot.AddRelation(gRoot, RelationType.Size);
            
            SetDepth(depth);
            gRoot.AddChild(m_GroupRoot);
        }

        /// <summary>
        /// 设置界面组深度。
        /// </summary>
        /// <param name="depth">深度。</param>
        public void SetDepth(int depth)
        {
            m_GroupRoot.sortingOrder = depth + 1;
        }
    }
}