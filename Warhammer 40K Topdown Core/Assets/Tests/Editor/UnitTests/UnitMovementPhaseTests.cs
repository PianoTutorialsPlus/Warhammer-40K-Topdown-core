//using Editor.Infrastructure;
//using NSubstitute;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.EventSystems;

//namespace Editor
//{
//    public class UnitMovementPhaseTests
//    {
//        public IUnitStats ActiveUnit => Target._gameStats.activeUnitTest;
//        public Fraction TargetFraction => ActiveUnit.Fraction;
//        public IUnitStats EnemyUnit => Target._gameStats.enemyUnitTest;
//        public Fraction EnemyFraction => EnemyUnit.Fraction;

//        protected GameStatsSO GameStats;
//        protected UnitMovementPhase Target;
//        protected IUnitStats unit;
//        protected Unit targetUnit;

//        [SetUp]
//        public void BeforeEveryTest()
//        {
//            GameStats = ScriptableObject.CreateInstance<GameStatsSO>();

//            Target = new GameObject().AddComponent<UnitMovementPhase>();
//            Target._gameStats = GameStats;

//            unit = Substitute.For<IUnitStats>();
//            unit.Fraction.Returns(Fraction.Necrons);
//        }

//        public void SetUnitSelector(Fraction fraction)
//        {
//            var unitSelector = new UnitSelector(fraction, unit);
//            Target.SetPrivate(u => u.UnitSelector, unitSelector);
//        }

//        public class UnitMovementPhaseTestsExtensions : UnitMovementPhaseTests
//        {
//            protected EventSystem eventSystem;
//            protected PointerEventData eventData;
//            protected string validationWithArgumentText;
//            protected string validationText;

//            [SetUp]
//            public void AddToEveryTest()
//            {
//                eventSystem = new GameObject().AddComponent<EventSystem>();
//                eventData = new PointerEventData(eventSystem);
//                validationText = null;
//                validationWithArgumentText = null;
//            }

//            public void UnityActionFillerWithArgument(Unit unit)
//            {
//                targetUnit = unit;
//                validationWithArgumentText = "Test passed";
//            }

//            public void UnityActionFiller()
//            {
//                validationText = "Test passed";
//            }
//        }
//        public class TheSelectUnitMethod : UnitMovementPhaseTests
//        {
//            [Test]
//            public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_Unit_With_Fraction_Necrons_Is_Returned()
//            {
//                ARRANGE
//               var fraction = Fraction.Necrons;

//                ACT
//                SetUnitSelector(fraction);
//                Target.SelectUnit();

//                ASSERT
//                Assert.AreEqual(Fraction.Necrons, TargetFraction);
//            }

//            [Test]
//            public void When_Unit_With_Fraction_Space_Marines_Is_Compared_To_Necrons_Then_ActiveUnit_Is_Null()
//            {
//                ARRANGE
//               var fraction = Fraction.SpaceMarines;

//                ACT
//                SetUnitSelector(fraction);
//                Target.SelectUnit();

//                ASSERT
//                Assert.IsNull(ActiveUnit);
//            }

//        }
//        public class TheSetIsSelectedMethod : UnitMovementPhaseTests
//        {
//            [Test]
//            public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_Unit_Is_Selected()
//            {
//                ARRANGE
//               var fraction = Fraction.Necrons;

//                ACT
//                SetUnitSelector(fraction);
//                Target.SetIsSelected();

//                ASSERT
//                Assert.IsTrue(Target.IsSelected);
//            }

//            [Test]
//            public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Space_Marines_Then_Unit_Is_Not_Selected()
//            {
//                ARRANGE
//               var fraction = Fraction.SpaceMarines;

//                ACT
//                SetUnitSelector(fraction);
//                Target.SetIsSelected();

//                ASSERT
//                Assert.IsFalse(Target.IsSelected);
//            }
//        }
//        public class TheSetUnitAsEnemyClass : UnitMovementPhaseTests
//        {
//            [SetUp]
//            public void AddToEveryTest()
//            {
//                GameStats.enemyPlayer = ScriptableObject.CreateInstance<PlayerSO>();
//            }

//            [Test]
//            public void When_Enemy_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_Necrons_Is_Returned()
//            {
//                ARRANGE
//               var fraction = Fraction.Necrons;

//                ACT
//                SetUnitSelector(fraction);
//                Target.SetUnitAsEnemy();

//                ASSERT
//                Assert.AreEqual(Fraction.Necrons, EnemyFraction);
//            }

//            [Test]
//            public void When_Enemy_Unit_With_Fraction_Space_Marines_Is_Compared_To_Necrons_Then_Enemy_Unit_Is_Null()
//            {
//                ARRANGE
//               var fraction = Fraction.SpaceMarines;

//                ACT
//                SetUnitSelector(fraction);
//                Target.SetUnitAsEnemy();

//                ASSERT
//                Assert.IsNull(EnemyUnit);
//            }
//        }
//        public class TheOnPointerClickMethod : UnitMovementPhaseTestsExtensions
//        {
//            [Test]
//            public void When_OnTapDownAction_Is_Set_Then_ActiveUnit_Is_Not_Null()
//            {
//                ARRANGE
//               var fraction = Fraction.Necrons;
//                Target.onTapDownAction += UnityActionFillerWithArgument;

//                ACT
//                SetUnitSelector(fraction);
//                Target.OnPointerClick(eventData);

//                ASSERT
//                Assert.IsNotNull(ActiveUnit);
//            }



//            [Test]
//            public void When_OnTapDownAction_Is_Set_With_Left_Button_Clicked_Then_Validation_Text_Is_Returned()
//            {
//                ARRANGE
//               var fraction = Fraction.Necrons;
//                Target.onTapDownAction += UnityActionFillerWithArgument;

//                ACT
//                SetUnitSelector(fraction);
//                Target.OnPointerClick(eventData);

//                ASSERT
//                Assert.AreEqual("Test passed", validationWithArgumentText);
//            }

//            [Test]
//            public void When_OnTapDownAction_Is_Not_Set_Then_ActiveUnit_Is_Null()
//            {
//                ACT
//                Target.OnPointerClick(eventData);

//                ASSERT
//                Assert.IsNull(ActiveUnit);
//            }

//            [Test]
//            public void When_Unit_With_Fraction_Necrons_Is_Clicked_With_Left_Button_Then_Necrons_Is_Returned()
//            {
//                ARRANGE
//               var fraction = Fraction.Necrons;
//                Target.onTapDownAction += UnityActionFillerWithArgument;

//                eventData.button = PointerEventData.InputButton.Left;

//                ACT
//                SetUnitSelector(fraction);
//                Target.OnPointerClick(eventData);

//                ASSERT
//                Assert.AreEqual(Fraction.Necrons, TargetFraction);
//            }

//            [Test]
//            public void When_Unit_With_Fraction_Space_Marines_Is_Clicked_With_Left_Button_Then_ActiveUnit_Is_Null()
//            {
//                ARRANGE
//               var fraction = Fraction.SpaceMarines;
//                Target.onTapDownAction += UnityActionFillerWithArgument;

//                eventData.button = PointerEventData.InputButton.Left;

//                ACT
//                SetUnitSelector(fraction);
//                Target.OnPointerClick(eventData);

//                ASSERT
//                Assert.IsNull(ActiveUnit);
//            }

//            [Test]
//            public void When_Unit_With_Fraction_Necrons_Is_Clicked_With_Right_Button_Then_ActiveUnit_Is_Null()
//            {
//                ARRANGE
//               var fraction = Fraction.Necrons;
//                Target.onTapDownAction += UnityActionFillerWithArgument;

//                eventData.button = PointerEventData.InputButton.Right;

//                ACT
//                SetUnitSelector(fraction);
//                Target.OnPointerClick(eventData);

//                ASSERT
//                Assert.IsNull(ActiveUnit);
//            }
//        }
//        public class TheOnPointerEnterMethod : UnitMovementPhaseTestsExtensions
//        {
//            [Test]
//            public void When_OnPointerEnterInfo_And_OnPointerEnter_Is_Not_Set_Then_Validation_Text_Is_Null()
//            {
//                ACT
//                Target.OnPointerEnter(eventData);

//                ASSERT
//                Assert.IsNull(validationText);
//                Assert.IsNull(validationWithArgumentText);
//            }

//            [Test]
//            public void When_OnPointerEnter_Is_Set_Then_Validation_Text_Is_Returned()
//            {
//                ARRANGE
//                Target.onPointerEnter += UnityActionFiller;

//                ACT
//                Target.OnPointerEnter(eventData);

//                ASSERT
//                Assert.AreEqual("Test passed", validationText);
//            }

//            [Test]
//            public void When_OnPointerEnterInfo_Is_Set_Then_Validation_Text_Is_Returned()
//            {
//                ARRANGE
//                Target.onPointerEnterInfo += UnityActionFillerWithArgument;

//                ACT
//                Target.OnPointerEnter(eventData);

//                ASSERT
//                Assert.AreEqual("Test passed", validationWithArgumentText);
//            }
//        }
//        public class TheOnTapDownActionUnityAction : UnitMovementPhaseTestsExtensions
//        {
//            [Test]
//            public void When_OnTapDownAction_Is_Set_With_A_Unit_Then_Unit_Is_Returned()
//            {
//                ARRANGE
//                Target.onTapDownAction += UnityActionFillerWithArgument;

//                ACT
//                Target.onTapDownAction(Target);

//                ASSERT
//                Assert.AreEqual(Target, targetUnit);
//            }
//        }
//        public class TheOnPointerEnterUnityAction : UnitMovementPhaseTestsExtensions
//        {
//            [Test]
//            public void When_OnPointerEnter_Is_Set_Then_Validation_Text_Is_Returned()
//            {
//                ARRANGE
//                Target.onPointerEnter += UnityActionFiller;

//                ACT
//                Target.onPointerEnter();

//                ASSERT
//                Assert.AreEqual("Test passed", validationText);
//            }
//        }
//        public class TheOnPointerEnterInfoUnityAction : UnitMovementPhaseTestsExtensions
//        {
//            [Test]
//            public void When_OnPointerEnterInfo_Is_Set_With_A_Unit_Then_Unit_Is_Returned()
//            {
//                ARRANGE
//                Target.onPointerEnterInfo += UnityActionFillerWithArgument;

//                ACT
//                Target.onPointerEnterInfo(Target);

//                ASSERT
//                Assert.AreEqual(Target, targetUnit);
//            }
//        }
//        public class TheOnPointerExitUnityAction : UnitMovementPhaseTestsExtensions
//        {
//            [Test]
//            public void When_OnPointerExit_Is_Set_With_A_Unit_Then_Unit_Is_Returned()
//            {
//                ARRANGE
//                Target.onPointerExit += UnityActionFillerWithArgument;

//                ACT
//                Target.onPointerExit(Target);

//                ASSERT
//                Assert.AreEqual(Target, targetUnit);
//            }
//        }
//    }
//}