using Editor.Infrastructure;
using System.Collections.Generic;
using WH40K.EventChannels;
using WH40K.GamePhaseEvents;
using WH40K.PlayerEvents;

namespace Editor.GameMechanics
{
    public class SubPhasesTestsBase
    {
        public List<int> _action;
        public List<int> _result;

        public void ActionFiller(List<int> hitResult)
        {
            _action = hitResult;
        }
        public void ResultFiller(List<int> hitResult)
        {
            _result = hitResult;
        }
        public void FillerDummy(List<int> hitResult)
        {
        }
        public RollTheDiceEventChannelSO GetActionDiceEventChannel()
        {
            RollTheDiceEventChannelSO eventChannel = A.RollTheDiceEventChannel;
            eventChannel.OnEventRaised += ActionFiller;
            return eventChannel;
        }
        public RollTheDiceEventChannelSO GetResultDiceEventChannel()
        {
            RollTheDiceEventChannelSO eventChannel = A.RollTheDiceEventChannel;
            eventChannel.OnEventRaised += ResultFiller;
            return eventChannel;
        }
        public RollTheDiceEventChannelSO GetDiceSubEventChannel()
        {
            RollTheDiceEventChannelSO eventChannel = A.RollTheDiceEventChannel;
            eventChannel.OnEventRaised += FillerDummy;
            return eventChannel;
        }
        public IUnit GetUnit(int value, int wounds = 0)
        {
            return A.Unit
                .WithInteger(value)
                .WithWounds(wounds)
                .Build();
        }
        public IResult GetIResult(IUnit unit, RollTheDiceEventChannelSO diceAction = null, RollTheDiceEventChannelSO diceResult = null, RollTheDiceEventChannelSO subResult = null)
        {
            GetGameStats(unit);
            return An.IResultEvent
                        .WithDiceActionEventChannel(diceAction)
                        .WithDiceResultEventChannel(diceResult)
                        .WithDiceSubResultEventChannel(subResult)
                        .Build();
        }

        private void GetGameStats(IUnit unit)
        {
            A.GameStats
                .WithActiveUnit(unit)
                .WithEnemyUnit(unit)
                .Build();
        }

        public void SetShootingSubPhaseProcessor(IResult result)
        {
            ShootingSubPhaseProcessor processor = A.ShootingSubPhaseProcessor.WithIResult(result);
            processor.SetPrivate(x => x.Initialized, false);
            SetCombatProcessor(result);
        }
        public void SetCombatProcessor(IResult result)
        {
            CombatProcessor processor = A.CombatProcessor.WithIResult(result);
            processor.SetPrivate(x => x.Initialized, false);
        }
    }
}
