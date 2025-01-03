//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Threading;
using UnityGameFramework.Runtime;
using UnityGameFramework.Runtime.FairyGUI;

namespace GameLogic.UI.{{ package_name }}
{
    [FGUICustomComponent(URL)]
    public partial class {{ name }} : {{ extension_type.full_name }}, IFGUICustomComponent, IFeatureContainerOwner
    {
        public const string URL = "{{ url }}";

        #region [子节点]
{{ for node in nodes }} {{ if is_accept_name node.name }}
        public {{ node.ref_type_full_name }} {{ upper_first node.name }};
{{ end }} {{ end }}
        #endregion

        #region [控制器]
{{ for controller in controllers }} {{ if is_accept_name controller.name }}
        public FairyGUI.Controller {{ upper_first controller.name }}Controller;

        public enum {{ upper_first controller.name }}ControllerPage
        { {{ for page in controller.pages }}
            Page{{ upper_first page }},{{ end }}
        }

        public {{ upper_first controller.name }}ControllerPage Current{{ upper_first controller.name }}ControllerPage
        {
            get => ({{ upper_first controller.name }}ControllerPage){{ upper_first controller.name }}Controller.selectedIndex;
            set => {{ upper_first controller.name }}Controller.selectedIndex = (int)value;
        }
{{ end }} {{ end }}
        #endregion

        #region [动效]
{{ for transition in transitions }} {{ if is_accept_name transition.name }}
        public FairyGUI.Transition {{ upper_first transition.name }}Transition;
{{ end }} {{ end }}
        #endregion

        #region [FeatureContainer]
        
        /// <summary>
        /// 默认功能容器。指向FeatureContainerOnAddedToStage。
        /// </summary>
        public FeatureContainer FeatureContainer => FeatureContainerOnAddedToStage;
        
        /// <summary>
        /// 功能容器。在组件销毁时会跟着被销毁。
        /// </summary>
        public FeatureContainer FeatureContainerOnInit => m_FeatureContainerOnInit ??= new FeatureContainer(this);
        
        /// <summary>
        /// 功能容器。在组件从舞台移除时会跟着被销毁。
        /// </summary>
        public FeatureContainer FeatureContainerOnAddedToStage => m_FeatureContainerOnAddedToStage ??= new FeatureContainer(this);
        
        private FeatureContainer m_FeatureContainerOnInit;
        private FeatureContainer m_FeatureContainerOnAddedToStage;

        #endregion
        
        public override void ConstructFromXML(FairyGUI.Utils.XML xml)
        {
            base.ConstructFromXML(xml);

            #region [子节点]
{{ for node in nodes }} {{ if is_accept_name node.name }}
            {{ upper_first node.name }} = ({{ node.ref_type_full_name }})GetChild("{{ node.name }}");
{{ end }} {{ end }}
            #endregion

            #region [控制器]
{{ for controller in controllers }} {{ if is_accept_name controller.name }}
            {{ upper_first controller.name }}Controller = GetController("{{ controller.name }}");
{{ end }} {{ end }}
            #endregion

            #region [动效]
{{ for transition in transitions }} {{ if is_accept_name transition.name }}
            {{ upper_first transition.name }}Transition = GetTransition("{{ transition.name }}");
{{ end }} {{ end }}
            #endregion

            #region [生命周期事件]

            this.onAddedToStage.Add(_OnAddedToStage);
            this.onRemovedFromStage.Add(_OnRemoveFromStage);

            #endregion

            m_FeatureContainerOnInit?.Awake();
            OnInitialize();
        }

        public override void Dispose()
        {
            OnDispose();
            
            m_FeatureContainerOnInit?.Shutdown();
            base.Dispose();
        }
        
        #region [生命周期]
        
        private void _OnAddedToStage()
        {
            m_FeatureContainerOnAddedToStage?.Awake();
            OnAddedToStage();
        }
        
        private void _OnRemoveFromStage()
        {
            OnRemoveFromStage();
            m_FeatureContainerOnAddedToStage?.Shutdown();
        }
        
        partial void OnAddedToStage();
        partial void OnRemoveFromStage();
        
        #endregion
    }
}