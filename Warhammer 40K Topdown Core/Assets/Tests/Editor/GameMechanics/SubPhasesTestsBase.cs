using Editor.Infrastructure;
using GameMechanics.Combat;
using System.Collections.Generic;
using WH40K.Essentials;
using WH40K.GameMechanics;

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
        public RollTheDiceSO GetActionDiceEventChannel()
        {
            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel;
            eventChannel.OnEventRaised += ActionFiller;
            return eventChannel;
        }
        public RollTheDiceSO GetResultDiceEventChannel()
        {
            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel;
            eventChannel.OnEventRaised += ResultFiller;
            return eventChannel;
        }
        public RollTheDiceSO GetDiceSubEventChannel()
        {
            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel;
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
        public IResult GetIResult(IUnit unit, RollTheDiceSO diceAction = null, RollTheDiceSO diceResult = null, RollTheDiceSO subResult = null)
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
