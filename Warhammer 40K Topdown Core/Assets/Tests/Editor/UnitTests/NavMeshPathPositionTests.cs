using Editor.Infrastructure;
using NUnit.Framework;
using UnityEngine;
using WH40K.Essentials;

namespace Editor.Units.Movement
{
    public class NavMeshPathPositionTests
    {
        public class TheEndPositionProperty
        {
            [Test]
            public void When_Range_Is_0_And_Start_Path_X_Is_0_Then_End_Position_X_Is_0()
            {
                NavMeshPathPosition navMeshPosition = A.PathPosition;
                var endPosition = navMeshPosition.EndPosition;

                Assert.AreEqual(0, endPosition.x);
            }
            [Test]
            public void When_Range_Is_0_At_Start_Path_X_Of_1_And_Path_Corners_Is_Range_Of_1_Then_End_Position_X_Is_0()
            {
                Vector3 startPath = Vector3.right;
                NavMeshPathPosition navMeshPosition = A.PathPosition.WithPathCorners(startPath);

                var endPath = navMeshPosition.EndPosition;

                Assert.AreEqual(0, endPath.x);
            }
            [Test]
            public void When_Range_Is_0_And_Moved_1_In_X_Then_End_Position_X_Is_0()
            {
                Vector3 startPath = Vector3.zero;
                Vector3 endPath = Vector3.right;

                NavMeshPathPosition navMeshPosition =
                    A.PathPosition
                        .WithMoveRange(0)
                        .WithPathCorners(startPath, endPath);

                navMeshPosition.EndPosition = endPath;
                var endPosition = navMeshPosition.EndPosition;

                Assert.AreEqual(0, endPosition.x);
            }
            [Test]
            public void When_Range_Is_1_And_Moved_1_In_X_Then_End_Position_X_Is_1()
            {
                Vector3 startPath = Vector3.zero;
                Vector3 endPath = Vector3.right;

                NavMeshPathPosition navMeshPosition =
                    A.PathPosition
                        .WithMoveRange(1)
                        .WithPathCorners(startPath, endPath);

                navMeshPosition.EndPosition = endPath;
                var endPosition = navMeshPosition.EndPosition;


                Assert.AreEqual(1, endPosition.x);
            }
            [Test]
            public void When_Range_Is_1_Moved_2_In_X_Then_End_Position_X_Is_1()
            {
                Vector3 startPath = Vector3.zero;
                Vector3 endPath = new Vector3(2, 0, 0);

                NavMeshPathPosition navMeshPosition =
                    A.PathPosition
                        .WithMoveRange(1)
                        .WithPathCorners(startPath, endPath);

                navMeshPosition.EndPosition = endPath;
                var endPosition = navMeshPosition.EndPosition;


                Assert.AreEqual(1, endPosition.x);
            }
            [Test]
            public void When_Range_5_Moved_6_In_X_And_8_In_Z_Then_End_Position_Is_3_X_And_4_Z()
            {
                Vector3 startPath = Vector3.zero;
                Vector3 endPath = new Vector3(6, 0, 8);

                NavMeshPathPosition navMeshPosition =
                    A.PathPosition
                        .WithMoveRange(5)
                        .WithPathCorners(startPath, endPath);

                navMeshPosition.EndPosition = endPath;
                var endPosition = navMeshPosition.EndPosition;

                Assert.AreEqual(new Vector3(3, 0, 4), endPosition);
            }
            [Test]
            public void When_Range_5_First_Moved_3_In_Neg_X_And_Second_Moved_5_In_Neg_Z_Then_End_Position_Is_Minus_3_X_And_Minus_2_Z()
            {
                Vector3 startPath = Vector3.zero;
                Vector3 midPath = new Vector3(-3, 0, 0);
                Vector3 endPath = new Vector3(-3, 0, -5);

                NavMeshPathPosition navMeshPosition =
                    A.PathPosition
                        .WithMoveRange(5)
                        .WithPathCorners(startPath, midPath, endPath);

                navMeshPosition.EndPosition = endPath;
                var endPosition = navMeshPosition.EndPosition;

                Assert.AreEqual(new Vector3(-3, 0, -2), endPosition);
            }
        }
    }
}