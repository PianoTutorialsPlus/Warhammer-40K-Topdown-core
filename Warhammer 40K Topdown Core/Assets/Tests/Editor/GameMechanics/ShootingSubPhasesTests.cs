using Editor.Base;
using NUnit.Framework;
using System.Collections.Generic;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;

namespace Editor.GameMechanics
{
    public class ShootingSubPhasesTests : SubPhasesTestsBase
    {
        public void HandleShooting(ShootingSubEvents phase, List<int> list)
        {
            ShootingSubPhaseProcessor.HandleShooting(phase, list);
        }

        [SetUp]
        public void BeforeEveryTest()
        {
            _result = null;
            _actionResult = null;
        }

        public class TheHandleShootingMethod : ShootingSubPhasesTests
        {
            [Test]
            public void When_ShootingSubPhase_State_Is_SelectEnemy_Then_DiceResult_Event_Is_Raised_With_State_SelectEnemy()
            {
                var diceResult = GetResultDiceEventChannel();
                var unit = GetUnit(value: 1);

                SetShootingSubPhaseProcessor();
                SetCombatProcessor(diceResult: diceResult, unit: unit);

                HandleShooting(ShootingSubEvents.SelectEnemy, new List<int>());

                Assert.AreEqual(1, _result.Count);
            }

            [Test]
            public void When_ShootingSubPhase_State_Is_Hit_Then_DiceAction_Event_Is_Raised_With_State_Hit()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(value: 1);

                SetShootingSubPhaseProcessor();
                SetCombatProcessor(diceAction: diceAction, unit: unit);

                HandleShooting(ShootingSubEvents.Hit, new List<int>() { 1 });

                Assert.AreEqual(1, _actionResult.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Wound_Then_DiceAction_Event_Is_Raised_With_State_Wound()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(value: 1);

                SetShootingSubPhaseProcessor();
                SetCombatProcessor(diceAction: diceAction, unit: unit);

                HandleShooting(ShootingSubEvents.Wound, new List<int>() { 1 });

                Assert.AreEqual(1, _actionResult.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Save_Then_DiceAction_Event_Is_Raised_With_State_Save()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(value: 1);

                SetShootingSubPhaseProcessor();
                SetCombatProcessor(diceAction: diceAction, unit: unit);

                HandleShooting(ShootingSubEvents.Save, new List<int>() { 1 });

                Assert.AreEqual(1, _actionResult.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Damage_Then_DiceResult_Event_Is_Raised()
            {
                var diceResult = GetResultDiceEventChannel();
                var unit = GetUnit(value: 1,wounds: 2);

                SetShootingSubPhaseProcessor();
                SetCombatProcessor(diceResult: diceResult, unit: unit);

                HandleShooting(ShootingSubEvents.Damage, new List<int>() { 4 });

                Assert.IsNull(_result);
            }
        }
    }
}
