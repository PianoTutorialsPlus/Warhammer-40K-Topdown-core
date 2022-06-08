using Editor.Infrastructure;
using NUnit.Framework;
using WH40K.Gameplay.Events;
using WH40K.Stats.Player;

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
                    .WithMoveRangeIndicatorEventListener(eventListener))
                    .ConnectIndicator(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheSetIndicatorConnectionMethod : UIMovementRangeEventsTests
        {
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsActivated_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(isActivated: true);
                GetGameStats(Fraction.Necrons);

                ((UIMovementRangeEvents)A.UIMovementRangeEvent)
                    .SetIndicatorConnection(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_Is_Not_Activated_Then_OnTapDownAction_IsNotNull()
            {
                var child = GetUnit(isActivated: false);
                GetGameStats(Fraction.Necrons);

                ((UIMovementRangeEvents)A.UIMovementRangeEvent)
                    .SetIndicatorConnection(child);

                Assert.IsNotNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Enemy_Fraction_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(playerFraction: Fraction.SpaceMarines);
                GetGameStats(Fraction.Necrons);

                ((UIMovementRangeEvents)A.UIMovementRangeEvent)
                    .SetIndicatorConnection(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsDone_Then_OnTapDownAction_IsNull()
            {
                var child = GetUnit(isDone: true);
                GetGameStats(Fraction.Necrons);

                ((UIMovementRangeEvents)A.UIMovementRangeEvent)
                    .SetIndicatorConnection(child);

                Assert.IsNull(child.OnTapDownAction);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_IndicatorConnectionUIEvent_Is_Raised()
            {
                var eventListener = GetIndicatorEventListener();
                var child = GetUnit();
                GetGameStats(Fraction.Necrons, child);

                ((UIMovementRangeEvents)A.UIMovementRangeEvent
                    .WithMoveRangeIndicatorEventListener(eventListener))
                    .SetIndicatorConnection(child);

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

                var uIMoveRangeEvents = (UIMovementRangeEvents)A.UIMovementRangeEvent;

                child.OnPointerEnterInfo += uIMoveRangeEvents.ConnectIndicator;
                uIMoveRangeEvents.ResetOnTapDownAction(child);

                Assert.IsNull(child.OnTapDownAction);
            }
        }
    }
}
