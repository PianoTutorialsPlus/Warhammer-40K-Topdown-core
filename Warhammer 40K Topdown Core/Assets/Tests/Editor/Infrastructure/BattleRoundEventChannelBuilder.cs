using UnityEngine;

namespace Editor.Infrastructure
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
