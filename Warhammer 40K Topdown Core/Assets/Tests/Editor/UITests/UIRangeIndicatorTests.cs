using Editor.Infrastructure;
using NUnit.Framework;
using System;
using UnityEngine;
using WH40K.UI;

namespace Editor.UI
{
    public class UIRangeIndicatorTests
    {
        public IUIRangeIndicator GetRangeIndicator(int baseSize = 0)
        {
            return A.RangeIndicator
                .WithBaseSize(baseSize)
                .Build();
        }
        public UIRangeController GetRangeController(
            IUIRangeIndicator rangeIndicator = null, 
            Vector3 position = new Vector3())
        {
            return A.RangeController
                .WithRangeIndicator(rangeIndicator)
                .WithPosition(position);
        }

        public class TheConnectIndicatorMethod : UIRangeIndicatorTests
        {
            [Test]
            public void When_Current_Position_X_Is_0_Then_Position_X_Is_0()
            {
                var rangeIndicator = GetRangeIndicator();

                GetRangeController(rangeIndicator: rangeIndicator, position: Vector3.zero);

                Assert.AreEqual(0, rangeIndicator.Position.x);
            }
            [Test]
            public void When_Current_Position_Z_Is_0_Then_Position_Z_Is_0()
            {
                var rangeIndicator = GetRangeIndicator();

                GetRangeController(rangeIndicator: rangeIndicator, position: Vector3.zero);

                Assert.AreEqual(0, rangeIndicator.Position.z);
            }
            [Test]
            public void When_Current_Position_X_Is_1_Then_Position_X_Is_1()
            {
                var rangeIndicator = GetRangeIndicator();
                
                GetRangeController(rangeIndicator: rangeIndicator, position: Vector3.right);

                Assert.AreEqual(1, rangeIndicator.Position.x);
            }
            [Test]
            public void When_Current_Position_Z_Is_1_Then_Position_Z_Is_1()
            {
                var rangeIndicator = GetRangeIndicator();
                
                GetRangeController(rangeIndicator: rangeIndicator, position: Vector3.forward);

                Assert.AreEqual(1, rangeIndicator.Position.z);
            }
        }
        public class TheSetActionRadiusCoroutine : UIRangeIndicatorTests
        {
            private const float _baseScale = 3.5f;
            [Test]
            public void When_Range_Is_Negative_Then_ArgumentOutOfRange_Exception_Is_Thrown()
            {
                var rangeIndicator = GetRangeIndicator();
                
                var rangeController = GetRangeController(rangeIndicator: rangeIndicator);

                Assert.Throws<ArgumentOutOfRangeException>(() => rangeController.ScaleRange(-1));
            }
            [Test]
            public void When_Range_Is_0_And_BaseSize_Is_0_Then_LocalScale_X_Is_0()
            {
                var rangeIndicator = GetRangeIndicator(baseSize: 0);

                GetRangeController(rangeIndicator: rangeIndicator)
                    .ScaleRange(0);

                Assert.AreEqual(0 * _baseScale, rangeIndicator.LocalScale.x);
            }
            [Test]
            public void When_Range_Is_5_And_BaseSize_Is_0_Then_LocalScale_X_Is_5_Times_BaseScale()
            {
                var rangeIndicator = GetRangeIndicator(baseSize: 0);

                GetRangeController(rangeIndicator: rangeIndicator)
                    .ScaleRange(5);

                Assert.AreEqual(5 * _baseScale, rangeIndicator.LocalScale.x);
            }
            [Test]
            public void When_Range_Is_5_And_BaseSize_Is_1_Then_LocalScale_X_Is_6_Times_BaseScale()
            {
                var rangeIndicator = GetRangeIndicator(baseSize: 1);

                GetRangeController(rangeIndicator: rangeIndicator)
                  .ScaleRange(5);

                Assert.AreEqual(6 * _baseScale, rangeIndicator.LocalScale.x);
            }
            [Test]
            public void When_Range_Is_0_And_BaseSize_Is_1_Then_LocalScale_X_Is_1_Times_BaseScale()
            {
                var rangeIndicator = GetRangeIndicator(baseSize: 1);

                GetRangeController(rangeIndicator: rangeIndicator)
                  .ScaleRange(0);
                
                Assert.AreEqual(1 * _baseScale, rangeIndicator.LocalScale.x);
            }
        }
    }
}