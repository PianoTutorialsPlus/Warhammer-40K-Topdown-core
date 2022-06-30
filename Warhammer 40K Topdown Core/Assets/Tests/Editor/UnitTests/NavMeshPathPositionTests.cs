using Editor.Infrastructure;
using NUnit.Framework;
using UnityEngine;
using WH40K.NavMesh;

namespace Editor.Units.Movement
{
    public class NavMeshPathPositionTests
    {
        public NavMeshPathPosition GetPathPosition(
            int moveRange = 0 ,
            Vector3 startPath = new Vector3(),
            Vector3 midPath = new Vector3(),
            Vector3 endPath = new Vector3())
        {
            return A.PathPosition
                .WithMoveRange(moveRange)
                .WithPathCorners(startPath, midPath, endPath);
        }
        public class TheEndPositionProperty : NavMeshPathPositionTests
        {
            [Test]
            public void When_Range_Is_0_And_Start_Path_X_Is_0_Then_End_Position_X_Is_0()
            {
                var endPosition = GetPathPosition().EndPosition;

                Assert.AreEqual(0, endPosition.x);
            }
            [Test]
            public void When_Range_Is_0_At_Start_Path_X_Of_1_And_Path_Corners_Is_Range_Of_1_Then_End_Position_X_Is_0()
            {
                var endPath = GetPathPosition(startPath: Vector3.right).EndPosition;

                Assert.AreEqual(0, endPath.x);
            }
            [Test]
            public void When_Range_Is_0_And_Moved_1_In_X_Then_End_Position_X_Is_0()
            {
                Vector3 endPath = Vector3.right;

                var navMeshPosition = GetPathPosition(
                    moveRange: 0, startPath: Vector3.zero, endPath: endPath);

                navMeshPosition.EndPosition = endPath;
                var endPosition = navMeshPosition.EndPosition;

                Assert.AreEqual(0, endPosition.x);
            }
            [Test]
            public void When_Range_Is_1_And_Moved_1_In_X_Then_End_Position_X_Is_1()
            {
                Vector3 endPath = Vector3.right;

                var navMeshPosition = GetPathPosition(
                    moveRange: 1, startPath: Vector3.zero, endPath: endPath);

                navMeshPosition.EndPosition = endPath;
                var endPosition = navMeshPosition.EndPosition;


                Assert.AreEqual(1, endPosition.x);
            }
            [Test]
            public void When_Range_Is_1_Moved_2_In_X_Then_End_Position_X_Is_1()
            {
                Vector3 endPath = new Vector3(2, 0, 0);

                var navMeshPosition = GetPathPosition(
                    moveRange: 1, startPath: Vector3.zero, endPath: endPath);

                navMeshPosition.EndPosition = endPath;
                var endPosition = navMeshPosition.EndPosition;


                Assert.AreEqual(1, endPosition.x);
            }
            [Test]
            public void When_Range_5_Moved_6_In_X_And_8_In_Z_Then_End_Position_Is_3_X_And_4_Z()
            {
                Vector3 endPath = new Vector3(6, 0, 8);

                var navMeshPosition = GetPathPosition(
                    moveRange: 5, startPath: Vector3.zero, endPath: endPath);

                navMeshPosition.EndPosition = endPath;
                var endPosition = navMeshPosition.EndPosition;

                Assert.AreEqual(new Vector3(3, 0, 4), endPosition);
            }
            [Test]
            public void When_Range_5_First_Moved_3_In_Neg_X_And_Second_Moved_5_In_Neg_Z_Then_End_Position_Is_Minus_3_X_And_Minus_2_Z()
            {
                Vector3 midPath = new Vector3(-3, 0, 0);
                Vector3 endPath = new Vector3(-3, 0, -5);

                var navMeshPosition = GetPathPosition(
                    moveRange: 5, startPath: Vector3.zero, midPath: midPath, endPath: endPath);

                navMeshPosition.EndPosition = endPath;
                var endPosition = navMeshPosition.EndPosition;

                Assert.AreEqual(new Vector3(-3, 0, -2), endPosition);
            }
        }
    }
}