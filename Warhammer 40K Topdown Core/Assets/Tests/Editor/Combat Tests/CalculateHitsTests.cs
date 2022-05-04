using Editor.Infrastructure;
using NUnit.Framework;
using System.Collections.Generic;
using WH40K.Essentials;
using WH40K.GameMechanics.Combat;

namespace Editor.CombatTests
{
    public class CalculateHitsTests
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
        private CalculateHits GetCalculateHits(IUnit unit, RollTheDiceSO diceResult, RollTheDiceSO diceSubResult = null)
        {
            return A.CalculateHits
                .WithIResult(An.IResultEvent
                    .WithDiceActionEventChannel(diceResult)
                    .WithDiceSubResultEventChannel(diceSubResult)
                    .WithDiceResultEventChannel(diceResult)
                    .WithGameStats(A.GameStats.WithActiveUnit(unit))
                    .Build());
        }
        public class TheActionMethod : CalculateHitsTests
        {
            [Test]
            public void When_Unit_Has_1_Shot_Then_DiceAction_Event_Is_Raised_With_1_Shot()
            {
                var diceAction = GetRollTheDiceEventChannel();
                var unit = GetUnit(1);
                var calculateHits = GetCalculateHits(unit, diceAction);

                calculateHits.Action(new List<int>());

                Assert.AreEqual(1, _result.Count);
            }
            [Test]
            public void When_Action_Is_Called_Then_DiceAction_Event_Is_Raised_With_State_Hit()
            {
                var diceAction = GetRollTheDiceEventChannel();
                var unit = GetUnit(1);
                var calculateHits = GetCalculateHits(unit, diceAction);

                calculateHits.Action(new List<int>());

                Assert.AreEqual(ShootingSubEvents.Hit, _diceEvent);
            }
        }
        public class TheResultMethod : CalculateHitsTests
        {
            [Test]
            public void When_Hit_Result_Is_Null_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateHits(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.Hit, null);

                Assert.IsNull(_result);
            }
            [Test]
            public void When_Hit_Result_Count_Is_Null_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateHits(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.Hit, new List<int>());

                Assert.IsNull(_result);
            }
            [Test]
            public void When_ShootingSubEvents_Not_Eqauls_Hit_Then_DiceResult_Event_Is_Not_Raised()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateHits(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.None, new List<int>() { 2 });
                Assert.IsNull(_result);
            }
            [Test]
            public void When_ShootingSubEvents_Eqauls_Hit_Then_DiceResult_Event_Is_Raised_With_State_Hit()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateHits(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.Hit, new List<int>() { 2 });
                Assert.AreEqual(ShootingSubEvents.Hit, _diceEvent);
            }
            [Test]
            public void When_1_HitResult_Has_A_Value_Of_2_And_ToHit_Is_2_Then_DiceResult_Event_Is_Raised_With_1_Hit()
            {
                var diceResult = GetRollTheDiceEventChannel();
                var diceSubResult = GetDiceSubEventChannel();
                var unit = GetUnit(2);
                GetCalculateHits(unit, diceResult, diceSubResult);

                diceSubResult.RaiseEvent(ShootingSubEvents.Hit, new List<int>() { 2 });
                Assert.AreEqual(1, _result.Count);
            }
        }
    }
}
