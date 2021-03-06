using NSubstitute;
using WH40K.DiceEvents;

namespace Editor.Infrastructure.Combat
{
    public class IResultsBuilder : TestDataBuilder<IResult>
    {
        private RollTheDiceEventChannelSO _diceAction;
        private RollTheDiceEventChannelSO _diceSubResult;
        private RollTheDiceEventChannelSO _diceResult;

        public IResultsBuilder()
        {
        }
        public IResultsBuilder WithDiceActionEventChannel(RollTheDiceEventChannelSO diceAction)
        {
            _diceAction = diceAction;
            return this;
        }
        public IResultsBuilder WithDiceSubResultEventChannel(RollTheDiceEventChannelSO diceSubResult)
        {
            _diceSubResult = diceSubResult;
            return this;
        }
        public IResultsBuilder WithDiceResultEventChannel(RollTheDiceEventChannelSO diceResult)
        {
            _diceResult = diceResult;
            return this;
        }

        public override IResult Build()
        {
            var result = Substitute.For<IResult>();
            result.DiceAction.Returns(_diceAction ??= A.EventChannel<RollTheDiceEventChannelSO>());
            result.DiceSubResult.Returns(_diceSubResult ??= A.EventChannel<RollTheDiceEventChannelSO>());
            result.DiceResult.Returns(_diceResult ??= A.EventChannel<RollTheDiceEventChannelSO>());
            return result;
        }
    }
}
