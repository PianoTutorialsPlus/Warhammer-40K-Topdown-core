using WH40K.DiceEvents;
using WH40K.Gameplay.Events;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;

namespace Editor.Infrastructure.GamePhases
{
    public class GamePhaseFactoryBuilder : TestDataBuilder<GamePhaseFactory>
    {
        private GameStatsSO _gameStats;
        private IPhase _gamePhase;
        private RollTheDiceEventChannelSO _diceAction;
        private RollTheDiceEventChannelSO _diceSubResult;
        private RollTheDiceEventChannelSO _diceResult;

        public GamePhaseFactoryBuilder()
        {
        }
        public GamePhaseFactoryBuilder WithGamePhase(IPhase gamePhase)
        {
            _gamePhase = gamePhase;
            return this;
        }
        public GamePhaseFactoryBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }
        public GamePhaseFactoryBuilder WithDiceActionEventChannel(RollTheDiceEventChannelSO diceAction)
        {
            _diceAction = diceAction;
            return this;
        }
        public GamePhaseFactoryBuilder WithDiceSubResultEventChannel(RollTheDiceEventChannelSO diceSubResult)
        {
            _diceSubResult = diceSubResult;
            return this;
        }
        public GamePhaseFactoryBuilder WithDiceResultEventChannel(RollTheDiceEventChannelSO diceResult)
        {
            _diceResult = diceResult;
            return this;
        }
        public override GamePhaseFactory Build()
        {
            Container.BindInterfacesAndSelfTo<GamePhaseFactory>().AsSingle();
            Container.BindInstance(_gamePhase ??= An.IPhaseEvent.Build());
            Container.BindInstance(_gameStats ??= A.GameStats);

            Container.BindInstance(_diceAction ??= A.EventChannel<RollTheDiceEventChannelSO>()).WithId("Action");
            Container.BindInstance(_diceResult ??= A.EventChannel<RollTheDiceEventChannelSO>()).WithId("Result");
            Container.BindInstance(_diceSubResult ??= A.EventChannel<RollTheDiceEventChannelSO>()).WithId("Sub Result");

            return Container.Resolve<GamePhaseFactory>();
        }
    }
}
