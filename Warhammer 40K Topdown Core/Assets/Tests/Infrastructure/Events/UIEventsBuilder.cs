using WH40K.Gameplay.EventChannels;
using WH40K.Stats;

namespace Editor.Infrastructure.Events
{
    public class UIEventsBuilder<T> : TestDataBuilder<T> where T : class
    {
        private BattleroundEventChannelSO _battleRoundEventListener;
        private InteractionUIEventChannelSO _interactionEventListener;
        private InfoUIEventChannelSO _playerEventListener;
        private InfoUIEventChannelSO _enemyEventListener;
        private IndicatorUIEventChannelSO _indicatorEventListener;
        private GameStatsSO _gameStats;

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
            _enemyEventListener = eventListener;
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
        public UIEventsBuilder<T> WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }
        public override T Build()
        {
            Container.BindInstance(_interactionEventListener ??= A.EventChannel<InteractionUIEventChannelSO>());
            Container.BindInstance(_battleRoundEventListener ??= A.EventChannel<BattleroundEventChannelSO>());
            Container.BindInstance(_playerEventListener ??= A.EventChannel<InfoUIEventChannelSO>()).WithId("Player");
            Container.BindInstance(_enemyEventListener ??= A.EventChannel<InfoUIEventChannelSO>()).WithId("Enemy");
            Container.BindInstance(_indicatorEventListener ??= A.EventChannel<IndicatorUIEventChannelSO>());
            Container.BindInstance(_gameStats ??= A.GameStats.Build());

            Container.Bind<T>().AsSingle();
            return Container.Resolve<T>();

        }
    }
}
