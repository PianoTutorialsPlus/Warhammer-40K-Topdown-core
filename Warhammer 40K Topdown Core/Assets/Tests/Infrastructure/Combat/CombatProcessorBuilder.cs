using WH40K.DiceEvents;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;

namespace Editor.Infrastructure.Combat
{
    public class CombatProcessorBuilder : TestDataBuilder<CombatProcessor>
    {
        private IResult _result;
        private GameStatsSO _gameStats;
        private GamePhaseFactory _factory;
        private RollTheDiceEventChannelSO _diceAction;
        private RollTheDiceEventChannelSO _diceSubResult;
        private RollTheDiceEventChannelSO _diceResult;

        public CombatProcessorBuilder()
        {
        }
        public CombatProcessorBuilder WithIResult(IResult result)
        {
            _result = result;
            return this;
        }
        public CombatProcessorBuilder WithDiceActionEventChannel(RollTheDiceEventChannelSO diceAction)
        {
            _diceAction = diceAction;
            return this;
        }
        public CombatProcessorBuilder WithDiceSubResultEventChannel(RollTheDiceEventChannelSO diceSubResult)
        {
            _diceSubResult = diceSubResult;
            return this;
        }
        public CombatProcessorBuilder WithDiceResultEventChannel(RollTheDiceEventChannelSO diceResult)
        {
            _diceResult = diceResult;
            return this;
        }

        public CombatProcessorBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }
        public CombatProcessorBuilder WithGamePhaseFactory(GamePhaseFactory factory)
        {
            _factory = factory;
            return this;
        }

        public override CombatProcessor Build()
        {
            Container.Bind<CombatProcessor>().AsSingle();
            //.WithArguments(_gameStats ??= A.GameStats.Build(), _result ??= An.IResultEvent.Build());
            //Container.BindInstance(_diceAction ??= A.EventChannel<RollTheDiceEventChannelSO>()).WithId("Action");
            //Container.BindInstance(_diceResult ??= A.EventChannel<RollTheDiceEventChannelSO>()).WithId("Result");
            //Container.BindInstance(_diceSubResult ??= A.EventChannel<RollTheDiceEventChannelSO>()).WithId("Sub Result");
            Container.BindInstance(_factory ??= A.GamePhaseFactory
                .WithGameStats(_gameStats)
                .WithDiceActionEventChannel(_diceAction)
                .WithDiceResultEventChannel(_diceResult)
                .WithDiceSubResultEventChannel(_diceSubResult));

            return Container.Resolve<CombatProcessor>();
        }
    }
}
