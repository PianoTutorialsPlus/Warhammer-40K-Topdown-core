using Editor.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using WH40K.Essentials;

namespace Editor.Units.Movement
{
    public class UnitMovementControllerTests
    {
        public class TheSetDestinationMethod
        {
            [Test]
            public void When_Unit_With_Movement_0_Is_Set_To_Move_1_Then_Then_x_Position_Is_0()
            {
                var unit = (UnitMovementController)A.UnitMovementController
                    .WithUnitMover(A.UnitMover
                        .WithMaxDistance(0)
                        .WithCurrentPosition(Vector3.zero)
                        .WithPathCalculator(A.PathCalculator.Build())
                        .WithMovementRange(A.MovementRange.WithMaxRange(0))
                        .Build());

                unit.SetDestination(Vector3.right);

                Assert.AreEqual(0, unit.EndPosition.x);
            }

            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_1_Then_Then_x_Position_Is_1()
            {
                var position = Vector3.right;
                var range = 1;

                var unit = (UnitMovementController)A.UnitMovementController
                    .WithUnitMover(A.UnitMover
                        .WithMaxDistance(range)
                        .WithCurrentPosition(Vector3.zero)
                        .WithPathCalculator(A.PathCalculator
                            .WithEndPosition(position)
                            .Build())
                        .Build());

                unit.SetDestination(position);

                Assert.AreEqual(1, unit.EndPosition.x);
            }

            [Test]
            public void When_Unit_With_Movement_1_Is_Set_To_Move_2_Then_Then_x_Position_Is_1()
            {
                var position = new Vector3(2, 0, 0);
                var range = 1;

                var unit = (UnitMovementController)A.UnitMovementController
                    .WithUnitMover(A.UnitMover
                        .WithMaxDistance(range)
                        .WithCurrentPosition(Vector3.zero)
                        .WithPathCalculator(A.PathCalculator
                            .WithEndPosition(Vector3.right)
                            .Build())
                        .Build());

                unit.SetDestination(position);

                Assert.AreEqual(1, unit.EndPosition.x);
            }
            [Test]
            public void When_NavMeshAgent_Is_Not_Stopped_Then_Then_PathCalculator_SetDestination_Is_Called()
            {
                var count = 0;
                var pathCalculator = A.PathCalculator.Build();
        
                pathCalculator
                    .When(x => x.SetDestination(Arg.Any<Vector3>()))
                    .Do(x => count++);

                var unit = (UnitMovementController)A.UnitMovementController
                    .WithUnitMover(A.UnitMover
                        .WithPathCalculator(pathCalculator)
                        .Build());

                unit.SetDestination(Vector3.right);

                Assert.AreEqual(1, count);
            }
            [Test]
            public void When_NavMeshAgent_Is__Stopped_Then_Then_PathCalculator_SetDestination_Is_Not_Called()
            {
                var count = 0;
                var pathCalculator = A.PathCalculator.Build();

                pathCalculator.AgentIsStopped.Returns(true);
                pathCalculator
                    .When(x => x.SetDestination(Arg.Any<Vector3>()))
                    .Do(x => count++);

                var unit = (UnitMovementController)A.UnitMovementController
                    .WithUnitMover(A.UnitMover
                        .WithPathCalculator(pathCalculator)
                        .Build());

                unit.SetDestination(Vector3.right);

                Assert.AreEqual(0, count);
            }
        }
        public class TheFreezeUnitsWithZeroDistanceMethod
        {
            [Test]
            public void When_MovementRange_Is_0_Then_PathCalculator_FreezeAgent_Is_Called()
            {
                var count = 0;
                var pathCalculator = A.PathCalculator.Build();

                pathCalculator
                    .When(x => x.FreezeAgent())
                    .Do(x => count++);
                
                var unit = (UnitMovementController)A.UnitMovementController
                    .WithUnitMover(A.UnitMover
                        .WithMovementRange(A.MovementRange.WithMaxRange(0))
                        .WithPathCalculator(pathCalculator)
                        .Build());

                unit.FreezeUnitsWithZeroMoveDistance();

                Assert.AreEqual(1, count);
            }
            [Test]
            public void When_MovementRange_Is_1_Then_PathCalculator_FreezeAgent_Is_Not_Called()
            {
                var count = 0;
                var pathCalculator = A.PathCalculator.Build();

                pathCalculator
                    .When(x => x.FreezeAgent())
                    .Do(x => count++);

                var unit = (UnitMovementController)A.UnitMovementController
                    .WithUnitMover(A.UnitMover
                        .WithMovementRange(A.MovementRange.WithMaxRange(1))
                        .WithPathCalculator(pathCalculator)
                        .Build());

                unit.FreezeUnitsWithZeroMoveDistance();

                Assert.AreEqual(0, count);
            }
        }
    }
}