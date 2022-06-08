using Editor.Infrastructure;
using NUnit.Framework;
using WH40K.Gameplay.Events;
using WH40K.Stats.Player;

namespace Editor.UI
{
    public class UIDisplayInfoEventsTests : UIDisplayEventsTestsBase
    {
        [SetUp]
        public void BeforeEveryTest()
        {
            _action = null;
            _state = false;
        }

        public class TheDisplayInfoUIMethod : UIDisplayInfoEventsTests
        {
            [Test]
            public void When_Unit_Fraction_Equals_Player_Fraction_Then_PlayerInfoUIEvent_Is_Raised()
            {
                var eventListener = GetInfoEventListener();
                GetGameStats(Fraction.Necrons);
                var child = GetUnit();

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                    .WithPlayerEventListener(eventListener))
                     .DisplayInfoUI(child);

                Assert.IsTrue(_state);
            }
            [Test]
            public void When_Unit_Fraction_Equals_Enemy_Fraction_Then_EnemyInfoUIEvent_Is_Raised()
            {
                var eventListener = GetInfoEventListener();
                GetGameStats(Fraction.Necrons);
                var child = GetUnit(playerFraction: Fraction.SpaceMarines);

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                    .WithEnemyEventListener(eventListener))
                    .DisplayInfoUI(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheSetDisplayInfoMethod : UIDisplayInfoEventsTests
        {
            UIDisplayInfoEvents _uIDisplayInfoEvents;
            [SetUp]
            public void ExtendBeforeEveryTest()
            {
                GetGameStats(Fraction.Necrons);
                _uIDisplayInfoEvents = A.UIDisplayInfoEvent;
            }

            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsActivated_Then_OnPointerEnterInfo_IsNotNull()
            {
                var child = GetUnit(isActivated: true);

                _uIDisplayInfoEvents.SetDisplayInfo(child);

                Assert.IsNotNull(child.OnPointerEnterInfo);
            }
            [Test]
            public void When_Unit_Is_Not_From_Player_Fraction_Then_OnPointerEnterInfo_IsNotNull()
            {
                var child = GetUnit(playerFraction: Fraction.SpaceMarines);

                _uIDisplayInfoEvents.SetDisplayInfo(child);

                Assert.IsNotNull(child.OnPointerEnterInfo);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsDone_Then_OnPointerEnterInfo_IsNull()
            {
                var child = GetUnit(isDone: true);

                _uIDisplayInfoEvents.SetDisplayInfo(child);

                Assert.IsNull(child.OnPointerEnterInfo);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_PlayerInfoUIEvent_Is_Raised()
            {
                var eventListener = GetInfoEventListener();
                var child = GetUnit();
                GetGameStats(Fraction.Necrons, child);

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                    .WithPlayerEventListener(eventListener))
                    .SetDisplayInfo(child);

                child.OnPointerEnterInfo(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheSetResetInteractionMethod : UIDisplayInfoEventsTests
        {
            [Test]
            public void When_SetResetInteraction_Is_Called_Then_OnPointerExit_IsNotNull()
            {
                var child = GetUnit();
                GetGameStats(Fraction.Necrons);

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent)
                    .SetResetInteraction(child);

                Assert.IsNotNull(child.OnPointerExit);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_Then_EnemyInfoUIEvent_Is_Raised()
            {
                _state = true;

                var eventListener = GetInfoEventListener();
                GetGameStats(Fraction.Necrons);
                var child = GetUnit();

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                    .WithEnemyEventListener(eventListener))
                    .SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsFalse(_state);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_And_Unit_Is_Not_Activated_Then_PlayerInfoUIEvent_Is_Raised()
            {
                _state = true;
                var eventListener = GetInfoEventListener();
                GetGameStats(Fraction.Necrons);
                var child = GetUnit(isActivated: false);

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                    .WithPlayerEventListener(eventListener))
                    .SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsFalse(_state);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_And_Unit_Is_Activated_Then_PlayerInfoUIEvent_Is_Not_Raised()
            {
                _state = true;
                var eventListener = GetInfoEventListener();
                GetGameStats(Fraction.Necrons);
                var child = GetUnit(isActivated: true);

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                    .WithPlayerEventListener(eventListener))
                    .SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheResetOnPointerEnterInfoMethod : UIDisplayInfoEventsTests
        {
            [Test]
            public void When_ResetOnPointerEnterInfo_Is_Called_Then_OnPointerEnterInfo_IsNull()
            {
                var child = GetUnit();

                var uIDisplayInfoEvents = (UIDisplayInfoEvents)A.UIDisplayInfoEvent;

                child.OnPointerEnterInfo += uIDisplayInfoEvents.DisplayInfoUI;
                uIDisplayInfoEvents.ResetOnPointerEnterInfo(child);

                Assert.IsNull(child.OnPointerEnterInfo);
            }
        }
        public class TheResetOnPointerEnterExitMethod : UIDisplayInfoEventsTests
        {
            [Test]
            public void When_ResetOnPointerExit_Is_Called_Then_OnPointerExit_IsNull()
            {
                var child = GetUnit();

                var uIDisplayInfoEvents = (UIDisplayInfoEvents)A.UIDisplayInfoEvent;

                uIDisplayInfoEvents.SetResetInteraction(child);
                uIDisplayInfoEvents.ResetOnPointerExit(child);

                Assert.IsNull(child.OnPointerExit);
            }
        }
    }
}