using Editor.Base;
using Editor.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using WH40K.NavMesh;
using WH40K.Stats.Player;

namespace Editor.UnitTests.Movement
{
    public class UnitMovementControllerTests : UnitElementsBase
    {
        public UnitMovementController GetUnitMovementController(
            IUnit unit = null,
            MovementRange movementRange = null,
            IPathCalculator pathCalculator = null)
        {
            return A.UnitMovementController
                .WithUnit(unit)
                .WithMovementRange(movementRange)
                .WithPathCalculator(pathCalculator);
        }

        public IPathCalculator GetPathCalculator(
            Vector3 endPosition = new Vector3(),
            bool agentIsStopped = false)
        {
            return A.PathCalculator
                .WithEndPosition(endPosition)
                .WithAgentIsStopped(agentIsStopped)
                .Build();
        }

        public class TheSetDestinationMethod : UnitMovementControllerTests
        {
            [Test]
            public void When_Unit_With_Movement_0_Is_Set_To_Move_1_Then_Then_x_Position_Is_0()
            {
                var unit = GetUnit(currentPosition: Vector3.zero);
                var movementRange = GetMovementRange(maxRange: 0);
                var pathCalculator = GetPathCalculator();

                var movementController = GetUnitMovementController(
                    unit: unit, movementRange: movementRange, pathCalculator: pathCalculator);

                movementController.SetDestination(Vector3.right);

                Assert.AreEqual(0, movementController.EndPosition.x);
            }

            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_1_Then_Then_x_Position_Is_1()
            {
                var position = Vector3.right;

                var unit = GetUnit(currentPosition: Vector3.zero);
                var movementRange = GetMovementRange(maxRange: 1);
                var pathCalculator = GetPathCalculator(endPosition: position);

                var movementController = GetUnitMovementController(
                    unit: unit, movementRange: movementRange, pathCalculator: pathCalculator);

                movementController.SetDestination(position);

                Assert.AreEqual(1, movementController.EndPosition.x);
            }

            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_2_Then_Then_x_Position_Is_1()
            {
                var position = new Vector3(2, 0, 0);
            
                var unit = GetUnit(currentPosition: Vector3.zero);
                var movementRange = GetMovementRange(maxRange: 1);
                var pathCalculator = GetPathCalculator(endPosition: Vector3.right);

                var movementController = GetUnitMovementController(
                    unit: unit, movementRange: movementRange, pathCalculator: pathCalculator);

                movementController.SetDestination(position);

                Assert.AreEqual(1, movementController.EndPosition.x);
            }
            [Test]
            public void When_NavMeshAgent_Is_Not_Stopped_Then_Then_PathCalculator_SetDestination_Is_Called()
            {
                var count = 0;
                var pathCalculator = GetPathCalculator();

                pathCalculator
                    .When(x => x.SetDestination(Arg.Any<Vector3>()))
                    .Do(x => count++);

                GetUnitMovementController(pathCalculator: pathCalculator)
                    .SetDestination(Vector3.right);

                Assert.AreEqual(1, count);
            }
            [Test]
            public void When_NavMeshAgent_Is_Stopped_Then_Then_PathCalculator_SetDestination_Is_Not_Called()
            {
                var count = 0;
                var pathCalculator = GetPathCalculator(agentIsStopped: true);

                pathCalculator
                    .When(x => x.SetDestination(Arg.Any<Vector3>()))
                    .Do(x => count++);

                GetUnitMovementController(pathCalculator: pathCalculator)
                    .SetDestination(Vector3.right);

                Assert.AreEqual(0, count);
            }
        }
        public class TheFreezeUnitsWithZeroDistanceMethod : UnitMovementControllerTests
        {
            [Test]
            public void When_MovementRange_Is_0_Then_PathCalculator_FreezeAgent_Is_Called()
            {
                var count = 0;
                var pathCalculator = GetPathCalculator();
                var movementRange = GetMovementRange(maxRange: 0);

                pathCalculator
                    .When(x => x.FreezeAgent())
                    .Do(x => count++);

                GetUnitMovementController(movementRange: movementRange, pathCalculator: pathCalculator)
                    .FreezeUnitsWithZeroMoveDistance(); 

                Assert.AreEqual(1, count);
            }
            [Test]
            public void When_MovementRange_Is_1_Then_PathCalculator_FreezeAgent_Is_Not_Called()
            {
                var count = 0;
                var pathCalculator = GetPathCalculator();
                var movementRange = GetMovementRange(maxRange: 1);

                pathCalculator
                    .When(x => x.FreezeAgent())
                    .Do(x => count++);

                GetUnitMovementController(movementRange: movementRange, pathCalculator: pathCalculator)
                    .FreezeUnitsWithZeroMoveDistance();

                Assert.AreEqual(0, count);
            }
        }
    }
}