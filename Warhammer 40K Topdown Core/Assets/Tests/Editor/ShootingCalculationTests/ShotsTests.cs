using NUnit.Framework;
using System;
using WH40K.GameMechanics.Combat;

namespace Editor.ShootingCalculations
{
    public class ShotsTests
    {
        public class TheShotsConstructor
        {
            [Test]
            public void When_MaxShots_Is_Lower_Than_1_Then_ArgumentOutOfRange_Exception_Is_Thrown()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new Shots(0));
            }
        }
        public class TheGetShotsMethod
        {
            [Test]
            public void When_MaxShots_Is_1_Then_Result_Count_Is_1()
            {
                var shots = new Shots(1);
                var result = shots.GetShots();
                Assert.AreEqual(1, result.Count);
            }
            [Test]
            public void When_MaxShots_Is_2_Then_Result_Count_Is_2()
            {
                var shots = new Shots(2);
                var result = shots.GetShots();
                Assert.AreEqual(2, result.Count);
            }
        }
    }
}