using Editor.Base;
using Editor.Infrastructure;
using NUnit.Framework;
using WH40K.Gameplay.EventChannels;
using WH40K.Gameplay.Events;
using WH40K.Stats;
using WH40K.Stats.Player;

namespace Editor.UI
{
    public class UIDisplayInfoEventsTests : UIDisplayEventsTestsBase
    {
        public UIDisplayInfoEvents GetUIDisplayInfoEvents(
            GameStatsSO gameStats = null,
            InfoUIEventChannelSO enemyEventListener = null,
            InfoUIEventChannelSO eventListener = null)
        {
            return A.UIEvent<UIDisplayInfoEvents>()
                    .WithGameStats(gameStats)
                    .WithPlayerEventListener(eventListener)
                    .WithEnemyEventListener(enemyEventListener);
        }

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
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);
                var child = GetUnit();

                GetUIDisplayInfoEvents(gameStats:gameStats,eventListener:eventListener)
                    .DisplayInfoUI(child);

                Assert.IsTrue(_state);
            }
            [Test]
            public void When_Unit_Fraction_Equals_Enemy_Fraction_Then_EnemyInfoUIEvent_Is_Raised()
            {
                var eventListener = GetInfoEventListener();
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);
                var child = GetUnit(playerFraction: Fraction.SpaceMarines);

                GetUIDisplayInfoEvents(gameStats: gameStats, enemyEventListener: eventListener)
                    .DisplayInfoUI(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheSetDisplayInfoMethod : UIDisplayInfoEventsTests
        {
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsActivated_Then_OnPointerEnterInfo_IsNotNull()
            {
                var child = GetUnit(isActivated: true);
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);

                GetUIDisplayInfoEvents(gameStats: gameStats)
                    .SetDisplayInfo(child);

                Assert.IsNotNull(child.OnPointerEnterInfo);
            }
            [Test]
            public void When_Unit_Is_Not_From_Player_Fraction_Then_OnPointerEnterInfo_IsNotNull()
            {
                var child = GetUnit(playerFraction: Fraction.SpaceMarines);
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);

                GetUIDisplayInfoEvents(gameStats: gameStats)
                    .SetDisplayInfo(child);

                Assert.IsNotNull(child.OnPointerEnterInfo);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsDone_Then_OnPointerEnterInfo_IsNull()
            {
                var child = GetUnit(isDone: true);
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);

                GetUIDisplayInfoEvents(gameStats: gameStats)
                    .SetDisplayInfo(child);

                Assert.IsNull(child.OnPointerEnterInfo);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_PlayerInfoUIEvent_Is_Raised()
            {
                var eventListener = GetInfoEventListener();
                var child = GetUnit();
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons,unit: child);

                GetUIDisplayInfoEvents(gameStats: gameStats, eventListener: eventListener)
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
                GetGameStats(playerFraction: Fraction.Necrons);

                GetUIDisplayInfoEvents()
                    .SetResetInteraction(child);

                Assert.IsNotNull(child.OnPointerExit);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_Then_EnemyInfoUIEvent_Is_Raised()
            {
                _state = true;

                var eventListener = GetInfoEventListener();
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);
                var child = GetUnit();

                GetUIDisplayInfoEvents(gameStats: gameStats, eventListener: eventListener)
                    .SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsFalse(_state);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_And_Unit_Is_Not_Activated_Then_PlayerInfoUIEvent_Is_Raised()
            {
                _state = true;
                var eventListener = GetInfoEventListener();
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);
                var child = GetUnit(isActivated: false);

                GetUIDisplayInfoEvents(gameStats: gameStats, eventListener: eventListener)
                    .SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsFalse(_state);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_And_Unit_Is_Activated_Then_PlayerInfoUIEvent_Is_Not_Raised()
            {
                _state = true;
                var eventListener = GetInfoEventListener();
                var gameStats = GetGameStats(playerFraction: Fraction.Necrons);
                var child = GetUnit(isActivated: true);

                GetUIDisplayInfoEvents(gameStats: gameStats, eventListener: eventListener)
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

                var uIDisplayInfoEvents = GetUIDisplayInfoEvents();

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

                var uIDisplayInfoEvents = GetUIDisplayInfoEvents();

                uIDisplayInfoEvents.SetResetInteraction(child);
                uIDisplayInfoEvents.ResetOnPointerExit(child);

                Assert.IsNull(child.OnPointerExit);
            }
        }
    }
}