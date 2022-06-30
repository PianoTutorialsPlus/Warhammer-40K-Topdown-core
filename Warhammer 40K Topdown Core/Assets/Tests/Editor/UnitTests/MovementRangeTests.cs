using Editor.Base;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Units.Movement
{
    public class MovementRangeTests : UnitElementsBase
    {
        public class TheMoveRangeProperty : MovementRangeTests
        {
            [Test]
            public void When_MaxRange_Is_0_Then_MoveRange_Is_0()
            {
                var movementRange = GetMovementRange(maxRange: 0);

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_Then_MoveRange_Is_1()
            {
                var movementRange = GetMovementRange(maxRange: 1);

                Assert.AreEqual(1, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_0_With_Start_X_0_And_Current_X_1_Then_MoveRange_Is_0()
            {
                var movementRange = GetMovementRange(
                    startPosition: Vector3.zero, currentPosition: Vector3.right);

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_0_And_Current_X_0_Then_MoveRange_Is_1()
            {
                var movementRange = GetMovementRange(
                    maxRange: 1, startPosition: Vector3.zero);

                Assert.AreEqual(1, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_0_And_Current_X_1_Then_MoveRange_Is_0()
            {
                var movementRange = GetMovementRange(
                    maxRange: 1, startPosition: Vector3.zero, currentPosition: Vector3.right);

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_0_And_Current_X_2_Then_MoveRange_Is_0()
            {
                var movementRange = GetMovementRange(
                    maxRange: 1, startPosition: Vector3.zero, currentPosition: new Vector3(2, 0, 0));

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_0_And_Current_X_Is_Negative_Then_MoveRange_Is_0()
            {
                var movementRange = GetMovementRange(
                    maxRange: 1, startPosition: Vector3.zero, currentPosition: Vector3.left);

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_2_With_Start_X_0_And_Current_X_Is_1_Then_MoveRange_Is_1()
            {
                var movementRange = GetMovementRange(
                    maxRange: 2, startPosition: Vector3.zero, currentPosition: Vector3.right);

                Assert.AreEqual(1, movementRange.MoveRange);
            }
        }
        public class TheIsMoveRangeZeroProperty : MovementRangeTests
        {
            [Test]
            public void When_MaxRange_Is_0_Then_IsMoveRangeZero_Is_True()
            {
                var movementRange = GetMovementRange(maxRange: 0);

                Assert.IsTrue(movementRange.IsMoveRangeZero);
            }
            [Test]
            public void When_MaxRange_Is_1_Then_IsMoveRangeZero_Is_False()
            {
                var movementRange = GetMovementRange(maxRange: 1);

                Assert.IsFalse(movementRange.IsMoveRangeZero);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_0_And_Current_X_1_Then_IsMoveRangeZero_Is_True()
            {
                var movementRange = GetMovementRange(
                    maxRange: 1, startPosition: Vector3.zero, currentPosition: Vector3.right);

                Assert.IsTrue(movementRange.IsMoveRangeZero);
            }
        }
        public class TheSetStartPositionMethod : MovementRangeTests
        {
            [Test]
            public void When_startPosition_X_Is_0_Then_Current_Position_X_Is_0()
            {
                var movementRange = GetMovementRange();
                movementRange.SetStartPosition(Vector3.zero);

                Assert.AreEqual(0, movementRange.CurrentPosition.x);
            }
            [Test]
            public void When_startPosition_X_Is_1_Then_Current_Position_X_Is_1()
            {
                var movementRange = GetMovementRange(startPosition: Vector3.zero);
                movementRange.SetStartPosition(Vector3.right);

                Assert.AreEqual(1, movementRange.CurrentPosition.x);
            }
        }
        public class TheUpdateRangeMethod : MovementRangeTests
        {
            [Test]
            public void When_MaxRange_Is_0_Then_MoveRange_Is_0()
            {
                var movementRange = GetMovementRange(maxRange: 0);

                movementRange.UpdateRange();

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_Pos_Is_0_And_Current_X_Pos_Is_1_Then_MoveRange_Is_0()
            {
                var movementRange = GetMovementRange(
                    maxRange: 0, startPosition: Vector3.zero, currentPosition: Vector3.right);

                movementRange.UpdateRange();

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_Pos_Is_0_And_Current_X_Pos_Is_1_Then_Start_X_Is_1()
            {
                var movementRange = GetMovementRange(
                    maxRange: 1, startPosition: Vector3.zero, currentPosition: Vector3.right);

                movementRange.UpdateRange();

                Assert.AreEqual(1, movementRange.StartPosition.x);
            }
            [Test]
            public void When_MaxRange_Is_3_With_Current_X_Pos_Is_1_And_End_Position_X_2_Pos_Is_Then_MoveRange_Is_1()
            {
                var movementRange = GetMovementRange(
                    maxRange: 3, startPosition: Vector3.zero, currentPosition: Vector3.right);

                movementRange.UpdateRange();
                movementRange.UpdatePosition(new Vector3(2, 0, 0));

                Assert.AreEqual(1, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_3_With_Current_X_Pos_Is_1_And_End_Position_X_Pos_Is_2_Then_Start_Position_X_Is_1()
            {
                var movementRange = GetMovementRange(
                    maxRange: 3, startPosition: Vector3.zero, currentPosition: Vector3.right);

                movementRange.UpdateRange();
                movementRange.UpdatePosition(new Vector3(2, 0, 0));

                Assert.AreEqual(1, movementRange.StartPosition.x);
            }
        }
        public class TheUpdatePositionMethod : MovementRangeTests
        {
            [Test]
            public void When_startPosition_X_Is_0_And_currentPosition_X_Is_1_Then_Current_Position_X_Is_1()
            {
                var movementRange = GetMovementRange(startPosition: Vector3.zero);

                movementRange.UpdatePosition(Vector3.right);

                Assert.AreEqual(1, movementRange.CurrentPosition.x);
            }
        }
    }
}