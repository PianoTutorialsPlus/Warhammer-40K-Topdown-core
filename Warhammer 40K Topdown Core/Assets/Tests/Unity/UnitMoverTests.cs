using Editor.Infrastructure;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

namespace Unity
{
    public class UnitMoverTests
    {
        public class TheUnitMoverMethod
        {
            private const float Delta = 0.2f;
            private const float Seconds = 0.8f;
            private Vector3 position;
            //protected UnitMover Target;
            protected IPathCalculator PathCalculator;
            protected IUnitStats unit;
            private bool initialize;
            private NecronWarrior dut;
            protected float TargetMoveDistance => Target.MoveDistance;
            public UnitMover Target { get; private set; }

            private GameTable table;

            //protected float TargetDistance => Target.DistanceToMove;
            //protected float TargetMoveDistance => Target.MoveDistance;

            [SetUp]
            public void BeforeEveryTest()
            {
                if (!initialize)
                {
                    table = GameObject.Instantiate(Resources.Load("Table", (typeof(GameTable)))) as GameTable;
                    NavigationBaker builder = new GameObject().AddComponent<NavigationBaker>();
                    table.Surface = table.GetComponent<NavMeshSurface>();

                    builder.surfaces = new NavMeshSurface[1] { table.Surface };
                    builder.BuildNavMesh();
                    builder.name = "Builder";

                    initialize = true;
                }
                dut = GameObject.Instantiate(Resources.Load("Necron Warrior", (typeof(NecronWarrior)))) as NecronWarrior;
                Target = dut.UnitMover;

            }
            [UnityTest]
            public IEnumerator When_Unit_With_Movement_5_Is_Moving_1_Then_MoveDistance_Is_4()
            {
                //ARRANGE
                position = new Vector3(1, 0, 0);

                // ACT
                Target.SetDestination(position);

                // ASSERT
                yield return new WaitForSeconds(Seconds);
                Assert.AreEqual(4, TargetMoveDistance, Delta);
            }

            [UnityTest]
            public IEnumerator When_Unit_With_Movement_5_Is_Moving_6_Then_MoveDistance_Is_0()
            {
                //ARRANGE
                position = new Vector3(6, 0, 0);

                // ACT
                Target.SetDestination(position);

                // ASSERT
                yield return new WaitForSeconds(Seconds);
                Assert.AreEqual(0, TargetMoveDistance, Delta);

            }

            [UnityTest]
            public IEnumerator When_Unit_With_Movement_5_Is_Moving_6_Then_X_Position_Is_5()
            {
                //ARRANGE
                position = new Vector3(6, 0, 0);

                // ACT
                Target.SetDestination(position);

                // ASSERT
                yield return new WaitForSeconds(Seconds);
                Assert.AreEqual(5, dut.transform.position.x, Delta);

            }

            [UnityTest]
            public IEnumerator When_Unit_With_Movement_5_Is_Moving_6_Then_UnitIsDone_Is_True()
            {
                // ARRANGE
                position = new Vector3(6, 0, 0);

                // ACT
                Target.SetDestination(position);

                // ASSERT
                yield return new WaitForSeconds(Seconds);
                Assert.IsTrue(Target.Unit.IsDone);
            }
            [UnityTest]
            public IEnumerator When_Unit_With_Movement_5_Is_Moving_6_Then_AgentIsStopped_Is_True()
            {
                // ARRANGE
                position = new Vector3(6, 0, 0);

                // ACT
                Target.SetDestination(position);

                // ASSERT
                yield return new WaitForSeconds(Seconds);
                Assert.IsTrue(Target.IsAgentStopped);
            }



            [TearDown]
            public void AfterTests()
            {
                dut.Destroy();
            }
        }
    }
}