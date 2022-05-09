using NSubstitute;
using WH40K.UI;

namespace Editor.Infrastructure.Events
{
    public class IManageUIEventBuilder : TestDataBuilder<IManageUIEvents>
    {
        private InfoUIEventChannelSO _playerEventListener;
        private InfoUIEventChannelSO _enemyEventListener;
        private InteractionUIEventChannelSO _interactionEventListener;
        private IndicatorUIEventChannelSO _indicatorEventListener;
        private BattleroundEventChannelSO _battleRoundEventListener;

        public IManageUIEventBuilder()
        {

        }
        public IManageUIEventBuilder WithPlayerEventListener(InfoUIEventChannelSO eventListener)
        {
            _playerEventListener = eventListener;
            return this;
        }
        public IManageUIEventBuilder WithEnemyEventListener(InfoUIEventChannelSO eventListener)
        {
            _enemyEventListener = eventListener;
            return this;
        }
        public IManageUIEventBuilder WithInteractionEventListener(InteractionUIEventChannelSO eventListener)
        {
            _interactionEventListener = eventListener;
            return this;
        }
        public IManageUIEventBuilder WithMoveRangeIndicatorEventListener(IndicatorUIEventChannelSO eventListener)
        {
            _indicatorEventListener = eventListener;
            return this;
        }
        public IManageUIEventBuilder WithPhaseEventListener(BattleroundEventChannelSO eventListener)
        {
            _battleRoundEventListener = eventListener;
            return this;
        }

        public override IManageUIEvents Build()
        {
            var uIEvents = Substitute.For<IManageUIEvents>();
            uIEvents.InfoUIEvent.Returns(_playerEventListener ??= A.InfoUIEventChannel);
            uIEvents.EnemyInfoUIEvent.Returns(_enemyEventListener ??= A.InfoUIEventChannel);
            uIEvents.InteractionUIEvent.Returns(_interactionEventListener ??= A.InteractionUIEventChannel);
            uIEvents.IndicatorConnectionUIEvent.Returns(_indicatorEventListener ??= A.IndicatorUIEventChannel);
            uIEvents.SetPhaseEvent.Returns(_battleRoundEventListener ??= A.BattleRoundEventChannel);

            return uIEvents;
        }
    }
}
