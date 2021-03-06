using Editor.Base;
using Editor.Infrastructure;
using NUnit.Framework;
using WH40K.Gameplay.EventChannels;
using WH40K.Gameplay.Events;
using WH40K.Stats;
using WH40K.Stats.Player;

namespace Editor.GameMechanics
{
    public class BattleRoundsEventsTests : UIDisplayEventsTestsBase
    {

        public BattleRoundEvents GetBattleRoundEvents(
            BattleroundEventChannelSO eventListener = null, 
            GameStatsSO gameStats = null)
        {
            return A.UIEvent<BattleRoundEvents>()
                    .WithGameStats(gameStats)
                    .WithPhaseEventListener(eventListener);
        }

        [SetUp]
        public void BeforeEveryTest()
        {
            _action = null;
            _state = false;
        }

        public class TheConnectEventMethod : BattleRoundsEventsTests
        {
            [Test]
            public void When_ConnectEvent_Is_Called_Then_SetPhaseEvent_Is_Raised()
            {
                var eventListener = GetBattleRoundEventListener();
                var child = GetUnit();

                GetBattleRoundEvents(eventListener: eventListener).ConnectEvent(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheSetPhaseEventMethod : BattleRoundsEventsTests
        {
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsActivated_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(isActivated: true);
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);

                GetBattleRoundEvents(gameStats: gameStats).SetPhaseEvent(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_Is_Not_Activated_Then_OnTapDownAction_IsNotNull()
            {
                var child = GetUnit(isActivated: false);
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);

                GetBattleRoundEvents(gameStats: gameStats).SetPhaseEvent(child);

                Assert.IsNotNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Enemy_Fraction_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(playerFraction: Fraction.SpaceMarines);
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);

                GetBattleRoundEvents(gameStats: gameStats).SetPhaseEvent(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsDone_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(isDone: true);
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);

                GetBattleRoundEvents(gameStats: gameStats).SetPhaseEvent(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_SetPhaseEvent_Is_Raised()
            {
                var eventListener = GetBattleRoundEventListener();
                var child = GetUnit();
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons, unit: child);

                GetBattleRoundEvents(eventListener: eventListener, gameStats: gameStats).SetPhaseEvent(child);

                child.OnTapDownAction(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheResetOnTapDownActionMethod : BattleRoundsEventsTests
        {
            [Test]
            public void When_ResetOnTapdownAction_Is_Called_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit();

                var phaseEvents = GetBattleRoundEvents();

                child.OnPointerEnterInfo += phaseEvents.ConnectEvent;
                phaseEvents.ResetOnTapDownAction(child);

                Assert.IsNull(child.OnTapDownAction);
            }
        }
    }
}

