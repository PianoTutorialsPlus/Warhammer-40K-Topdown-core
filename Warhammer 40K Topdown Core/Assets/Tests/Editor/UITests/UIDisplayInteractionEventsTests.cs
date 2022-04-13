using Editor.Infrastructure;
using NUnit.Framework;
using UnityEngine.Events;
using WH40K.Essentials;
using WH40K.UI;

namespace Editor.UI
{
    public class UIDisplayInteractionEventsTests
    {
        public UnityAction<IUnit> _pointerAction;
        public UnityAction _action;
        public bool _state;
        [SetUp]
        public void BeforeEveryTest()
        {
            _action = null;
            _state = false;
        }
        public void FillWithStats(bool state, InteractionType stats)
        {
            _state = state;
        }
        public class TheDisplayInteractionUIMethod : UIDisplayInteractionEventsTests
        {
            [Test]
            public void When_DisplayInteractionUI_Is_Called_Then_InteractionUIEvent_Is_Raised()
            {
                InteractionUIEventChannelSO eventListener = A.InteractionUIEventChannel;
                eventListener.OnEventRaised += FillWithStats;

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                    .WithUIEvents(
                        An.UIEvent
                            .WithInteractionEventListener(eventListener)
                            .Build()))
                    .DisplayInteractionUI();

                Assert.IsTrue(_state);
            }
        }
        public class TheSetDisplayInteractionMethod : UIDisplayInteractionEventsTests
        {
            UIDisplayInteractionEvents _uIDisplayInteractionEvents;
            [SetUp]
            public void ExtendBeforeEveryTest()
            {
                _uIDisplayInteractionEvents = A.UIDisplayInteractionEvent
                    .WithUIEvents(
                        An.UIEvent
                            .Build())
                    .WithPlayerFraction(Fraction.Necrons)
                    .WithActiveUnit(A.Unit.Build());
            }

            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_Unit_Is_Active_Then_OnPointerEnter_IsNotNull()
            {
                var child = A.Unit
                    .WithOnPointerEnter(_action)
                    .Build();

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                    .WithUIEvents(
                        An.UIEvent
                            .Build())
                    .WithPlayerFraction(Fraction.Necrons)
                    .WithActiveUnit(child)).SetDisplayInteraction(child);
                
                Assert.IsNotNull(child.OnPointerEnter);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_Unit_Is_Not_Active_Then_OnPointerEnter_IsNull()
            {
                var child = A.Unit
                    .WithOnPointerEnter(_action)
                    .Build();

                _uIDisplayInteractionEvents.SetDisplayInteraction(child);

                Assert.IsNull(child.OnPointerEnter);
            }
            [Test]
            public void When_Unit_Is_From_Enemy_Fraction_Then_OnPointerEnter_IsNull()
            {
                var child = A.Unit
                    .WithOnPointerEnter(_action)
                    .WithFraction(Fraction.SpaceMarines)
                    .Build();

                _uIDisplayInteractionEvents.SetDisplayInteraction(child);

                Assert.IsNull(child.OnPointerEnter);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsDone_Then_OnPointerEnterInfo_IsNull()
            {
                var child = A.Unit
                    .WithOnPointerEnter(_action)
                    .WithIsDoneState(true)
                    .Build();

                _uIDisplayInteractionEvents.SetDisplayInteraction(child);

                Assert.IsNull(child.OnPointerEnter);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_And_IsActivated_Then_OnPointerEnterInfo_IsNull()
            {
                var child = A.Unit
                    .WithOnPointerEnter(_action)
                    .WithIsActivatedState(true)
                    .Build();

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                    .WithUIEvents(
                        An.UIEvent
                            .Build())
                    .WithPlayerFraction(Fraction.Necrons)
                    .WithActiveUnit(child)).SetDisplayInteraction(child);

                Assert.IsNull(child.OnPointerEnter);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_InteractionUIEvent_Is_Raised()
            {
                InteractionUIEventChannelSO eventListener = A.InteractionUIEventChannel;
                eventListener.OnEventRaised += FillWithStats;

                var child = A.Unit
                    .WithOnPointerEnter(_action)
                    .Build();

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                   .WithUIEvents(
                       An.UIEvent
                           .WithInteractionEventListener(eventListener)
                           .Build())
                   .WithPlayerFraction(Fraction.Necrons)
                   .WithActiveUnit(child)).SetDisplayInteraction(child);

                child.OnPointerEnter();

                Assert.IsTrue(_state);
            }
        }
        public class TheSetResetInteractionMethod : UIDisplayInteractionEventsTests
        {
            [Test]
            public void When_SetResetInteraction_Is_Called_Then_OnPointerExit_IsNotNull()
            {
                var child = A.Unit
                    .WithOnPointerExit(_pointerAction)
                    .Build();

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                   .WithUIEvents(
                       An.UIEvent
                           .Build())
                   .WithPlayerFraction(Fraction.Necrons)).SetResetInteraction(child);

                Assert.IsNotNull(child.OnPointerExit);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_Then_InteractionUIEvent_Is_Raised()
            {
                _state = true;
                InteractionUIEventChannelSO eventListener = A.InteractionUIEventChannel;
                eventListener.OnEventRaised += FillWithStats;

                var child = A.Unit
                    .WithOnPointerExit(_pointerAction)
                    .Build();

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                   .WithUIEvents(
                       An.UIEvent
                           .WithInteractionEventListener(eventListener)
                           .Build())
                   .WithPlayerFraction(Fraction.Necrons)).SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsFalse(_state);
            }
            [Test]
            public void When_OnPointerExit_Is_Invoked_And_Unit_Is_Not_Active_Then_InteractionUIEvent_Is_Not_Raised()
            {
                _state = true;
                InteractionUIEventChannelSO eventListener = A.InteractionUIEventChannel;
                eventListener.OnEventRaised += FillWithStats;

                var child = A.Unit
                    .WithOnPointerExit(_pointerAction)
                    .Build();

                ((UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                   .WithUIEvents(
                       An.UIEvent
                           .WithInteractionEventListener(eventListener)
                           .Build())
                   .WithPlayerFraction(Fraction.Necrons)
                   .WithActiveUnit(child)).SetResetInteraction(child);

                child.OnPointerExit(child);

                Assert.IsTrue(_state);
            }
        }
        public class TheResetOnPointerEnterInfoMethod : UIDisplayInteractionEventsTests
        {
            [Test]
            public void When_ResetOnPointerEnter_Is_Called_Then_OnPointerEnter_IsNull()
            {
                var child = A.Unit
                    .WithOnPointerEnter(_action)
                    .Build();

                var uIDisplayInteractionEvents = (UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                   .WithUIEvents(
                       An.UIEvent.Build());

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
                var child = A.Unit
                    .WithOnPointerExit(_pointerAction)
                    .Build();

                var uIDisplayInteractionEvents = (UIDisplayInteractionEvents)A.UIDisplayInteractionEvent
                   .WithUIEvents(An.UIEvent.Build());

                uIDisplayInteractionEvents.SetResetInteraction(child);
                uIDisplayInteractionEvents.ResetOnPointerExit(child);

                Assert.IsNull(child.OnPointerExit);
            }
        }
    }
}

