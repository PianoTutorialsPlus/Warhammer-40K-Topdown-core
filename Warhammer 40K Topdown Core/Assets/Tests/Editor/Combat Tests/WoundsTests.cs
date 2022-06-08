using Editor.Infrastructure;
using NUnit.Framework;
using System.Collections.Generic;
using WH40K.Gameplay.Combat;

namespace Editor.CombatTests
{
    public class WoundsTests
    {
        public class TheTakeDamageMethod
        {
            [Test]
            public void When_Count_Of_Unsaved_Wounds_Is_0_Then_0_Wounds_Are_Taken()
            {
                var wounds = (Wounds)A.Wound.WithUnsavedWoundList(new List<int>());

                Assert.AreEqual(0, wounds.TakeDamage(2));
            }
            [Test]
            public void When_1_Unsaved_Wounds_Has_Value_Of_0_Then_0_Wounds_Are_Taken()
            {
                var wounds = (Wounds)A.Wound.WithUnsavedWoundList(new List<int>() { 0 });

                Assert.AreEqual(0, wounds.TakeDamage(2));
            }
            [Test]
            public void When_1_Unsaved_Wounds_Takes_1_Damage_Then_1_Wounds_Is_Taken()
            {
                var wounds = (Wounds)A.Wound.WithUnsavedWoundList(new List<int>() { 1 });

                Assert.AreEqual(1, wounds.TakeDamage(1));
            }
            [Test]
            public void When_1_Unsaved_Wounds_Takes_2_Damage_Then_2_Wounds_Are_Taken()
            {
                var wounds = (Wounds)A.Wound.WithUnsavedWoundList(new List<int>() { 1 });

                Assert.AreEqual(2, wounds.TakeDamage(2));
            }
            [Test]
            public void When_2_Unsaved_Wounds_Takes_1_Damage_Each_Then_2_Wounds_Are_Taken()
            {
                var wounds = (Wounds)A.Wound.WithUnsavedWoundList(new List<int>() { 2, 2 });

                Assert.AreEqual(2, wounds.TakeDamage(1));
            }
            [Test]
            public void When_2_Unsaved_Wounds_Takes_2_Damage_Each_Then_4_Wounds_Are_Taken()
            {
                var wounds = (Wounds)A.Wound.WithUnsavedWoundList(new List<int>() { 2, 2 });

                Assert.AreEqual(4, wounds.TakeDamage(2));
            }
        }
    }
}
