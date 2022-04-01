using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using WH40K.UnitHandler;

namespace Editor.Unit.Movement
{
    public class MovementControllerTests
    {
        public class TheSetDestinationMethod
        {
            [Test]
            public void When_Unit_With_Movement_0_Is_Set_To_Move_1_Then_Then_x_Position_Is_0()
            {

                var unitMover = Substitute.For<IUnitMover>();
                unitMover.MaxDistance.Returns(0);
                unitMover.CurrentPosition.Returns(Vector3.zero);


                IPathCalculator pathCalculator = Substitute.For<IPathCalculator>();
                var position = Vector3.right;
                pathCalculator.GetEndPosition(position).Returns(Vector3.zero);

                unitMover.PathCalculator.Returns(pathCalculator);

                var unit = new UnitMovementController(unitMover);

                unit.SetDestination(position);

                var movedDistance = unit.MovedDistance;

                Assert.AreEqual(0, unit.EndPosition.x);
            }

            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_1_Then_Then_x_Position_Is_1()
            {

                var unitMover = Substitute.For<IUnitMover>();
                unitMover.MaxDistance.Returns(1);
                unitMover.CurrentPosition.Returns(Vector3.zero);


                IPathCalculator pathCalculator = Substitute.For<IPathCalculator>();
                var position = Vector3.right;
                pathCalculator.GetEndPosition(position).Returns(Vector3.right);

                unitMover.PathCalculator.Returns(pathCalculator);

                var unit = new UnitMovementController(unitMover);

                unit.SetDestination(position);

                Assert.AreEqual(1, unit.EndPosition.x);
            }

            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_2_Then_Then_x_Position_Is_1()
            {

                var unitMover = Substitute.For<IUnitMover>();
                unitMover.MaxDistance.Returns(1);
                unitMover.CurrentPosition.Returns(Vector3.zero);

                IPathCalculator pathCalculator = Substitute.For<IPathCalculator>();
                var position = new Vector3(2, 0, 0);
                pathCalculator.GetEndPosition(position).Returns(Vector3.right);

                unitMover.PathCalculator.Returns(pathCalculator);

                var unit = new UnitMovementController(unitMover);

                unit.SetDestination(position);

                Assert.AreEqual(1, unit.EndPosition.x);
            }
        }

    }
}