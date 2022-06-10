using Editor.GameMechanics;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;

namespace Editor.CombatTests
{
    public class CombatPhasesTests : SubPhasesTestsBase
    {
        [SetUp]
        public void BeforeEveryTest()
        {
            _action = null;
            _result = null;
        }

        public class TheActionMethod : CombatPhasesTests
        {
            [Test]
            public void When_ShootingSubPhase_State_Is_SelectEnemy_Then_DiceResult_Event_Is_Raised_With_1_Result()
            {
                var diceResult = GetResultDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceResult: diceResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.SelectEnemy);

                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Hit_And_Unit_Has_1_Shot_Then_DiceAction_Event_Is_Raised_With_1_Shot()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Hit, new List<int>());

                Assert.AreEqual(1, _action.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Wound_And_Hits_Is_Null_Then_DiceAction_Event_Is_Not_Raised()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Wound);

                Assert.IsNull(_action);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Wound_And_Argument_Has_0_Hits_Then_DiceAction_Event_Is_Not_Raised()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Wound, new List<int>());

                Assert.IsNull(_action);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Wound_And_Argument_Has_1_Hit_Then_DiceAction_Event_Is_Raised_With_1_Hit()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Wound, new List<int>() { 2 });

                Assert.AreEqual(1, _action.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Save_And_Wounds_Is_Null_Then_DiceAction_Event_Is_Not_Raised()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Save);

                Assert.IsNull(_action);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Save_And_Argument_Has_0_Wounds_Then_DiceAction_Event_Is_Not_Raised()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Save, new List<int>());

                Assert.IsNull(_action);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Save_And_Argument_Has_1_Wound_Then_DiceAction_Event_Is_Raised_With_1_Wound()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Save, new List<int>() { 2 });

                Assert.AreEqual(1, _action.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Damage_And_Unsaved_Wounds_Is_Null_Then_DiceAction_Event_Is_Not_Raised()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Damage);

                Assert.IsNull(_action);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Damage_And_Argument_Has_0_Unsaved_Wounds_Then_DiceAction_Event_Is_Not_Raised()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceAction);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Damage, new List<int>());

                Assert.IsNull(_action);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Damage_And_Unit_With_2_Wounds_Takes_0_Unsaved_Wounds_With_1_Damage_Then_Unit_Has_2_Wounds_Left()
            {
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1, 2);
                var result = GetIResult(unit, diceAction);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Damage, new List<int>());

                Assert.AreEqual(2, unit.Wounds);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Damage_And_Unit_With_2_Wounds_Takes_1_Unsaved_Wounds_With_1_Damage_Then_Unit_Has_1_Wounds_Left()
            {
                var count = 0;
                var diceAction = GetActionDiceEventChannel();
                var unit = GetUnit(1, 2);
                var result = GetIResult(unit, diceAction);

                unit.When(x => x.TakeDamage(1))
                    .Do(x => count = 1);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Damage, new List<int>() { 1 });

                Assert.AreEqual(1, count);
            }
        }
        public class TheResultMethod : CombatPhasesTests
        {
            [Test]
            public void When_ShootingSubPhase_State_Is_SelectEnemy_Then_DiceResult_Event_Is_Raised_With_1_Result()
            {
                var diceResult = GetResultDiceEventChannel();
                var unit = GetUnit(1);
                var result = GetIResult(unit, diceResult: diceResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.SelectEnemy);

                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Hit_And_Hit_Result_Is_Null_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Hit);

                diceSubResult.RaiseEvent(null);

                Assert.IsNull(_result);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Hit_And_Hit_Result_Count_Is_Null_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Hit);
                diceSubResult.RaiseEvent(new List<int>());

                Assert.IsNull(_result);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Hit_And_1_HitResult_Has_A_Value_Of_2_And_ToHit_Is_2_Then_DiceResult_Event_Is_Raised_With_1_Hit()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Hit);
                diceSubResult.RaiseEvent(new List<int>() { 2 });

                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Wound_And_Hit_Result_Is_Null_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Wound);

                diceSubResult.RaiseEvent(null);

                Assert.IsNull(_result);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Wound_And_Hit_Result_Count_Is_0_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Wound);

                diceSubResult.RaiseEvent(new List<int>());

                Assert.IsNull(_result);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Wound_And_1_WoundResult_Passes_ToWound_Then_DiceResult_Event_Is_Raised_With_1_Wound()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Wound, new List<int>() { 2 });

                diceSubResult.RaiseEvent(new List<int>() { 4 });
                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Wound_And_1_WoundResult_Failed_ToWound_Then_DiceResult_Event_Is_Raised_With_0_Wounds()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Wound, new List<int>() { 2 });

                diceSubResult.RaiseEvent(new List<int>() { 3 });
                Assert.AreEqual(0, _result.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Save_And_Save_Result_Is_Null_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Save);
                diceSubResult.RaiseEvent(null);

                Assert.IsNull(_result);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Save_And_Save_Result_Count_Is_0_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Save);
                diceSubResult.RaiseEvent(new List<int>());

                Assert.IsNull(_result);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Save_And_1_Save_Result_Failed_Then_DiceResult_Event_Is_Raised_With_1_Failed_Save()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Save, new List<int>() { 2 });
                diceSubResult.RaiseEvent(new List<int>() { 1 });
                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Save_And_1_Save_Result_Passes_Then_DiceResult_Event_Is_Raised_With_0_Failed_Saves()
            {
                var diceResult = GetResultDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult, subResult: diceSubResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Save, new List<int>() { 2 });
                diceSubResult.RaiseEvent(new List<int>() { 3 });
                Assert.AreEqual(0, _result.Count);
            }
            [Test]
            public void When_ShootingSubPhase_State_Is_Damage__Then_DiceResult_Event_Is_Raised()
            {
                var diceResult = GetResultDiceEventChannel();
                //var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                var result = GetIResult(unit, diceResult: diceResult);

                SetCombatProcessor(result);
                CombatProcessor.Action(ShootingSubEvents.Damage, new List<int>() { 2 });
                Assert.IsNull(_result);
            }
        }
    }
}
