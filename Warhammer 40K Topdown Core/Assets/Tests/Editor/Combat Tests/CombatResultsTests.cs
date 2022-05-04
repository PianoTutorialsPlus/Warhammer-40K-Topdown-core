using Editor.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using WH40K.GameMechanics.Combat;

namespace Editor.CombatTests
{
    public class CombatResultsTests
    {
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
        public class TheHitsProperty
        {
            [Test]
            public void When_Results_Is_Empty_Then_Result_Count_Is_0()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToHit(1)).Hits;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_Results_With_1_Result_Is_Lower_Than_ToHit_Then_Result_Count_Is_0()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToHit(2)
                    .WithResult(1)).Hits;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_Results_With_1_Result_Is_Equal_To_ToHit_Then_Result_Count_Is_1()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToHit(1)
                    .WithResult(1)).Hits;

                Assert.AreEqual(1, result.Count);
            }
            [Test]
            public void When_Results_With_2_Results_Hits_Once_Then_Result_Count_Is_1()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToHit(3)
                    .WithResult(1)
                    .WithResult(6)).Hits;

                Assert.AreEqual(1, result.Count);
            }
            [Test]
            public void When_Results_With_2_Results_Hits_Twice_Then_Result_Count_Is_2()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToHit(1)
                    .WithResult(1)
                    .WithResult(6)).Hits;

                Assert.AreEqual(2, result.Count);
            }
        }
        public class TheWoundsProperty
        {
            [Test]
            public void When_WoundResults_Is_Empty_Then_Result_Count_Is_0()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToWound(1)).Wounds;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_WoundResults_With_1_Result_Is_Lower_Than_ToWound_Then_Result_Count_Is_0()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToWound(2)
                    .WithResult(1)).Wounds;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_WoundResults_With_1_Result_Is_Equal_To_ToWound_Then_Result_Count_Is_1()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToWound(1)
                    .WithResult(1)).Wounds;

                Assert.AreEqual(1, result.Count);
            }
            [Test]
            public void When_WoundResults_With_1_Result_Is_Greater_Than_ToWound_Then_Result_Count_Is_1()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToWound(1)
                    .WithResult(1)).Wounds;

                Assert.AreEqual(1, result.Count);
            }
        }
        public class TheSavesProperty
        {
            [Test]
            public void When_SaveResults_Is_Empty_Then_Result_Count_Is_0()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToWound(1)).Wounds;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_SaveResults_With_1_Result_Is_Lower_Than_ToSave_Then_Result_Count_Is_1()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToSave(2)
                    .WithResult(1)).FailedSaves;

                Assert.AreEqual(1, result.Count);
            }
            [Test]
            public void When_SaveResults_With_1_Result_Is_Equal_To_ToSave_Then_Result_Count_Is_0()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToSave(1)
                    .WithResult(1)).FailedSaves;

                Assert.AreEqual(0, result.Count);
            }
            [Test]
            public void When_SaveResults_With_1_Result_Is_Greater_Than_ToSave_Then_Result_Count_Is_0()
            {
                var result = ((CombatResults)A.CombatResult
                    .WithToSave(1)
                    .WithResult(2)).FailedSaves;

                Assert.AreEqual(0, result.Count);
            }
        }
    }
}