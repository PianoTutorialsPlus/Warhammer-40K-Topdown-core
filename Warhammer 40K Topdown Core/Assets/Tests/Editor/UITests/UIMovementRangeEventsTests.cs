using Editor.Infrastructure;
using NUnit.Framework;
using WH40K.Essentials;
using WH40K.UI;

namespace Editor.UI
{
    class UIMovementRangeEventsTests : UIDisplayEventsTestsBase
    {
        [SetUp]
        public void BeforeEveryTest()
        {
            _action = null;
            _state = false;
        }

        public class TheConnectIndicatorMethod : UIMovementRangeEventsTests
        {
            [Test]
            public void When_ConnectIndicator_Is_Called_Then_IndicatorConnectionUIEvent_Is_Raised()
            {
                var eventListener = GetIndicatorEventListener();
                var child = GetUnit();

                ((UIMovementRangeEvents)A.UIMovementRangeEvent
                    .WithUIEvents(
                        An.UIEvent
                            .WithMoveRangeIndicatorEventListener(eventListener)
                            .Build())).ConnectIndicator(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheSetIndicatorConnectionMethod : UIMovementRangeEventsTests
        {
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsActivated_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(isActivated: true);
                var gameStats = GetGameStats(Fraction.Necrons);

                ((UIMovementRangeEvents)(A.UIMovementRangeEvent
                    .WithUIEvents(An.UIEvent.Build())
                    .WithGameStats(gameStats))).SetIndicatorConnection(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_Is_Not_Activated_Then_OnTapDownAction_IsNotNull()
            {
                var child = GetUnit(isActivated: false);
                var gameStats = GetGameStats(Fraction.Necrons);

                ((UIMovementRangeEvents)(A.UIMovementRangeEvent
                    .WithUIEvents(An.UIEvent.Build())
                    .WithGameStats(gameStats))).SetIndicatorConnection(child);

                Assert.IsNotNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Enemy_Fraction_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(playerFraction: Fraction.SpaceMarines);
                var gameStats = GetGameStats(Fraction.Necrons);

                ((UIMovementRangeEvents)(A.UIMovementRangeEvent
                    .WithUIEvents(An.UIEvent.Build())
                    .WithGameStats(gameStats))).SetIndicatorConnection(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsDone_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(isDone: true);
                var gameStats = GetGameStats(Fraction.Necrons);

                ((UIMovementRangeEvents)(A.UIMovementRangeEvent
                    .WithUIEvents(An.UIEvent.Build())
                    .WithGameStats(gameStats))).SetIndicatorConnection(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_IndicatorConnectionUIEvent_Is_Raised()
            {
                var eventListener = GetIndicatorEventListener();
                var child = GetUnit();
                var gameStats = GetGameStats(Fraction.Necrons, child);

                ((UIMovementRangeEvents)A.UIMovementRangeEvent
                   .WithUIEvents(
                       An.UIEvent
                           .WithMoveRangeIndicatorEventListener(eventListener)
                           .Build())
                   .WithGameStats(gameStats)).SetIndicatorConnection(child);

                child.OnTapDownAction(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheResetOnTapDownActionMethod : UIMovementRangeEventsTests
        {
            [Test]
            public void When_ResetOnTapdownAction_Is_Called_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit();

                var uIMoveRangeEvents = (UIMovementRangeEvents)A.UIMovementRangeEvent
                   .WithUIEvents(
                       An.UIEvent.Build());

                child.OnPointerEnterInfo += uIMoveRangeEvents.ConnectIndicator;
                uIMoveRangeEvents.ResetOnTapDownAction(child);

                Assert.IsNull(child.OnTapDownAction);
            }
        }
    }
}
