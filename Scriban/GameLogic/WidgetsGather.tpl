using UnityGameFramework.Runtime;

namespace GameLogic
{
    public sealed partial class {{ name }} : WidgetsGatherBase
    {
{{ for node in nodes }}
        public {{ node.type_full_name }} {{ node.field_name }} { get; private set; }
{{ end }}

        protected override void Awake(WidgetsGather gather)
        {
{{ for node in nodes }}
            {{ node.field_name }} = gather.GetWidget<{{ node.type_full_name }}>("{{ node.name }}");
{{ end }}
            OnAwake();
        }

        partial void OnAwake();
    }
}