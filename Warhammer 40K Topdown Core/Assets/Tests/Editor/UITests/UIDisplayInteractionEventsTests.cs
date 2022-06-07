using Editor.Infrastructure;
using NUnit.Framework;
using WH40K.Events;
using WH40K.PlayerEvents;

namespace Editor.UI
{
    public class UIDisplayInteractionEventsTests : UIDisplayEventsTestsBase
    {
        [SetUp]
        public void BeforeEveryTest()
        {
            _action = null;
            _state = false;
        }

        public class TheDisplayInteractionUIMethod : UIDisplayInteractionEventsTests
        {
            [Test]
            public void When_DisplayInteractionUI_Is_Called_Then_InteractionUIEvent_Is_Raised()
            {
                var eventListener = GetInteractionEventListener();

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                    .WithInteractionEventListener(eventListener))
                    .DisplayInteractionUI();

                Assert.IsTrue(_state);
            }
        }
        public class TheSetDisplayInteractionMethod : UIDisplayInteractionEventsTests
        {
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_Unit_Is_Active_Then_OnPointerEnter_IsNotNull()
            {
                var child = GetUnit();
                GetGameStats(Fraction.Necrons, child);

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent)
                    .SetDisplayInteraction(child);

                Assert.IsNotNull(child.OnPointerEnter);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_Unit_Is_Not_Active_Then_OnPointerEnter_IsNull()
            {
                var child = GetUnit();
                GetGameStats(Fraction.Necrons);

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent)
                    .SetDisplayInteraction(child);

                Assert.IsNull(child.OnPointerEnter);
            }
            [Test]
            public void When_Unit_Is_From_Enemy_Fraction_Then_OnPointerEnter_IsNull()
            {
                var child = GetUnit(playerFraction: Fraction.SpaceMarines);
                GetGameStats(Fraction.Necrons);

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent)
                    .SetDisplayInteraction(child);

                Assert.IsNull(child.OnPointerEnter);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsDone_Then_OnPointerEnterInfo_IsNull()
            {
                var child = GetUnit(isDone: true);
                GetGameStats(Fraction.Necrons);

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent)
                    .SetDisplayInteraction(child);

                Assert.IsNull(child.OnPointerEnter);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsActivated_Then_OnPointerEnterInfo_IsNull()
            {
                var child = GetUnit(isActivated: true);
                GetGameStats(Fraction.Necrons, child);

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent)
                    .SetDisplayInteraction(child);

                Assert.IsNull(child.OnPointerEnter);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_InteractionUIEvent_Is_Raised()
            {
                var eventListener = GetInteractionEventListener();
                var child = GetUnit();
                GetGameStats(Fraction.Necrons, child);

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                    .WithInteractionEventListener(eventListener))
                    .SetDisplayInteraction(child);

                child.OnPointerEnter();

                Assert.IsTrue(_state);
            }
        }
        public class TheSetResetInteractionMethod : UIDisplayInteractionEventsTests
        {
            [Test]
            public void When_SetResetInteraction_Is_Called_Then_OnPointerExit_IsNotNull()
            {
                var child = GetUnit();
                GetGameStats(Fraction.Necrons);

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent)
                    .SetResetInteraction(child);

                Assert.IsNotNull(child.OnPointerExit);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_Then_InteractionUIEvent_Is_Raised()
            {
                _state = true;
                var eventListener = GetInteractionEventListener();
                var child = GetUnit();
                GetGameStats(Fraction.Necrons);

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                    .WithInteractionEventListener(eventListener))
                    .SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsFalse(_state);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_And_Unit_Is_Not_Active_Then_InteractionUIEvent_Is_Not_Raised()
            {
                _state = true;
                var eventListener = GetInteractionEventListener();
                var child = GetUnit();
                GetGameStats(Fraction.Necrons, child);

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                    .WithInteractionEventListener(eventListener))
                    .SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheResetOnPointerEnterInfoMethod : UIDisplayInteractionEventsTests
        {
            [Test]
            public void When_ResetOnPointerEnter_Is_Called_Then_OnPointerEnter_IsNull()
            {
                var child = GetUnit();

                var uIDisplayInteractionEvents =
                    (UIDisplayInteractionEvents)A.UIDisplayInteractionEvent;

                child.OnPointerEnter += uIDisplayInteractionEvents.DisplayInteractionUI;
                uIDisplayInteractionEvents.ResetOnPointerEnter(child);

                Assert.IsNull(child.OnPointerEnter);
            }
        }
        public class TheResetOnPointerEnterExitMethod : UIDisplayInteractionEventsTests
        {
            [Test]
            public void When_ResetOnPointerExit_Is_Called_Then_OnPointerExit_IsNull()
            {
                var child = GetUnit();

                var uIDisplayInteractionEvents =
                    (UIDisplayInteractionEvents)A.UIDisplayInteractionEvent;

                uIDisplayInteractionEvents.SetResetInteraction(child);
                uIDisplayInteractionEvents.ResetOnPointerExit(child);

                Assert.IsNull(child.OnPointerExit);
            }
        }
    }
}

