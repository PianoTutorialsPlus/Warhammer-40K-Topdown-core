using Infrastructure.EventChannels;
using WH40K.EventChannels;

namespace Editor.Infrastructure
{
    public static partial class A
    {
        public static EventChannelBuilder<InfoUIEventChannelSO> InfoUIEventChannel => new EventChannelBuilder<InfoUIEventChannelSO>();
        public static EventChannelBuilder<InteractionUIEventChannelSO> InteractionUIEventChannel => new EventChannelBuilder<InteractionUIEventChannelSO>();
        public static EventChannelBuilder<RollTheDiceEventChannelSO> RollTheDiceEventChannel => new EventChannelBuilder<RollTheDiceEventChannelSO>();
        public static EventChannelBuilder<IndicatorUIEventChannelSO> IndicatorUIEventChannel => new EventChannelBuilder<IndicatorUIEventChannelSO>();
        public static EventChannelBuilder<BattleroundEventChannelSO> BattleRoundEventChannel => new EventChannelBuilder<BattleroundEventChannelSO>();

    }
}
