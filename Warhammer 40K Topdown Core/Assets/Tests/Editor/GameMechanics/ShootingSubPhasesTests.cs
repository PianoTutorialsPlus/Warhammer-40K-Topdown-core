using Editor.Infrastructure;
using GameMechanics.Combat;
using NUnit.Framework;
using System.Collections.Generic;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace Editor.GameMechanics
{
    public class ShootingSubPhasesTests
    {
        private ShootingSubEvents _diceEvent;
        private List<int> _result;

        [SetUp]
        public void BeforeEveryTest()
        {
            _result = null;
            _diceEvent = ShootingSubEvents.None;
        }

        public void Filler(List<int> hitResult)
        {
            //_diceEvent = diceEvent;
            _result = hitResult;
        }
        public void FillerDummy(List<int> hitResult)
        {
        }
        public RollTheDiceSO GetRollTheDiceEventChannel()
        {
            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel;
            eventChannel.OnEventRaised += Filler;
            return eventChannel;
        }
        public RollTheDiceSO GetDiceSubEventChannel()
        {
            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel;
            eventChannel.OnEventRaised += FillerDummy;
            return eventChannel;
        }
        public IUnit GetUnit(int value,int wounds = 0)
        {
            return A.Unit
                .WithInteger(value)
                .WithWounds(wounds)
                .Build();
        }
        public IResult GetIResult(IUnit unit, RollTheDiceSO diceAction, RollTheDiceSO subResult = null)
        {
            return An.IResultEvent
                        .WithDiceActionEventChannel(diceAction)
                        .WithDiceResultEventChannel(diceAction)
                        .WithDiceSubResultEventChannel(subResult)
                        .WithGameStats(A.GameStats
                            .WithActiveUnit(unit)
                            .WithEnemyUnit(unit))
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

        public class TheHandleShootingMethod : ShootingSubPhasesTests
        {
            [Test]
            public void When_ShootingSubPhase_State_Is_SelectEnemy_Then_DiceResult_Event_Is_Raised_With_State_SelectEnemy()
            {
                var diceAction = GetRollTheDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetShootingSubPhaseProcessor(result);
                ShootingSubPhaseProcessor.HandleShooting(ShootingSubEvents.SelectEnemy, new List<int>());

                Assert.AreEqual(1, _result.Count);
            }

            [Test]
            public void When_ShootingSubPhase_State_Is_Hit_Then_DiceAction_Event_Is_Raised_With_State_Hit()
            {
                var diceAction = GetRollTheDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetShootingSubPhaseProcessor(result);
                ShootingSubPhaseProcessor.HandleShooting(ShootingSubEvents.Hit, new List<int>() { 1 });

                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Wound_Then_DiceAction_Event_Is_Raised_With_State_Wound()
            {
                var diceAction = GetRollTheDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetShootingSubPhaseProcessor(result);
                ShootingSubPhaseProcessor.HandleShooting(ShootingSubEvents.Wound, new List<int>() { 1 });

                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Save_Then_DiceAction_Event_Is_Raised_With_State_Save()
            {
                var diceAction = GetRollTheDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetShootingSubPhaseProcessor(result);
                ShootingSubPhaseProcessor.HandleShooting(ShootingSubEvents.Save, new List<int>() { 1 });

                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Damage_Then_DiceAction_Event_Is_Raised_With_State_Damage()
            {
                var diceAction = GetRollTheDiceEventChannel();
                var unit = GetUnit(1,2);
                var result = GetIResult(unit, diceAction);

                SetShootingSubPhaseProcessor(result);
                ShootingSubPhaseProcessor.HandleShooting(ShootingSubEvents.Damage, new List<int>() { 4 });

                Assert.IsNull(_result);
            }
        }
    }
}
