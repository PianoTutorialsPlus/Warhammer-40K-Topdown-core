using Editor.Infrastructure;
using NUnit.Framework;
using System.Collections.Generic;
using WH40K.Essentials;
using WH40K.GameMechanics.Combat;

namespace Editor.CombatTests
{
    public class SelectEnemyTests
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

        public RollTheDiceSO GetRollTheDiceEventChannel()
        {
            RollTheDiceSO eventChannel = A.RollTheDiceEventChannel;
            eventChannel.OnEventRaised += Filler;
            return eventChannel;
        }
        public IUnit GetUnit(int value, int wounds)
        {
            var unit = A.Unit
                .WithInteger(value)
                .WithWounds(wounds)
                .Build();
            return unit;
        }
        private SelectEnemies GetDamageDealer(IUnit unit, RollTheDiceSO diceResult = null)
        {
            return An.EnemySelector
                .WithIResult(An.IResultEvent
                    .WithDiceResultEventChannel(diceResult)
                    .WithGameStats(A.GameStats
                        .WithActiveUnit(GetUnit(1, 2))
                        .WithEnemyUnit(unit))
                    .Build());
        }
        public class TheActionMethod : SelectEnemyTests
        {
            [Test]
            public void When_Action_Is_Called_Then_DiceResult_Event_Is_Raised_With_State_Damage()
            {
                var diceAction = GetRollTheDiceEventChannel();
                var unit = GetUnit(1, 2);
                var dealDamage = GetDamageDealer(unit, diceAction);

                dealDamage.Action(new List<int>() { 2 });

                Assert.AreEqual(ShootingSubEvents.SelectEnemy, _diceEvent);
            }
        }
    }
}

