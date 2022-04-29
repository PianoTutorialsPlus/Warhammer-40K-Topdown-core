using Editor.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using WH40K.Essentials;

namespace Editor.Units.Movement
{
    public class UnitMoverTests
    {
        private Vector3 position;
        protected UnitMover Target;
        public IPathCalculator pathCalculator;
        protected IStats unit;
        private IUnitMover unitMover;
        private UnitMover unitMoverTest;
        private UnitMovementController moveController;
        private bool initialize;
        private int movement;
        

        protected float TargetDistance => unitMover.Range;
        protected float TargetMoveDistance => Target.Range;

        [SetUp]
        public void BeforeEveryTest()
        {
            Target = new GameObject().AddComponent<UnitMover>();
            Target.gameObject.AddComponent<Unit>();
            Target.Awake();
            pathCalculator = Substitute.For<IPathCalculator>();
            //Target.Unit.SetPrivate(t => t.PathCalculator, PathCalculator);
            unit = Substitute.For<IStats>();

            unitMover = Substitute.For<IUnitMover>();
            //unitMoverTest = new GameObject().AddComponent<UnitMover>();

            //unitMoverTest._moveController = new UnitMovementController(unitMover);

            moveController = new UnitMovementController(unitMover);
            //unitMoverTest.Initialize(pathCalculator, unit);
            position = Vector3.zero;
            initialize = false;



        }
        private void UnitMovement(int value)
        {
            //movement = value;
            // unit.Movement.Returns(value);
            unitMover.MaxDistance.Returns(value);

        }
        //private void SetPathDistance(int value)
        //{
        //    PathCalculator.GetDistance(position).Returns(value);
        //    Initialize();
        //}
        //private void Initialize()
        //{
        //    if (!initialize)
        //    {
        //        Target.Initialize(PathCalculator, unit);
        //        initialize = true;
        //    }
        //}
        //private void SetDestinationTimes()
        //{
        //    Target.SetDestination(position);
        //}
        //[Test]
        //public void Template()
        //{
        //    // ARRANGE


        //    // ACT


        //    // ASSERT

        //}
        public class UnitMoverTestsExtensions : UnitMoverTests
        {
            //protected void SetRemainingDistance(int value, int hasMoved = 0)
            //{
            //    //Target.SetPrivate(t => t.MoveDistance, movement - hasMoved);
            //    value -= hasMoved;

            //    SetPathDistance(value);

            //   // PathCalculator.RemainingDistance.Returns(value);
            //}
            protected void HasPath(bool hasPath)
            {
                pathCalculator.IsPathCalculated.Returns(hasPath);
            }
        }
        public class TheSetDestinationMethod : UnitMoverTests
        {
            [Test]
            public void When_Unit_With_Movement_0_Is_Set_To_Move_1_Then_Then_MoveDistance_Is_0()
            {
                // ARRANGE
                UnitMovement(0);

                Vector3 startPosition = Vector3.zero;
                Vector3 endPosition = Vector3.right;

                moveController.SetStartPosition(startPosition);
                moveController.SetDestination(endPosition);
                //SetPathDistance(1);

                // ACT
                //SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(0, TargetDistance);
            }
            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_1_Then_Then_DistanceToMove_Is_1()
            {
                // ARRANGE
                UnitMovement(2);
                //SetPathDistance(1);
                //Target.transform.position = new Vector3(1, 0, 0);
                Vector3 startPosition = Vector3.zero;
                Vector3 endPosition = Vector3.right;

                moveController.SetStartPosition(startPosition);
                //moveController.SetDestination(endPosition);
                unitMover.SetDestination(endPosition);
                //unitMover.CurrentPosition.Returns(endPosition);
                moveController.UpdateMoveDistance();
                // ACT
                //SetDestinationTimes();

                // ASSERT
                Assert.AreEqual(1, TargetDistance);
            }

            //[Test]
            //public void When_Unit_With_Movement_2_Is_Set_To_Move_1_Then_Then_DistanceToMove_Is_1()
            //{
            //    // ARRANGE
            //    UnitMovement(2);
            //    SetPathDistance(1);

            //    // ACT
            //    SetDestinationTimes();

            //    // ASSERT
            //    Assert.AreEqual(1, TargetDistance);
            //}

            //[Test]
            //public void When_Unit_With_Movement_1_Is_Set_To_Move_2_Then_Then_DistanceToMove_Is_1()
            //{
            //    // ARRANGE
            //    UnitMovement(1);
            //    SetPathDistance(2);

            //    // ACT
            //    SetDestinationTimes();

            //    // ASSERT
            //    Assert.AreEqual(1, TargetDistance);
            //}

            //[Test]
            //public void When_Unit_With_Movement_1_Is_Set_To_Move_1_Then_DistanceToMove_Is_1()
            //{
            //    // ARRANGE
            //    UnitMovement(1);
            //    SetPathDistance(1);

            //    // ACT
            //    SetDestinationTimes();

            //    // ASSERT
            //    Assert.AreEqual(1, TargetDistance);
            //}

            //[Test]
            //public void When_Unit_With_Movement_1_Is_Set_To_Move_1_With_Agent_Is_Stopped_Then_DistanceToMove_Is_0()
            //{
            //    // ARRANGE
            //    UnitMovement(1);
            //    SetPathDistance(1);
            //    PathCalculator.AgentIsStopped.Returns(true);

            //    // ACT
            //    SetDestinationTimes();

            //    // ASSERT
            //    Assert.AreEqual(0, TargetDistance);
            //}
        }
        //public class TheSetDistanceToMoveMethod : UnitMoverTestsExtensions
        //{
        //    [Test]
        //    public void When_Unit_With_Movement_3_Is_Moving_1_Then_DistanceToMove_Is_1()
        //    {
        //        // ARRANGE
        //        UnitMovement(3);
        //        SetRemainingDistance(1);
        //        HasPath(true);

        //        // ACT
        //        SetDestinationTimes();

        //        // ASSERT
        //        Assert.AreEqual(1, TargetDistance);
        //    }

        //    [Test]
        //    public void When_Unit_With_Movement_3_Is_Moving_4_Then_DistanceToMove_Is_3()
        //    {
        //        // ARRANGE
        //        UnitMovement(3);
        //        SetRemainingDistance(4);
        //        HasPath(true);

        //        // ACT
        //        SetDestinationTimes();

        //        // ASSERT
        //        Assert.AreEqual(3, TargetDistance);
        //    }

        //    [Test]
        //    public void When_Unit_With_Movement_3_Has_Moved_1_And_Is_Moving_4_Then_DistanceToMove_Is_2()
        //    {
        //        // ARRANGE
        //        UnitMovement(3);
        //        HasPath(true);
        //        // ACT
        //        SetRemainingDistance(4, 1);
        //        SetDestinationTimes();

        //        // ASSERT
        //        Assert.AreEqual(2, TargetDistance);
        //    }
        //    [Test]
        //    public void When_Unit_With_Movement_1_Has_Moved_1_And_Is_Moving_2_Then_DistanceToMove_Is_0()
        //    {
        //        // ARRANGE
        //        UnitMovement(1);
        //        HasPath(true);
        //        // ACT
        //        SetRemainingDistance(2, 1);
        //        SetDestinationTimes();

        //        // ASSERT
        //        Assert.AreEqual(0, TargetDistance);
        //    }
        //    [Test]
        //    public void When_Unit_With_Movement_1_Has_Moved_1_And_Is_Moving_2_Then_UnitIsDone_Is_True()
        //    {
        //        // ARRANGE
        //        UnitMovement(1);
        //        HasPath(true);
        //        SetRemainingDistance(2, 1);

        //        // ACT
        //        SetDestinationTimes();

        //        // ASSERT
        //        Assert.IsTrue(Target.Unit.IsDone);
        //    }

        //}
        //public class TheMoveDistanceProperty : UnitMoverTestsExtensions
        //{
        //    [Test]
        //    public void When_Unit_With_Movement_3_Is_Moving_2_Then_MoveDistance_Is_3()
        //    {
        //        // ARRANGE
        //        UnitMovement(3);
        //        HasPath(true);

        //        // ACT
        //        SetRemainingDistance(2);
        //        SetDestinationTimes();

        //        // ASSERT
        //        Assert.AreEqual(3, TargetMoveDistance);
        //    }
        //    [Test]
        //    public void When_Unit_With_Movement_3_Has_Moved_1_And_Is_Moving_4_Then_MoveDistance_Is_2()
        //    {
        //        // ARRANGE
        //        UnitMovement(3);
        //        HasPath(true);

        //        // ACT
        //        SetRemainingDistance(4, 1);
        //        SetDestinationTimes();

        //        // ASSERT
        //        Assert.AreEqual(2, TargetMoveDistance);
        //    }

        //    //[Test]
        //    //public void When_Unit_With_Movement_3_Is_Set_To_Move_4_While_Moving_1_Then_MoveDistance_Is_2()
        //    //{
        //    //    // ARRANGE
        //    //    UnitMovement(3);
        //    //    HasPath(true);
        //    //    SetRemainingDistance(1);

        //    //    // ACT
        //    //    SetDestinationTimes();

        //    //    SetRemainingDistance(4);
        //    //    SetDestinationTimes();

        //    //    // ASSERT
        //    //    Assert.AreEqual(2, TargetMoveDistance);
        //    //}
        //    [Test]
        //    public void When_Unit_With_Movement_3_Has_Moved_1_And_Is_Moving_1_Then_MoveDistance_Is_2()
        //    {
        //        // ARRANGE
        //        UnitMovement(3);
        //        HasPath(true);

        //        // ACT
        //        SetRemainingDistance(1, 1);
        //        SetDestinationTimes();

        //        // ASSERT
        //        Assert.AreEqual(2, TargetMoveDistance);
        //    }
        //}
    }
}