using Editor.Infrastructure;
using NUnit.Framework;
using System.Collections.Generic;
using WH40K.Essentials;
using WH40K.GameMechanics.Combat;

namespace Editor.CombatTests
{
    public class CalculateSaverolesTests
    {
        private ShootingSubEvents _diceEvent;
        private List<int> _result;

        [SetUp]
        public void BeforeEveryTest()
        {
            _result = null;
            _diceEvent = ShootingSubEvents.None;
        }

        public void Filler(ShootingSubEvents diceEvent, List<int> hitResult)
        {
            _diceEvent = diceEvent;
            _result = hitResult;
        }
        public void FillerDummy(ShootingSubEvents diceEvent, List<int> hitResult)
        {
        }
        public RollTheDiceSO GetRollTheDiceEventChannel()
        {
            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel; ;
            eventChannel.OnEventRaised += Filler;
            return eventChannel;
        }
        public RollTheDiceSO GetDiceSubEventChannel()
        {
            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel; ;
            eventChannel.OnEventRaised += FillerDummy;
            return eventChannel;
        }
        public IUnit GetUnit(int value)
        {
            return A.Unit.WithInteger(value).Build();
        }
        private CalculateSaveroles GetCalculateWounds(IUnit unit, RollTheDiceSO diceResult, RollTheDiceSO diceSubResult = null)
        {
            return A.CalculateSaves
                .WithIResult(An.IResultEvent
                    .WithDiceActionEventChannel(diceResult)
                    .WithDiceSubResultEventChannel(diceSubResult)
                    .WithDiceResultEventChannel(diceResult)
                    .WithGameStats(A.GameStats
                        .WithActiveUnit(unit)
                        .WithEnemyUnit(unit))
                    .Build());
        }
        public class TheActionMethod : CalculateSaverolesTests
        {
            [Test]
            public void When_Action_Is_Called_With_1_Wound_Then_DiceAction_Event_Is_Raised_With_1_Wound()
            {
                var diceAction = GetRollTheDiceEventChannel();
                var unit = GetUnit(1);
                var calculateHits = GetCalculateWounds(unit, diceAction);

                calculateHits.Action(new List<int>() { 2 });

                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_Action_Is_Called_Then_DiceAction_Event_Is_Raised_With_State_Save()
            {
                var diceAction = GetRollTheDiceEventChannel();
                var unit = GetUnit(1);
                var calculateHits = GetCalculateWounds(unit, diceAction);

                calculateHits.Action(new List<int>() { 2 });

                Assert.AreEqual(ShootingSubEvents.Save, _diceEvent);
            }

        }
        public class TheResultMethod : CalculateSaverolesTests
        {
            [Test]
            public void When_Save_Result_Is_Null_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateWounds(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.Save, null);

                Assert.IsNull(_result);
            }
            [Test]
            public void When_Save_Result_Count_Is_0_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateWounds(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.Save, new List<int>());

                Assert.IsNull(_result);
            }
            [Test]
            public void When_ShootingSubEvents_Not_Eqauls_Wound_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateWounds(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.None, new List<int>() { 2 });
                Assert.IsNull(_result);
            }
            [Test]
            public void When_ShootingSubEvents_Eqauls_Save_Then_DiceResult_Event_Is_Raised_With_State_Save()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateWounds(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.Save, new List<int>() { 2 });
                Assert.AreEqual(ShootingSubEvents.Save, _diceEvent);
            }
            [Test]
            public void When_1_Save_Result_Failed_Then_DiceResult_Event_Is_Raised_With_1_Failed_Save()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateWounds(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.Save, new List<int>() { 1 });
                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_1_Save_Result_Passes_Then_DiceResult_Event_Is_Raised_With_0_Failed_Saves()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateWounds(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.Save, new List<int>() { 3 });
                Assert.AreEqual(0, _result.Count);
            }
        }
    }
}
