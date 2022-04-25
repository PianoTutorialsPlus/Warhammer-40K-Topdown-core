using UnityEngine;
using WH40K.UI;

namespace Editor.Infrastructure.EventChannels
{
    public class InteractionUIEventChannelBuilder : TestDataBuilder<InteractionUIEventChannelSO>
    {
        public InteractionUIEventChannelBuilder()
        {
        }

        public override InteractionUIEventChannelSO Build()
        {
            return ScriptableObject.CreateInstance<InteractionUIEventChannelSO>();
        }
    }
}
