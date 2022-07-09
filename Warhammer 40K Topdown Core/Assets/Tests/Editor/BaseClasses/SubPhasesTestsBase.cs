using Editor.Infrastructure;
using System.Collections.Generic;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats.Player;

namespace Editor.Base
{
    public class SubPhasesTestsBase : CoreElementsBase
    {
        public List<int> _actionResult;
        public List<int> _result;

        public void ActionFiller(List<int> hitResult)
        {
            _actionResult = hitResult;
        }
        public void ResultFiller(List<int> hitResult)
        {
            _result = hitResult;
        }
        public void FillerDummy(List<int> hitResult)
        {
        }
        public T GetEventChannel<T>() where T : ScriptableObject
        {
            T eventListener = A.EventChannel<T>();
            return eventListener;
        }

        public RollTheDiceEventChannelSO GetActionDiceEventChannel()
        {
            var eventChannel = GetEventChannel<RollTheDiceEventChannelSO>();
            eventChannel.OnEventRaised += ActionFiller;
            return eventChannel;
        }
        public RollTheDiceEventChannelSO GetResultDiceEventChannel()
        {
            var eventChannel = GetEventChannel<RollTheDiceEventChannelSO>();
            eventChannel.OnEventRaised += ResultFiller;
            return eventChannel;
        }
        public RollTheDiceEventChannelSO GetDiceSubEventChannel()
        {
            var eventChannel = GetEventChannel<RollTheDiceEventChannelSO>();
            eventChannel.OnEventRaised += FillerDummy;
            return eventChannel;
        }

        public void SetShootingSubPhaseProcessor()
        {
            ShootingSubPhaseProcessor processor = A.ShootingSubPhaseProcessor;
            processor.SetPrivate(x => x.Initialized, false);
        }
        public void SetCombatProcessor(
            RollTheDiceEventChannelSO diceAction = null,
            RollTheDiceEventChannelSO diceResult = null,
            RollTheDiceEventChannelSO subResult = null, 
            IUnit unit = null)
        {
            var gameStats = GetGameStats(unit: unit);
            CombatProcessor processor = A.CombatProcessor
                .WithGameStats(gameStats)
                .WithDiceActionEventChannel(diceAction)
                .WithDiceResultEventChannel(diceResult)
                .WithDiceSubResultEventChannel(subResult);

            processor.SetPrivate(x => x.Initialized, false);
        }
    }
}
