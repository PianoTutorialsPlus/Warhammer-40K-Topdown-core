using UnityEngine;

namespace Editor.Infrastructure.EventChannels
{
    public class RollTheDiceEventChannelBuilder : TestDataBuilder<RollTheDiceSO>
    {
        public RollTheDiceEventChannelBuilder()
        {
        }

        public override RollTheDiceSO Build()
        {
            return ScriptableObject.CreateInstance<RollTheDiceSO>();
        }
    }
}
