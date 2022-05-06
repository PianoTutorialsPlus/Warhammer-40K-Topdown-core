//using Editor.Infrastructure;
//using NSubstitute;
//using NUnit.Framework;
//using System.Collections.Generic;
//using WH40K.Essentials;
//using WH40K.GameMechanics.Combat;

//namespace Editor.CombatTests
//{
//    public class DealDamageTests
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

//        public RollTheDiceSO GetRollTheDiceEventChannel()
//        {
//            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel;
//            eventChannel.OnEventRaised += Filler;
//            return eventChannel;
//        }
//        public IUnit GetUnit(int value, int wounds)
//        {
//            var unit = A.Unit
//                .WithInteger(value)
//                .WithWounds(wounds)
//                .Build();
//            return unit;
//        }
//        private DealDamage GetDamageDealer(IUnit unit, RollTheDiceSO diceResult = null)
//        {
//            return A.DamageDealer
//                .WithIResult(An.IResultEvent
//                    .WithDiceResultEventChannel(diceResult)
//                    .WithGameStats(A.GameStats
//                        .WithActiveUnit(GetUnit(1,2))
//                        .WithEnemyUnit(unit))
//                    .Build());
//        }
//        public class TheActionMethod : DealDamageTests
//        {
//            [Test]
//            public void When_Unit_With_2_Wounds_Takes_0_Unsaved_Wounds_With_1_Damage_Then_Unit_Has_2_Wounds_Left()
//            {
//                var unit = GetUnit(1, 2);
//                var dealDamage = GetDamageDealer(unit);

//                dealDamage.Action(new List<int>());

//                Assert.AreEqual(2, unit.Wounds);
//            }
//            [Test]
//            public void When_Unit_With_2_Wounds_Takes_1_Unsaved_Wounds_With_1_Damage_Then_Unit_Has_1_Wounds_Left()
//            {
//                var count = 0;
//                var unit = GetUnit(1, 2);
                
//                unit.When(x => x.TakeDamage(1))
//                    .Do(x=> count = 1);

//                var dealDamage = GetDamageDealer(unit);

//                dealDamage.Action(new List<int>() { 1 });

//                Assert.AreEqual(1, count);
//            }
//            [Test]
//            public void When_Action_Is_Called_Then_DiceResult_Event_Is_Raised_With_State_Damage()
//            {
//                var diceAction = GetRollTheDiceEventChannel();
//                var unit = GetUnit(1, 2);
//                var dealDamage = GetDamageDealer(unit, diceAction);

//                dealDamage.Action(new List<int>() { 2 });

//                Assert.AreEqual(ShootingSubEvents.Damage, _diceEvent);
//            }
//        }
//    }
//}
