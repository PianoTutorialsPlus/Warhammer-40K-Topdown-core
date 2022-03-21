using Editor.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor
{
    public class UnitMoverTests
    {
        private Vector3 position;
        protected UnitMover Target;
        protected IPathCalculator PathCalculator;
        protected IUnitStats unit;
        private bool initialize;
        private int movement;

        protected float TargetDistance => Target.DistanceToMove;
        protected float TargetMoveDistance => Target.MoveDistance;

        [SetUp]
        public void BeforeEveryTest()
        {
            Target = new GameObject().AddComponent<UnitMover>();
            Target.Unit = new GameObject().AddComponent<Unit>();
            PathCalculator = Substitute.For<IPathCalculator>();
            Target.Unit.SetPrivate(t => t.PathCalculator, PathCalculator);
            unit = Substitute.For<IUnitStats>();
            position = Vector3.zero;
            initialize = false;

        }
        private void UnitMovement(int value)
        {
            movement = value;
            unit.Movement.Returns(value);
        }
        private void SetPathDistance(int value)
        {
            PathCalculator.GetDistance(position).Returns(value);
            Initialize();
        }
        private void Initialize()
        {
            if (!initialize)
            {
                Target.Initialize(PathCalculator, unit);
                initialize = true;
            }
        }
        private void SetDestinationTimes()
        {
            Target.SetDestination(position);
        }
        //[Test]
        //public void Template()
        //{
        //    // ARRANGE


        //    // ACT


        //    // ASSERT

        //}
        public class UnitMoverTestsExtensions : UnitMoverTests
        {
            protected void SetRemainingDistance(int value, int hasMoved = 0)
            {
                hasMoved += value;

                SetPathDistance(hasMoved);
                PathCalculator.RemainingDistance.Returns(value);
            }
            protected void HasPath(bool hasPath)
            {
                PathCalculator.IsPathCalculated.Returns(hasPath);
            }
        }
        public class TheSetDestinationMethod : UnitMoverTests
        {
            [Test]
            public void When_Unit_With_Movement_0_Is_Set_To_Move_1_Then_Then_DistanceToMove_Is_0()
            {
                // ARRANGE
                UnitMovement(0);

                SetPathDistance(1);

                // ACT
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(0, TargetDistance);
            }
            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_1_Then_Then_DistanceToMove_Is_1()
            {
                // ARRANGE
                UnitMovement(1);
                SetPathDistance(1);

                // ACT
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(1, TargetDistance);
            }

            [Test]
            public void When_Unit_With_Movement_2_Is_Set_To_Move_1_Then_Then_DistanceToMove_Is_1()
            {
                // ARRANGE
                UnitMovement(2);
                SetPathDistance(1);

                // ACT
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(1, TargetDistance);
            }

            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_2_Then_Then_DistanceToMove_Is_1()
            {
                // ARRANGE
                UnitMovement(1);
                SetPathDistance(2);

                // ACT
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(1, TargetDistance);
            }

            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_1_Then_DistanceToMove_Is_1()
            {
                // ARRANGE
                UnitMovement(1);
                SetPathDistance(1);

                // ACT
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(1, TargetDistance);
            }

            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_1_With_Agent_Is_Stopped_Then_DistanceToMove_Is_0()
            {
                // ARRANGE
                UnitMovement(1);
                SetPathDistance(1);
                PathCalculator.AgentIsStopped.Returns(true);

                // ACT
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(0, TargetDistance);
            }
        }
        public class TheSetDistanceToMoveMethod : UnitMoverTestsExtensions
        {
            [Test]
            public void When_Unit_With_Movement_3_Is_Moving_1_Then_DistanceToMove_Is_1()
            {
                // ARRANGE
                UnitMovement(3);
                SetRemainingDistance(1);
                HasPath(true);

                // ACT
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(1, TargetDistance);
            }

            [Test]
            public void When_Unit_With_Movement_3_Is_Moving_4_Then_DistanceToMove_Is_3()
            {
                // ARRANGE
                UnitMovement(3);
                SetRemainingDistance(4);
                HasPath(true);

                // ACT
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(3, TargetDistance);
            }

            [Test]
            public void When_Unit_With_Movement_3_Has_Moved_1_And_Is_Moving_4_Then_DistanceToMove_Is_2()
            {
                // ARRANGE
                UnitMovement(3);
                HasPath(true);
                // ACT
                SetRemainingDistance(4, 1);
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(2, TargetDistance);
            }
            [Test]
            public void When_Unit_With_Movement_1_Has_Moved_1_And_Is_Moving_2_Then_DistanceToMove_Is_0()
            {
                // ARRANGE
                UnitMovement(1);
                HasPath(true);
                // ACT
                SetRemainingDistance(2, 1);
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(0, TargetDistance);
            }
            [Test]
            public void When_Unit_With_Movement_1_Has_Moved_1_And_Is_Moving_2_Then_UnitIsDone_Is_True()
            {
                // ARRANGE
                UnitMovement(1);
                HasPath(true);
                SetRemainingDistance(2, 1);

                // ACT
                SetDestinationTimes();

                // ASSERT
                Assert.IsTrue(Target.Unit.IsDone);
            }

        }
        public class TheMoveDistanceProperty : UnitMoverTestsExtensions
        {
            [Test]
            public void When_Unit_With_Movement_3_Is_Moving_2_Then_MoveDistance_Is_3()
            {
                // ARRANGE
                UnitMovement(3);
                HasPath(true);

                // ACT
                SetRemainingDistance(2);
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(3, TargetMoveDistance);
            }
            [Test]
            public void When_Unit_With_Movement_3_Has_Moved_1_And_Is_Moving_4_Then_MoveDistance_Is_2()
            {
                // ARRANGE
                UnitMovement(3);
                HasPath(true);

                // ACT
                SetRemainingDistance(4, 1);
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(2, TargetMoveDistance);
            }

            //[Test]
            //public void When_Unit_With_Movement_3_Is_Set_To_Move_4_While_Moving_1_Then_MoveDistance_Is_2()
            //{
            //    // ARRANGE
            //    UnitMovement(3);
            //    HasPath(true);
            //    SetRemainingDistance(1);

            //    // ACT
            //    SetDestinationTimes();

            //    SetRemainingDistance(4);
            //    SetDestinationTimes();

            //    // ASSERT
            //    Assert.AreEqual(2, TargetMoveDistance);
            //}
            [Test]
            public void When_Unit_With_Movement_3_Has_Moved_1_And_Is_Moving_1_Then_MoveDistance_Is_2()
            {
                // ARRANGE
                UnitMovement(3);
                HasPath(true);

                // ACT
                SetRemainingDistance(1, 1);
                SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(2, TargetMoveDistance);
            }
        }
    }
}