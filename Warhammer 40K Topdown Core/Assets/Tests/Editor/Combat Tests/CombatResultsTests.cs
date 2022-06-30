using Editor.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using WH40K.Gameplay.Combat;
using WH40K.Stats.Combat;

namespace Editor.CombatTests
{
    public class CombatResultsTests
    {
        public CombatResults GetCombatResults(
            int equalizer = 0,
            int result_1 = 0,
            int result_2 = 0)
        {
            return A.CombatResult
                    .WithEqualizer(equalizer)
                    .WithResult(result_1)
                    .WithResult(result_2);
        }
        public class TheCombatResultsConstructor
        {
            [Test]
            public void When_Equalizer_Is_Lower_Than_1_Then_ArgumentOutOfRange_Exception_Is_Thrown()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new CombatResults(0, new List<int>()));
            }
            [Test]
            public void When_A_Result_Is_Lower_Than_1_Then_ArgumentOutOfRange_Exception_Is_Thrown()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new CombatResults(1, new List<int>() { 0 }));
            }
            [Test]
            public void When_Equalizer_Is_Greater_Than_6_Then_ArgumentOutOfRange_Exception_Is_Thrown()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new CombatResults(7, new List<int>()));
            }
            [Test]
            public void When_A_Result_Is_Greater_Than_6_Then_ArgumentOutOfRange_Exception_Is_Thrown()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new CombatResults(1, new List<int>() { 7 }));
            }
        }
        public class TheHitsProperty : CombatResultsTests
        {
            [Test]
            public void When_Results_Is_Empty_Then_Result_Count_Is_0()
            {
                var result = GetCombatResults(equalizer: 1).Hits;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_Results_With_1_Result_Is_Lower_Than_ToHit_Then_Result_Count_Is_0()
            {
                var result = GetCombatResults(equalizer: 2,result_1: 1).Hits;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_Results_With_1_Result_Is_Equal_To_ToHit_Then_Result_Count_Is_1()
            {
                var result = GetCombatResults(equalizer: 1, result_1: 1).Hits;

                Assert.AreEqual(1, result.Count);
            }
            [Test]
            public void When_Results_With_2_Results_Hits_Once_Then_Result_Count_Is_1()
            {
                var result = GetCombatResults(equalizer: 3, result_1: 1,result_2: 6).Hits;

                Assert.AreEqual(1, result.Count);
            }
            [Test]
            public void When_Results_With_2_Results_Hits_Twice_Then_Result_Count_Is_2()
            {
                var result = GetCombatResults(equalizer: 1, result_1: 1, result_2: 6).Hits;

                Assert.AreEqual(2, result.Count);
            }
        }
        public class TheWoundsProperty : CombatResultsTests
        {
            [Test]
            public void When_WoundResults_Is_Empty_Then_Result_Count_Is_0()
            {
                var result = GetCombatResults(equalizer: 1).Wounds;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_WoundResults_With_1_Result_Is_Lower_Than_ToWound_Then_Result_Count_Is_0()
            {
                var result = GetCombatResults(equalizer: 2,result_1: 1).Wounds;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_WoundResults_With_1_Result_Is_Equal_To_ToWound_Then_Result_Count_Is_1()
            {
                var result = GetCombatResults(equalizer: 1, result_1: 1).Wounds;

                Assert.AreEqual(1, result.Count);
            }
            [Test]
            public void When_WoundResults_With_1_Result_Is_Greater_Than_ToWound_Then_Result_Count_Is_1()
            {
                var result = GetCombatResults(equalizer: 1, result_1: 2).Wounds;

                Assert.AreEqual(1, result.Count);
            }
        }
        public class TheSavesProperty : CombatResultsTests
        {
            [Test]
            public void When_SaveResults_Is_Empty_Then_Result_Count_Is_0()
            {
                var result = GetCombatResults(equalizer: 1).FailedSaves;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_SaveResults_With_1_Result_Is_Lower_Than_ToSave_Then_Result_Count_Is_1()
            {
                var result = GetCombatResults(equalizer: 2,result_1: 1).FailedSaves;

                Assert.AreEqual(1, result.Count);
            }
            [Test]
            public void When_SaveResults_With_1_Result_Is_Equal_To_ToSave_Then_Result_Count_Is_0()
            {
                var result = GetCombatResults(equalizer: 1, result_1: 1).FailedSaves;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_SaveResults_With_1_Result_Is_Greater_Than_ToSave_Then_Result_Count_Is_0()
            {
                var result = GetCombatResults(equalizer: 1, result_1: 2).FailedSaves;

                Assert.AreEqual(0, result.Count);
            }
        }
    }
}