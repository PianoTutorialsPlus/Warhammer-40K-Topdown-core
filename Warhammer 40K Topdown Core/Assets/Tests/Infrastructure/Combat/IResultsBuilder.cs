using NSubstitute;
using System.Collections.Generic;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace Editor.Infrastructure.Combat
{
    public class IResultsBuilder : TestDataBuilder<IResult>
    {
        private RollTheDiceSO _diceAction;
        private RollTheDiceSO _diceSubResult;
        private RollTheDiceSO _diceResult;
        private GameStatsSO _gameStats;

        public IResultsBuilder()
        {
        }
        public IResultsBuilder WithDiceActionEventChannel(RollTheDiceSO diceAction)
        {
            _diceAction = diceAction;
            return this;
        }
        public IResultsBuilder WithDiceSubResultEventChannel(RollTheDiceSO diceSubResult)
        {
            _diceSubResult = diceSubResult;
            return this;
        }
        public IResultsBuilder WithDiceResultEventChannel(RollTheDiceSO diceResult)
        {
            _diceResult = diceResult;
            return this;
        }
        public IResultsBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }

        public override IResult Build()
        {
            var result = Substitute.For<IResult>();
            result.DiceAction.Returns(_diceAction ??= A.RollTheDiceEventChannel);
            result.DiceSubResult.Returns(_diceSubResult ??= A.RollTheDiceEventChannel);
            result.DiceResult.Returns(_diceResult ??= A.RollTheDiceEventChannel);
            result.GameStats.Returns(_gameStats ??= A.GameStats);
            return result;
        }
    }
}
