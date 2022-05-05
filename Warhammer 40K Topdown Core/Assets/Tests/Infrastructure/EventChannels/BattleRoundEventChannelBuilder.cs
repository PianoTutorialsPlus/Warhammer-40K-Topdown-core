using UnityEngine;

namespace Editor.Infrastructure.EventChannels
{
    public class BattleRoundEventChannelBuilder : TestDataBuilder<BattleroundEventChannelSO>
    {
        public BattleRoundEventChannelBuilder()
        {
        }

        public override BattleroundEventChannelSO Build()
        {
            return ScriptableObject.CreateInstance<BattleroundEventChannelSO>();
        }
    }
}
