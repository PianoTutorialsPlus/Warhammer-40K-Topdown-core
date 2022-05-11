using NUnit.Framework;
using System.Collections.Generic;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace Editor.GameMechanics
{
    public class ShootingSubPhasesTests : SubPhasesTestsBase
    {
        [SetUp]
        public void BeforeEveryTest()
        {
            _result = null;
            _action = null;
        }

        public class TheHandleShootingMethod : ShootingSubPhasesTests
        {
            [Test]
            public void When_ShootingSubPhase_State_Is_SelectEnemy_Then_DiceResult_Event_Is_Raised_With_State_SelectEnemy()
            {
                var diceResult = GetResultDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceResult: diceResult);

                SetShootingSubPhaseProcessor(result);
                ShootingSubPhaseProcessor.HandleShooting(ShootingSubEvents.SelectEnemy, new List<int>());

                Assert.AreEqual(1, _result.Count);
            }

            [Test]
            public void When_ShootingSubPhase_State_Is_Hit_Then_DiceAction_Event_Is_Raised_With_State_Hit()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetShootingSubPhaseProcessor(result);
                ShootingSubPhaseProcessor.HandleShooting(ShootingSubEvents.Hit, new List<int>() { 1 });

                Assert.AreEqual(1, _action.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Wound_Then_DiceAction_Event_Is_Raised_With_State_Wound()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetShootingSubPhaseProcessor(result);
                ShootingSubPhaseProcessor.HandleShooting(ShootingSubEvents.Wound, new List<int>() { 1 });

                Assert.AreEqual(1, _action.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Save_Then_DiceAction_Event_Is_Raised_With_State_Save()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetShootingSubPhaseProcessor(result);
                ShootingSubPhaseProcessor.HandleShooting(ShootingSubEvents.Save, new List<int>() { 1 });

                Assert.AreEqual(1, _action.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Damage_Then_DiceResult_Event_Is_Raised()
            {
                var diceResult = GetResultDiceEventChannel();
                var unit = GetUnit(1,2);
                var result = GetIResult(unit, diceResult: diceResult);

                SetShootingSubPhaseProcessor(result);
                ShootingSubPhaseProcessor.HandleShooting(ShootingSubEvents.Damage, new List<int>() { 4 });

                Assert.IsNull(_result);
            }
        }
    }
}
