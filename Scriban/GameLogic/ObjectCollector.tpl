using UnityGameFramework.Runtime;

namespace {{ namespace }}
{
    public sealed partial class {{ name }} : ObjectCollectorBase
    {
{{ for node in nodes }}
        public {{ node.type_full_name }} {{ node.field_name }} { get; private set; }
{{ end }}

        protected override void Awake(ObjectCollector collector)
        {
{{ for node in nodes }}
            {{ node.field_name }} = collector.Get<{{ node.type_full_name }}>("{{ node.name }}");
{{ end }}
            OnAwake();
        }

        partial void OnAwake();
    }
}