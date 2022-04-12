using System.Collections;
using System.Collections.Generic;
using Editor.Infrastructure;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using WH40K.Essentials;

namespace Editor.Unit.Movement
{
    public class MovementRangeTests
    {
        public class TheMoveRangeProperty
        {
            [Test]
            public void When_MaxRange_Is_0_Then_MoveRange_Is_0()
            {
                MovementRange movementRange = A.MovementRange;

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_Then_MoveRange_Is_1()
            {
                MovementRange movementRange = A.MovementRange.WithMaxRange(1);

                Assert.AreEqual(1, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_0_With_Start_X_0_And_Current_X_1_Then_MoveRange_Is_0()
            {
                var startPosition = Vector3.zero;
                var currentPosition = Vector3.right;

                MovementRange movementRange = 
                    A.MovementRange
                        .WithStartPosition(startPosition)
                        .WithCurrentPosition(currentPosition);

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_0_And_Current_X_0_Then_MoveRange_Is_1()
            {
                var startPosition = Vector3.zero;

                MovementRange movementRange =
                    A.MovementRange
                        .WithMaxRange(1)
                        .WithStartPosition(startPosition);

                Assert.AreEqual(1, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_0_And_Current_X_1_Then_MoveRange_Is_0()
            {
                var startPosition = Vector3.zero;
                var currentPosition = Vector3.right;

                MovementRange movementRange =
                    A.MovementRange
                        .WithMaxRange(1)
                        .WithStartPosition(startPosition)
                        .WithCurrentPosition(currentPosition);
                        
                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_0_And_Current_X_2_Then_MoveRange_Is_0()
            {
                var startPosition = Vector3.zero;
                var currentPosition = new Vector3(2,0,0);

                MovementRange movementRange =
                    A.MovementRange
                        .WithMaxRange(1)
                        .WithStartPosition(startPosition)
                        .WithCurrentPosition(currentPosition);

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_0_And_Current_X_Is_Negative_Then_MoveRange_Is_0()
            {
                var startPosition = Vector3.zero;
                var currentPosition = Vector3.left;

                MovementRange movementRange =
                    A.MovementRange
                        .WithMaxRange(1)
                        .WithStartPosition(startPosition)
                        .WithCurrentPosition(currentPosition);

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_2_With_Start_X_0_And_Current_X_Is_1_Then_MoveRange_Is_1()
            {
                var startPosition = Vector3.zero;
                var currentPosition = Vector3.right;

                MovementRange movementRange =
                    A.MovementRange
                        .WithMaxRange(2)
                        .WithStartPosition(startPosition)
                        .WithCurrentPosition(currentPosition);

                Assert.AreEqual(1, movementRange.MoveRange);
            }
        }
        public class TheIsMoveRangeZeroProperty
        {
            [Test]
            public void When_MaxRange_Is_0_Then_IsMoveRangeZero_Is_True()
            {
                MovementRange movementRange = A.MovementRange;

                Assert.IsTrue(movementRange.IsMoveRangeZero);
            }
            [Test]
            public void When_MaxRange_Is_1_Then_IsMoveRangeZero_Is_False()
            {
                MovementRange movementRange = A.MovementRange.WithMaxRange(1);

                Assert.IsFalse(movementRange.IsMoveRangeZero);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_0_And_Current_X_1_Then_IsMoveRangeZero_Is_True()
            {
                var startPosition = Vector3.zero;
                var currentPosition = Vector3.right;

                MovementRange movementRange =
                    A.MovementRange
                        .WithMaxRange(1)
                        .WithStartPosition(startPosition)
                        .WithCurrentPosition(currentPosition);

                Assert.IsTrue(movementRange.IsMoveRangeZero);
            }
        }
        public class TheSetStartPositionMethod
        {
            [Test]
            public void When_startPosition_X_Is_0_Then_Current_Position_X_Is_0()
            {
                var startPosition = Vector3.zero;

                MovementRange movementRange = A.MovementRange;
                movementRange.SetStartPosition(startPosition);

                Assert.AreEqual(0, movementRange.CurrentPosition.x);
            }
            [Test]
            public void When_startPosition_X_Is_1_Then_Current_Position_X_Is_1()
            {
                var startPosition = Vector3.right;

                MovementRange movementRange = A.MovementRange;
                movementRange.SetStartPosition(startPosition);

                Assert.AreEqual(1, movementRange.CurrentPosition.x);
            }
        }
        public class TheUpdateRangeMethod
        {
            [Test]
            public void When_MaxRange_Is_0_Then_MoveRange_Is_0()
            {
                MovementRange movementRange = A.MovementRange;

                movementRange.UpdateRange();

                Assert.AreEqual(0,movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_Pos_Is_0_And_Current_X_Pos_Is_1_Then_MoveRange_Is_0()
            {
                var currentPosition = Vector3.right;

                MovementRange movementRange = 
                    A.MovementRange
                        .WithMaxRange(1)
                        .WithCurrentPosition(currentPosition);

                movementRange.UpdateRange();

                Assert.AreEqual(0, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_1_With_Start_X_Pos_Is_0_And_Current_X_Pos_Is_1_Then_Start_X_Is_1()
            {
                var currentPosition = Vector3.right;

                MovementRange movementRange =
                    A.MovementRange
                        .WithMaxRange(1)
                        .WithCurrentPosition(currentPosition);

                movementRange.UpdateRange();

                Assert.AreEqual(1, movementRange.StartPosition.x);
            }
            [Test]
            public void When_MaxRange_Is_3_With_Current_X_Pos_Is_1_And_End_Position_X_2_Pos_Is_Then_MoveRange_Is_1()
            {
                var currentPosition = Vector3.right;
                var endPosition = new Vector3(2, 0, 0);

                MovementRange movementRange =
                    A.MovementRange
                        .WithMaxRange(3)
                        .WithCurrentPosition(currentPosition);

                movementRange.UpdateRange();
                movementRange.UpdatePosition(endPosition);

                Assert.AreEqual(1, movementRange.MoveRange);
            }
            [Test]
            public void When_MaxRange_Is_3_With_Current_X_Pos_Is_1_And_End_Position_X_Pos_Is_2_Then_Start_Position_X_Is_1()
            {
                var currentPosition = Vector3.right;
                var endPosition = new Vector3(2, 0, 0);

                MovementRange movementRange =
                    A.MovementRange
                        .WithMaxRange(3)
                        .WithCurrentPosition(currentPosition);

                movementRange.UpdateRange();
                movementRange.UpdatePosition(endPosition);

                Assert.AreEqual(1, movementRange.StartPosition.x);
            }
        }
        public class TheUpdatePositionMethod
        {
            [Test]
            public void When_startPosition_X_Is_0_And_currentPosition_X_Is_1_Then_Current_Position_X_Is_1()
            {
                var startPosition = Vector3.zero;
                var currentPosition = Vector3.right;

                MovementRange movementRange =
                    A.MovementRange
                        .WithStartPosition(startPosition);

                movementRange.UpdatePosition(currentPosition);

                Assert.AreEqual(1, movementRange.CurrentPosition.x);
            }
        }
    }
}