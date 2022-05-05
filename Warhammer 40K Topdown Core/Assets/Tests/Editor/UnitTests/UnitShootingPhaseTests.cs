﻿using Editor.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using WH40K.Essentials;
using static UnityEngine.EventSystems.PointerEventData;

namespace Editor.UnitTests
{
    public class UnitShootingPhaseTests
    {
        public UnityAction _pointerAction;
        public UnityAction<IUnit> _action;

        [SetUp]
        public void BeforeEveryTest()
        {
            _pointerAction = null;
            _action = null;
        }
        public IUnit GetUnit(Fraction playerFraction = Fraction.Necrons, bool isActivated = false, bool isDone = false)
        {
            return A.Unit
                    .WithOnPointerEnterInfo(_action)
                    .WithOnPointerEnter(_pointerAction)
                    .WithOnTapdownAction(_action)
                    .WithOnPointerExit(_action)
                    .WithUnitSelector(A.UnitSelector)
                    .WithFraction(playerFraction)
                    .WithIsActivatedState(isActivated)
                    .WithIsDoneState(isDone)
                    .Build();
        }
        public UnitMovementPhase SetUnitMovementPhase(IUnit unit)
        {
            UnitMovementPhase target = new GameObject().AddComponent<UnitMovementPhase>();
            target.SetPrivate(x => x.Unit, unit);
            return target;
        }

        public void UnityActionFiller(IUnit unit)
        {
        }

        public void UnityActionFiller()
        {
        }
        public class TheOnPointerClickMethod : UnitShootingPhaseTests
        {
            [Test]
            public void When_onTapDownAction_Action_is_Null_Then_onTapDownAction_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerClick(A.PointerEventData);

                //ASSERT
                unit.Received(1).OnTapDownAction(unit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Right_Then_onTapDownAction_Action_Is_Not_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit();
                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerClick(
                    A.PointerEventData.WithButtonPressed(InputButton.Right));

                //ASSERT
                unit.Received(1).OnTapDownAction(unit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Middle_Then_onTapDownAction_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerClick(
                    A.PointerEventData.WithButtonPressed(InputButton.Middle));

                //ASSERT
                unit.Received(1).OnTapDownAction(unit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Left_Then_onTapDownAction_Action_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit();

                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerClick(
                    A.PointerEventData.WithButtonPressed(InputButton.Left));

                //ASSERT
                unit.Received(2).OnTapDownAction(unit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Left_Then_UnitSelector_SelectUnit_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                IUnit unit = GetUnit();

                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerClick(
                    A.PointerEventData.WithButtonPressed(InputButton.Left));

                //ASSERT
                unit.Received(1).UnitSelector.SelectUnit();
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Right_Then_UnitSelector_SelectEnemyUnit_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                IUnit unit = GetUnit();

                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerClick(
                    A.PointerEventData.WithButtonPressed(InputButton.Right));

                //ASSERT
                unit.Received(1).UnitSelector.SelectEnemyUnit();
            }
        }
        public class TheOnPointerEnterMethod : UnitShootingPhaseTests
        {
            [Test]
            public void When_onPointerEnter_Action_is_Null_Then_onPointerEnter_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerEnter(A.PointerEventData);

                //ASSERT
                unit.Received(1).OnPointerEnter();
            }
            [Test]
            public void When_onPointerEnterInfo_Action_is_Null_Then_onPointerEnterInfo_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerEnter(A.PointerEventData);

                //ASSERT
                unit.Received(1).OnPointerEnterInfo(unit);
            }
            [Test]
            public void When_onPointerEnter_Action_Has_Value_Then_onPointerEnter_Action_Is_Invoked()
            {
                //ARRANGE
                _pointerAction = UnityActionFiller;
                var unit = GetUnit();
                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerEnter(A.PointerEventData);

                //ASSERT
                unit.Received(2).OnPointerEnter();
            }
            [Test]
            public void When_onPointerEnterInfo_Action_Has_Value_Then_onPointerEnterInfo_Action_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit();


                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerEnter(A.PointerEventData);

                //ASSERT
                unit.Received(2).OnPointerEnterInfo(unit);
            }
        }
        public class TheOnPointerExitMethod : UnitShootingPhaseTests
        {
            [Test]
            public void When_onPointerExit_Action_is_Null_Then_onPointerExit_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerExit(A.PointerEventData);

                //ASSERT
                unit.Received(1).OnPointerExit(unit);
            }
            [Test]
            public void When_onPointerExit_Action_Has_Value_Then_onPointerExit_Action_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit();
                var unitShootingPhase = SetUnitMovementPhase(unit);

                //ACT
                unitShootingPhase.OnPointerExit(A.PointerEventData);

                //ASSERT
                unit.Received(2).OnPointerExit(unit);
            }
        }
    }
}