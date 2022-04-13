using Editor.Infrastructure;
using NUnit.Framework;
using UnityEngine.Events;
using WH40K.Essentials;
using WH40K.UI;

namespace Editor.UI
{
    public class UIDisplayInfoEventsTests
    {
        public UnityAction<IUnit> _action;
        public bool _state;
        [SetUp]
        public void BeforeEveryTest()
        {
            _action = null;
            _state = false;
        }
        public void FillWithStats(bool state, IStats stats)
        {
            _state = state;
        }
        public class TheDisplayInfoUIMethod : UIDisplayInfoEventsTests
        {
            [Test]
            public void When_Unit_Fraction_Equals_Player_Fraction_Then_PlayerInfoUIEvent_Is_Raised()
            {
                InfoUIEventChannelSO eventListener = A.InfoUIEventChannel;
                eventListener.OnEventRaised += FillWithStats;

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                    .WithUIEvents(
                        An.UIEvent
                            .WithPlayerEventListener(eventListener)
                            .Build())
                    .WithPlayerFraction(Fraction.Necrons)).DisplayInfoUI(
                        A.Unit.Build());

                Assert.IsTrue(_state);
            }
            [Test]
            public void When_Unit_Fraction_Equals_Enemy_Fraction_Then_EnemyInfoUIEvent_Is_Raised()
            {
                InfoUIEventChannelSO eventListener = A.InfoUIEventChannel;
                eventListener.OnEventRaised += FillWithStats;

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                    .WithUIEvents(
                        An.UIEvent
                            .WithEnemyEventListener(eventListener)
                            .Build())
                    .WithPlayerFraction(Fraction.Necrons)).DisplayInfoUI(
                        A.Unit
                            .WithFraction(Fraction.SpaceMarines)
                            .Build());

                Assert.IsTrue(_state);
            }
        }
        public class TheSetDisplayInfoMethod : UIDisplayInfoEventsTests
        {
            UIDisplayInfoEvents _uIDisplayInfoEvents;
            [SetUp]
            public void ExtendBeforeEveryTest()
            {
                _uIDisplayInfoEvents = A.UIDisplayInfoEvent
                    .WithUIEvents(
                        An.UIEvent
                            .Build())
                    .WithPlayerFraction(Fraction.Necrons);
            }

            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsActivated_Then_OnPointerEnterInfo_IsNotNull()
            {
                var child = A.Unit
                    .WithOnPointerEnterInfo(_action)
                    .WithIsActivatedState(true)
                    .Build();

                _uIDisplayInfoEvents.SetDisplayInfo(child);

                Assert.IsNotNull(child.OnPointerEnterInfo);
            }
            [Test]
            public void When_Unit_Is_Not_From_Player_Fraction_Then_OnPointerEnterInfo_IsNotNull()
            {
                var child = A.Unit
                    .WithOnPointerEnterInfo(_action)
                    .WithFraction(Fraction.SpaceMarines)
                    .Build();

                _uIDisplayInfoEvents.SetDisplayInfo(child);

                Assert.IsNotNull(child.OnPointerEnterInfo);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsDone_Then_OnPointerEnterInfo_IsNull()
            {
                var child = A.Unit
                    .WithOnPointerEnterInfo(_action)
                    .WithIsDoneState(true)
                    .Build();

                _uIDisplayInfoEvents.SetDisplayInfo(child);

                Assert.IsNull(child.OnPointerEnterInfo);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_PlayerInfoUIEvent_Is_Raised()
            {
                InfoUIEventChannelSO eventListener = A.InfoUIEventChannel;
                eventListener.OnEventRaised += FillWithStats;

                var child = A.Unit
                    .WithOnPointerEnterInfo(_action)
                    .Build();

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                   .WithUIEvents(
                       An.UIEvent
                           .WithPlayerEventListener(eventListener)
                           .Build())
                   .WithPlayerFraction(Fraction.Necrons)).SetDisplayInfo(child);

                child.OnPointerEnterInfo(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheSetResetInteractionMethod : UIDisplayInfoEventsTests
        {
            [Test]
            public void When_SetResetInteraction_Is_Called_Then_OnPointerExit_IsNotNull()
            {
                var child = A.Unit
                    .WithOnPointerExit(_action)
                    .Build();
                
                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                   .WithUIEvents(
                       An.UIEvent
                           .Build())
                   .WithPlayerFraction(Fraction.Necrons)).SetResetInteraction(child);

                Assert.IsNotNull(child.OnPointerExit);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_Then_EnemyInfoUIEvent_Is_Raised()
            {
                _state = true;
                InfoUIEventChannelSO eventListener = A.InfoUIEventChannel;
                eventListener.OnEventRaised += FillWithStats;

                var child = A.Unit
                    .WithOnPointerExit(_action)
                    .Build();

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                   .WithUIEvents(
                       An.UIEvent
                           .WithEnemyEventListener(eventListener)
                           .Build())
                   .WithPlayerFraction(Fraction.Necrons)).SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsFalse(_state);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_And_Unit_Is_Not_Activated_Then_PlayerInfoUIEvent_Is_Raised()
            {
                _state = true;
                InfoUIEventChannelSO eventListener = A.InfoUIEventChannel;
                eventListener.OnEventRaised += FillWithStats;

                var child = A.Unit
                    .WithOnPointerExit(_action)
                    .WithIsActivatedState(false)
                    .Build();

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                   .WithUIEvents(
                       An.UIEvent
                           .WithPlayerEventListener(eventListener)
                           .Build())
                   .WithPlayerFraction(Fraction.Necrons)).SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsFalse(_state);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_And_Unit_Is_Activated_Then_PlayerInfoUIEvent_Is_Not_Raised()
            {
                _state = true;
                InfoUIEventChannelSO eventListener = A.InfoUIEventChannel;
                eventListener.OnEventRaised += FillWithStats;

                var child = A.Unit
                    .WithOnPointerExit(_action)
                    .WithIsActivatedState(true)
                    .Build();

                ((UIDisplayInfoEvents)A.UIDisplayInfoEvent
                   .WithUIEvents(
                       An.UIEvent
                           .WithPlayerEventListener(eventListener)
                           .Build())
                   .WithPlayerFraction(Fraction.Necrons)).SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheResetOnPointerEnterInfoMethod : UIDisplayInfoEventsTests
        {
            [Test]
            public void When_ResetOnPointerEnterInfo_Is_Called_Then_OnPointerEnterInfo_IsNull()
            {
                var child = A.Unit
                    .WithOnPointerEnterInfo(_action)
                    .Build();

                var uIDisplayInfoEvents = (UIDisplayInfoEvents)A.UIDisplayInfoEvent
                   .WithUIEvents(
                       An.UIEvent.Build());

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
                var child = A.Unit
                    .WithOnPointerExit(_action)
                    .Build();

                var uIDisplayInfoEvents = (UIDisplayInfoEvents)A.UIDisplayInfoEvent
                   .WithUIEvents(An.UIEvent.Build());

                uIDisplayInfoEvents.SetResetInteraction(child);
                uIDisplayInfoEvents.ResetOnPointerExit(child);

                Assert.IsNull(child.OnPointerExit);
            }
        }
    }
}