using WH40K.Gameplay.EventChannels;

namespace Editor.Infrastructure.Events
{
    public class UIEventsBuilder<T> : TestDataBuilder<T> where T : class
    {
        private BattleroundEventChannelSO _battleRoundEventListener;
        private InteractionUIEventChannelSO _interactionEventListener;
        private InfoUIEventChannelSO _playerEventListener;
        private InfoUIEventChannelSO _enemyEventListener;
        private IndicatorUIEventChannelSO _indicatorEventListener;

        public UIEventsBuilder()
        {
        }
        public UIEventsBuilder<T> WithPlayerEventListener(InfoUIEventChannelSO eventListener)
        {
            _playerEventListener = eventListener;
            return this;
        }
        public UIEventsBuilder<T> WithEnemyEventListener(InfoUIEventChannelSO eventListener)
        {
            _playerEventListener = eventListener;
            return this;
        }
        public UIEventsBuilder<T> WithMoveRangeIndicatorEventListener(IndicatorUIEventChannelSO eventListener)
        {
            _indicatorEventListener = eventListener;
            return this;
        }
        public UIEventsBuilder<T> WithPhaseEventListener(BattleroundEventChannelSO eventListener)
        {
            _battleRoundEventListener = eventListener;
            return this;
        }

        public UIEventsBuilder<T> WithInteractionEventListener(InteractionUIEventChannelSO eventListener)
        {
            _interactionEventListener = eventListener;
            return this;
        }
        public override T Build()
        {
            Container.BindInstance(_interactionEventListener ??= A.InteractionUIEventChannel);
            Container.BindInstance(_battleRoundEventListener ??= A.BattleRoundEventChannel);
            Container.BindInstance(_playerEventListener ??= A.InfoUIEventChannel);
            //Container.BindInstance(_enemyEventListener ??= A.InfoUIEventChannel);
            Container.BindInstance(_indicatorEventListener ??= A.IndicatorUIEventChannel);

            Container.Bind<T>().AsSingle();
            return Container.Resolve<T>();

        }
    }
}
