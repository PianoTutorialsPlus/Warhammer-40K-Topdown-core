using Editor.Base;
using NSubstitute;
using NUnit.Framework;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats.Player;
using static UnityEngine.EventSystems.PointerEventData;

namespace Editor.UnitTests
{
    public class UnitShootingPhaseTests : UnitPhasesTestsBase
    {
        public UnitShootingPhase SetUnitShootingPhase(IUnit unit,UnitSelector unitSelector = null)
        {
            return SetUnitPhase<UnitShootingPhase>(unit,unitSelector);
        }

        [SetUp]
        public void BeforeEveryTest()
        {
            _pointerAction = null;
            _action = null;
        }

        public class TheOnPointerClickMethod : UnitShootingPhaseTests
        {
            [Test]
            public void When_onTapDownAction_Action_is_Null_Then_onTapDownAction_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitShootingPhase = SetUnitShootingPhase(unit);

                //ACT
                unitShootingPhase.OnPointerClick(GetPointerEvent());

                //ASSERT
                unit.Received(1).OnTapDownAction(unit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Right_Then_onTapDownAction_Action_Is_Not_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit(pointer: _action);
                var unitShootingPhase = SetUnitShootingPhase(unit);

                //ACT
                unitShootingPhase.OnPointerClick(
                    GetPointerEvent(button: InputButton.Right));

                //ASSERT
                unit.Received(1).OnTapDownAction(unit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Middle_Then_onTapDownAction_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitShootingPhase = SetUnitShootingPhase(unit);

                //ACT
                unitShootingPhase.OnPointerClick(
                    GetPointerEvent(button: InputButton.Middle));

                //ASSERT
                unit.Received(1).OnTapDownAction(unit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Left_Then_onTapDownAction_Action_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit(pointer: _action);

                var unitShootingPhase = SetUnitShootingPhase(unit);

                //ACT
                unitShootingPhase.OnPointerClick(
                    GetPointerEvent(button: InputButton.Left));

                //ASSERT
                unit.Received(2).OnTapDownAction(unit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Left_Then_UnitSelector_SelectUnit_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit(pointer: _action);
                var gameStats = GetGameStats();

                // ACT
                var unitSelector = GetUnitSelector(gameStats: gameStats);
                var unitShootingPhase = SetUnitShootingPhase(unit,unitSelector);

                //ACT
                unitShootingPhase.OnPointerClick(
                    GetPointerEvent(button: InputButton.Left));

                //ASSERT
                Assert.IsNotNull(gameStats.ActiveUnit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Right_Then_UnitSelector_SelectEnemyUnit_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit(pointer: _action);
                var gameStats = GetGameStats();

                // ACT
                var unitSelector = GetUnitSelector(gameStats: gameStats);
                var unitShootingPhase = SetUnitShootingPhase(unit, unitSelector);

                //ACT
                unitShootingPhase.OnPointerClick(
                    GetPointerEvent(button: InputButton.Right));

                //ASSERT
                Assert.IsNotNull(gameStats.EnemyUnit);
            }
        }
        public class TheOnPointerEnterMethod : UnitShootingPhaseTests
        {
            [Test]
            public void When_onPointerEnter_Action_is_Null_Then_onPointerEnter_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitShootingPhase = SetUnitShootingPhase(unit);

                //ACT
                unitShootingPhase.OnPointerEnter(GetPointerEvent());

                //ASSERT
                unit.Received(1).OnPointerEnter();
            }
            [Test]
            public void When_onPointerEnterInfo_Action_is_Null_Then_onPointerEnterInfo_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitShootingPhase = SetUnitShootingPhase(unit);

                //ACT
                unitShootingPhase.OnPointerEnter(GetPointerEvent());

                //ASSERT
                unit.Received(1).OnPointerEnterInfo(unit);
            }
            [Test]
            public void When_onPointerEnter_Action_Has_Value_Then_onPointerEnter_Action_Is_Invoked()
            {
                //ARRANGE
                _pointerAction = UnityActionFiller;
                var unit = GetUnit(pointerWithArgument: _pointerAction);
                var unitShootingPhase = SetUnitShootingPhase(unit);

                //ACT
                unitShootingPhase.OnPointerEnter(GetPointerEvent());

                //ASSERT
                unit.Received(2).OnPointerEnter();
            }
            [Test]
            public void When_onPointerEnterInfo_Action_Has_Value_Then_onPointerEnterInfo_Action_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit(pointer: _action);


                var unitShootingPhase = SetUnitShootingPhase(unit);

                //ACT
                unitShootingPhase.OnPointerEnter(GetPointerEvent());

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
                var unitShootingPhase = SetUnitShootingPhase(unit);

                //ACT
                unitShootingPhase.OnPointerExit(GetPointerEvent());

                //ASSERT
                unit.Received(1).OnPointerExit(unit);
            }
            [Test]
            public void When_onPointerExit_Action_Has_Value_Then_onPointerExit_Action_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit(pointer: _action);
                var unitShootingPhase = SetUnitShootingPhase(unit);

                //ACT
                unitShootingPhase.OnPointerExit(GetPointerEvent());

                //ASSERT
                unit.Received(2).OnPointerExit(unit);
            }
        }
    }
}
