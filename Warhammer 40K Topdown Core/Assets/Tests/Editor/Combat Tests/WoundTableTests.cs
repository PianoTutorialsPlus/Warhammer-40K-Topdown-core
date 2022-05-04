using Editor.Infrastructure;
using NUnit.Framework;
using System;
using WH40K.GameMechanics.Combat;

namespace Editor.CombatTests
{
    public class WoundTableTests
    {
        public WoundTable GetWoundTable()
        {
            return A.WoundTable;
        }

        public class TheToWoundMethod : WoundTableTests
        {
            [Test]
            public void When_Strength_Is_Lower_Than_1_Then_ArgumentOutOfRange_Exception_Is_Thrown()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new WoundTable().ToWound(0, 1));
            }
            [Test]
            public void When_Toughness_Is_Lower_Than_1_Then_ArgumentOutOfRange_Exception_Is_Thrown()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new WoundTable().ToWound(1, 0));
            }
            [Test]
            public void When_Strength_Is_Double_The_Value_Of_Toughness_Then_ToWound_Is_2()
            {
                var WoundTable = GetWoundTable();
                var toWound = WoundTable.ToWound(2, 1);
                Assert.AreEqual(2, toWound);
            }
            [Test]
            public void When_Strength_Is_Greater_Than_Toughness_Then_ToWound_Is_3()
            {
                var WoundTable = GetWoundTable();
                var toWound = WoundTable.ToWound(3, 2);
                Assert.AreEqual(3, toWound);
            }
            [Test]
            public void When_Strength_Equals_Toughness_Then_ToWound_Is_4()
            {
                var WoundTable = GetWoundTable();
                var toWound = WoundTable.ToWound(3, 3);
                Assert.AreEqual(4, toWound);
            }
            [Test]
            public void When_Strength_Is_Lower_Than_Toughness_Then_ToWound_Is_5()
            {
                var WoundTable = GetWoundTable();
                var toWound = WoundTable.ToWound(2, 3);
                Assert.AreEqual(5, toWound);
            }
            [Test]
            public void When_Strength_Is_Half_The_Value_Of_Toughness_Then_ToWound_Is_6()
            {
                var WoundTable = GetWoundTable();
                var toWound = WoundTable.ToWound(2, 4);
                Assert.AreEqual(6, toWound);
            }
        }
    }
}