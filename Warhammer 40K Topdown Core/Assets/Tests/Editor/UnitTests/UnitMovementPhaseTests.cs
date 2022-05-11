using Editor.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using WH40K.PlayerEvents;
using static UnityEngine.EventSystems.PointerEventData;

namespace Editor.UnitTests
{
    public class UnitMovementPhaseTests : UnitPhasesTestsBase
    {
        public UnitMovementPhase SetUnitMovementPhase(IUnit unit)
        {
            return SetUnitPhase<UnitMovementPhase>(unit);
        }

        [SetUp]
        public void BeforeEveryTest()
        {
            _pointerAction = null;
            _action = null;
        }

        public class TheOnPointerClickMethod : UnitMovementPhaseTests
        {
            [Test]
            public void When_onTapDownAction_Action_is_Null_Then_onTapDownAction_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerClick(A.PointerEventData);

                //ASSERT
                unit.Received(1).OnTapDownAction(unit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Right_Then_onTapDownAction_Action_Is_Not_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit();
                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerClick(
                    A.PointerEventData.WithButtonPressed(InputButton.Right));

                //ASSERT
                unit.Received(1).OnTapDownAction(unit);
            }
            [Test]
            public void When_onTapDownAction_Action_Has_Value_And_Button_Pressed_Is_Middle_Then_onTapDownAction_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerClick(
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

                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerClick(
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

                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerClick(
                    A.PointerEventData.WithButtonPressed(InputButton.Left));

                //ASSERT
                unit.Received(1).UnitSelector.SelectUnit();
            }
        }
        public class TheOnPointerEnterMethod : UnitMovementPhaseTests
        {
            [Test]
            public void When_onPointerEnter_Action_is_Null_Then_onPointerEnter_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerEnter(A.PointerEventData);

                //ASSERT
                unit.Received(1).OnPointerEnter();
            }
            [Test]
            public void When_onPointerEnterInfo_Action_is_Null_Then_onPointerEnterInfo_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerEnter(A.PointerEventData);

                //ASSERT
                unit.Received(1).OnPointerEnterInfo(unit);
            }
            [Test]
            public void When_onPointerEnter_Action_Has_Value_Then_onPointerEnter_Action_Is_Invoked()
            {
                //ARRANGE
                _pointerAction = UnityActionFiller;
                var unit = GetUnit();
                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerEnter(A.PointerEventData);

                //ASSERT
                unit.Received(2).OnPointerEnter();
            }
            [Test]
            public void When_onPointerEnterInfo_Action_Has_Value_Then_onPointerEnterInfo_Action_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit();


                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerEnter(A.PointerEventData);

                //ASSERT
                unit.Received(2).OnPointerEnterInfo(unit);
            }
        }
        public class TheOnPointerExitMethod : UnitMovementPhaseTests
        {
            [Test]
            public void When_onPointerExit_Action_is_Null_Then_onPointerExit_Action_Is_Not_Invoked()
            {
                //ARRANGE
                var unit = GetUnit();
                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerExit(A.PointerEventData);

                //ASSERT
                unit.Received(1).OnPointerExit(unit);
            }
            [Test]
            public void When_onPointerExit_Action_Has_Value_Then_onPointerExit_Action_Is_Invoked()
            {
                //ARRANGE
                _action = UnityActionFiller;
                var unit = GetUnit();
                var unitMovementPhase = SetUnitMovementPhase(unit);

                //ACT
                unitMovementPhase.OnPointerExit(A.PointerEventData);

                //ASSERT
                unit.Received(2).OnPointerExit(unit);
            }
        }
        //public class TheOnPointerEnterUnityAction : UnitMovementPhaseTestsExtensions
        //{
        //    [Test]
        //    public void When_OnPointerEnter_Is_Set_Then_Validation_Text_Is_Returned()
        //    {
        //        ARRANGE
        //        Target.onPointerEnter += UnityActionFiller;

        //        ACT
        //        Target.onPointerEnter();

        //        ASSERT
        //        Assert.AreEqual("Test passed", validationText);
        //    }
        //}
        //public class TheOnPointerEnterInfoUnityAction : UnitMovementPhaseTestsExtensions
        //{
        //    [Test]
        //    public void When_OnPointerEnterInfo_Is_Set_With_A_Unit_Then_Unit_Is_Returned()
        //    {
        //        ARRANGE
        //        Target.onPointerEnterInfo += UnityActionFillerWithArgument;

        //        ACT
        //        Target.onPointerEnterInfo(Target);

        //        ASSERT
        //        Assert.AreEqual(Target, targetUnit);
        //    }
        //}
        //public class TheOnPointerExitUnityAction : UnitMovementPhaseTestsExtensions
        //{
        //    [Test]
        //    public void When_OnPointerExit_Is_Set_With_A_Unit_Then_Unit_Is_Returned()
        //    {
        //        ARRANGE
        //        Target.onPointerExit += UnityActionFillerWithArgument;

        //        ACT
        //        Target.onPointerExit(Target);

        //        ASSERT
        //        Assert.AreEqual(Target, targetUnit);
        //    }
        //}

        //public class TheSelectUnitMethod : UnitMovementPhaseTests
        //{
        //    [Test]
        //    public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_Unit_With_Fraction_Necrons_Is_Returned()
        //    {
        //        ARRANGE
        //       var fraction = Fraction.Necrons;

        //        ACT
        //        SetUnitSelector(fraction);
        //        Target.SelectUnit();

        //        ASSERT
        //        Assert.AreEqual(Fraction.Necrons, TargetFraction);
        //    }

        //    [Test]
        //    public void When_Unit_With_Fraction_Space_Marines_Is_Compared_To_Necrons_Then_ActiveUnit_Is_Null()
        //    {
        //        ARRANGE
        //       var fraction = Fraction.SpaceMarines;

        //        ACT
        //        SetUnitSelector(fraction);
        //        Target.SelectUnit();

        //        ASSERT
        //        Assert.IsNull(ActiveUnit);
        //    }

        //}
        //public class TheSetIsSelectedMethod : UnitMovementPhaseTests
        //{
        //    [Test]
        //    public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_Unit_Is_Selected()
        //    {
        //        ARRANGE
        //       var fraction = Fraction.Necrons;

        //        ACT
        //        SetUnitSelector(fraction);
        //        Target.SetIsSelected();

        //        ASSERT
        //        Assert.IsTrue(Target.IsSelected);
        //    }

        //    [Test]
        //    public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Space_Marines_Then_Unit_Is_Not_Selected()
        //    {
        //        ARRANGE
        //       var fraction = Fraction.SpaceMarines;

        //        ACT
        //        SetUnitSelector(fraction);
        //        Target.SetIsSelected();

        //        ASSERT
        //        Assert.IsFalse(Target.IsSelected);
        //    }
        //}
        //public class TheSetUnitAsEnemyClass : UnitMovementPhaseTests
        //{
        //    [SetUp]
        //    public void AddToEveryTest()
        //    {
        //        GameStats.enemyPlayer = ScriptableObject.CreateInstance<PlayerSO>();
        //    }

        //    [Test]
        //    public void When_Enemy_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_Necrons_Is_Returned()
        //    {
        //        ARRANGE
        //       var fraction = Fraction.Necrons;

        //        ACT
        //        SetUnitSelector(fraction);
        //        Target.SetUnitAsEnemy();

        //        ASSERT
        //        Assert.AreEqual(Fraction.Necrons, EnemyFraction);
        //    }

        //    [Test]
        //    public void When_Enemy_Unit_With_Fraction_Space_Marines_Is_Compared_To_Necrons_Then_Enemy_Unit_Is_Null()
        //    {
        //        ARRANGE
        //       var fraction = Fraction.SpaceMarines;

        //        ACT
        //        SetUnitSelector(fraction);
        //        Target.SetUnitAsEnemy();

        //        ASSERT
        //        Assert.IsNull(EnemyUnit);
        //    }
        //}
    }
}