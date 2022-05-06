//using Editor.Infrastructure;
//using NUnit.Framework;
//using System.Collections.Generic;
//using WH40K.Essentials;
//using WH40K.GameMechanics.Combat;

//namespace Editor.CombatTests
//{
//    public class CalculateWoundsTests
//    {
//        private ShootingSubEvents _diceEvent;
//        private List<int> _result;

//        [SetUp]
//        public void BeforeEveryTest()
//        {
//            _result = null;
//            _diceEvent = ShootingSubEvents.None;
//        }

//        public void Filler(ShootingSubEvents diceEvent, List<int> hitResult)
//        {
//            _diceEvent = diceEvent;
//            _result = hitResult;
//        }
//        public void FillerDummy(ShootingSubEvents diceEvent, List<int> hitResult)
//        {
//        }
//        public RollTheDiceSO GetRollTheDiceEventChannel()
//        {
//            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel; ;
//            eventChannel.OnEventRaised += Filler;
//            return eventChannel;
//        }
//        public RollTheDiceSO GetDiceSubEventChannel()
//        {
//            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel; ;
//            eventChannel.OnEventRaised += FillerDummy;
//            return eventChannel;
//        }
//        public IUnit GetUnit(int value)
//        {
//            return A.Unit.WithInteger(value).Build();
//        }
//        private CalculateWounds GetCalculateWounds(IUnit unit, RollTheDiceSO diceResult, RollTheDiceSO diceSubResult = null)
//        {
//            return A.CalculateWounds
//                .WithIResult(An.IResultEvent
//                    .WithDiceActionEventChannel(diceResult)
//                    .WithDiceSubResultEventChannel(diceSubResult)
//                    .WithDiceResultEventChannel(diceResult)
//                    .WithGameStats(A.GameStats
//                        .WithActiveUnit(unit)
//                        .WithEnemyUnit(unit))
//                    .Build());
//        }
//        public class TheActionMethod : CalculateWoundsTests
//        {
//            [Test]
//            public void When_Hits_Is_Null_Then_DiceAction_Event_Is_Not_Raised()
//            {
//                var diceAction = GetRollTheDiceEventChannel();
//                var unit = GetUnit(1);
//                var calculateWounds = GetCalculateWounds(unit, diceAction);

//                calculateWounds.Action(null);

//                Assert.IsNull(_result);
//            }
//            [Test]
//            public void When_Action_Is_Called_With_0_Hits_Then_DiceAction_Event_Is_Not_Raised()
//            {
//                var diceAction = GetRollTheDiceEventChannel();
//                var unit = GetUnit(1);
//                var calculateWounds = GetCalculateWounds(unit, diceAction);

//                calculateWounds.Action(new List<int>());

//                Assert.IsNull(_result);
//            }
//            [Test]
//            public void When_Action_Is_Called_With_1_Hit_Then_DiceAction_Event_Is_Raised_With_1_Hit()
//            {
//                var diceAction = GetRollTheDiceEventChannel();
//                var unit = GetUnit(1);
//                var calculateWounds = GetCalculateWounds(unit, diceAction);

//                calculateWounds.Action(new List<int>() { 2 });

//                Assert.AreEqual(1, _result.Count);
//            }
//            [Test]
//            public void When_Action_Is_Called_Then_DiceAction_Event_Is_Raised_With_State_Wound()
//            {
//                var diceAction = GetRollTheDiceEventChannel();
//                var unit = GetUnit(1);
//                var calculateWounds = GetCalculateWounds(unit, diceAction);

//                calculateWounds.Action(new List<int>() { 2 });

//                Assert.AreEqual(ShootingSubEvents.Wound, _diceEvent);
//            }
//        }
//        public class TheResultMethod : CalculateWoundsTests
//        {
//            [Test]
//            public void When_Hit_Result_Is_Null_Then_DiceResult_Event_Is_Not_Raised()
//            {
//                var diceResult = GetRollTheDiceEventChannel();
//                var diceSubResult = GetDiceSubEventChannel();
//                var unit = GetUnit(2);
//                GetCalculateWounds(unit, diceResult, diceSubResult);

//                diceSubResult.RaiseEvent(ShootingSubEvents.Wound, null);

//                Assert.IsNull(_result);
//            }
//            [Test]
//            public void When_Hit_Result_Count_Is_0_Then_DiceResult_Event_Is_Not_Raised()
//            {
//                var diceResult = GetRollTheDiceEventChannel();
//                var diceSubResult = GetDiceSubEventChannel();
//                var unit = GetUnit(2);
//                GetCalculateWounds(unit, diceResult, diceSubResult);

//                diceSubResult.RaiseEvent(ShootingSubEvents.Wound, new List<int>());

//                Assert.IsNull(_result);
//            }
//            [Test]
//            public void When_ShootingSubEvents_Not_Eqauls_Wound_Then_DiceResult_Event_Is_Not_Raised()
//            {
//                var diceResult = GetRollTheDiceEventChannel();
//                var diceSubResult = GetDiceSubEventChannel();
//                var unit = GetUnit(2);
//                GetCalculateWounds(unit, diceResult, diceSubResult);

//                diceSubResult.RaiseEvent(ShootingSubEvents.None, new List<int>() { 2 });
//                Assert.IsNull(_result);
//            }
//            [Test]
//            public void When_ShootingSubEvents_Eqauls_Wound_Then_DiceResult_Event_Is_Raised_With_State_Wound()
//            {
//                var diceResult = GetRollTheDiceEventChannel();
//                var diceSubResult = GetDiceSubEventChannel();
//                var unit = GetUnit(2);
//                GetCalculateWounds(unit, diceResult, diceSubResult);

//                diceSubResult.RaiseEvent(ShootingSubEvents.Wound, new List<int>() { 2 });
//                Assert.AreEqual(ShootingSubEvents.Wound, _diceEvent);
//            }
//            [Test]
//            public void When_1_WoundResult_Passes_ToWound_Then_DiceResult_Event_Is_Raised_With_1_Wound()
//            {
//                var diceResult = GetRollTheDiceEventChannel();
//                var diceSubResult = GetDiceSubEventChannel();
//                var unit = GetUnit(2);
//                GetCalculateWounds(unit, diceResult, diceSubResult);

//                diceSubResult.RaiseEvent(ShootingSubEvents.Wound, new List<int>() { 4 });
//                Assert.AreEqual(1, _result.Count);
//            }
//            [Test]
//            public void When_1_WoundResult_Failed_ToWound_Then_DiceResult_Event_Is_Raised_With_0_Wounds()
//            {
//                var diceResult = GetRollTheDiceEventChannel();
//                var diceSubResult = GetDiceSubEventChannel();
//                var unit = GetUnit(2);
//                GetCalculateWounds(unit, diceResult, diceSubResult);

//                diceSubResult.RaiseEvent(ShootingSubEvents.Wound, new List<int>() { 3 });
//                Assert.AreEqual(0, _result.Count);
//            }
//        }
//    }
//}
