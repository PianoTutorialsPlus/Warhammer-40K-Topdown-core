using Editor.Infrastructure;
using Editor.UI;
using NUnit.Framework;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace Editor.GameMechanics
{
    public class BattleRoundsEventsTests : UIDisplayEventsTestsBase
    {
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

                ((BattleRoundEvents)A.BattleRoundEvent
                    .WithUIEvents(
                        An.UIEvent
                            .WithPhaseEventListener(eventListener)
                            .Build())).ConnectEvent(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheSetPhaseEventMethod : BattleRoundsEventsTests
        {
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsActivated_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(isActivated: true);
                GetGameStats(Fraction.Necrons);

                ((BattleRoundEvents)(A.BattleRoundEvent
                    .WithUIEvents(An.UIEvent.Build())
                    )).SetPhaseEvent(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_Is_Not_Activated_Then_OnTapDownAction_IsNotNull()
            {
                var child = GetUnit(isActivated: false);
                GetGameStats(Fraction.Necrons);

                ((BattleRoundEvents)(A.BattleRoundEvent
                    .WithUIEvents(An.UIEvent.Build())))
                    .SetPhaseEvent(child);

                Assert.IsNotNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Enemy_Fraction_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(playerFraction: Fraction.SpaceMarines);
                GetGameStats(Fraction.Necrons);

                ((BattleRoundEvents)(A.BattleRoundEvent
                    .WithUIEvents(An.UIEvent.Build())))
                    .SetPhaseEvent(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsDone_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(isDone: true);
                GetGameStats(Fraction.Necrons);

                ((BattleRoundEvents)(A.BattleRoundEvent
                    .WithUIEvents(An.UIEvent.Build())))
                    .SetPhaseEvent(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_SetPhaseEvent_Is_Raised()
            {
                var eventListener = GetBattleRoundEventListener();
                var child = GetUnit();
                GetGameStats(Fraction.Necrons, child);

                ((BattleRoundEvents)A.BattleRoundEvent
                   .WithUIEvents(
                       An.UIEvent
                           .WithPhaseEventListener(eventListener)
                           .Build()))
                   .SetPhaseEvent(child);

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

                var phaseEvents = (BattleRoundEvents)A.BattleRoundEvent
                   .WithUIEvents(An.UIEvent.Build());

                child.OnPointerEnterInfo += phaseEvents.ConnectEvent;
                phaseEvents.ResetOnTapDownAction(child);

                Assert.IsNull(child.OnTapDownAction);
            }
        }
    }
}

