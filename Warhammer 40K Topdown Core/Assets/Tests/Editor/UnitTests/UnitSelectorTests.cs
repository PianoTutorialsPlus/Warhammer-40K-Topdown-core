using Editor.Infrastructure;
using NUnit.Framework;
using WH40K.Essentials;

namespace Editor.Units
{
    public class UnitSelectorTests
    {
        public UnitSelector SetUnitSelector(Fraction fraction)
        {
            return A.UnitSelector
                .WithGameStats(A.GameStats
                    .WithActivePlayer(A.Player.WithFraction(fraction)))
                .WithUnit(A.Unit.Build());
        }
        public UnitSelector SetUnitSelector(GameStatsSO gameStats)
        {
            return A.UnitSelector
                .WithGameStats(gameStats)
                .WithUnit(A.Unit.Build());
        }
        public GameStatsSO SetGameStats(Fraction fraction)
        {
            return A.GameStats
                .WithActivePlayer(A.Player.WithFraction(fraction));
        }

        public class TheSelectUnitMethod : UnitSelectorTests
        {
            [Test]
            public void When_Unit_Is_From_Enemy_Fraction_Then_Active_Unit_Is_Null()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;
                var gameStats = SetGameStats(fraction);
          
                // ACT
                var unitSelector = SetUnitSelector(gameStats);
                unitSelector.SelectUnit();

                // ASSERT
                Assert.IsNull(gameStats.ActiveUnit);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_Active_Unit_Has_Value()
            {
                // ARRANGE
                var fraction = Fraction.Necrons;
                var gameStats = SetGameStats(fraction);

                // ACT
                var unitSelector = SetUnitSelector(gameStats);
                unitSelector.SelectUnit();

                // ASSERT
                Assert.IsNotNull(gameStats.ActiveUnit);
            }
        }
        public class TheGetUnitMethod : UnitSelectorTests
        {
            [Test]
            public void When_Unit_With_Fraction_Space_Marines_Is_Compared_To_Necrons_Then_Null_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;

                // ACT
                var unitSelector = SetUnitSelector(fraction);
                var unit = unitSelector.GetUnit();

                // ASSERT
                Assert.IsNull(unit);
            }

            [Test]
            public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_Unit_With_Fraction_Necrons_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.Necrons;

                // ACT
                var unitSelector = SetUnitSelector(fraction);
                var unit = unitSelector.GetUnit();

                // ASSERT
                Assert.AreEqual(Fraction.Necrons, unit.Fraction);
            }

            [Test]
            public void When_Enemy_Unit_With_Fraction_Space_Marines_Is_Compared_To_Necrons_Then_Null_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;

                // ACT
                var unitSelector = SetUnitSelector(fraction);
                var unit = unitSelector.GetUnit();

                // ASSERT
                Assert.IsNull(unit);
            }

            [Test]
            public void When_Enemy_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_Unit_With_Fraction_Necrons_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.Necrons;

                // ACT
                var unitSelector = SetUnitSelector(fraction);
                var unit = unitSelector.GetUnit();

                // ASSERT
                Assert.AreEqual(Fraction.Necrons, unit.Fraction);
            }
        }

        public class TheUnitIsFromFractionMethod : UnitSelectorTests
        {
            [Test]
            public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_True_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.Necrons;

                // ACT
                var unitSelector = SetUnitSelector(fraction);

                // ASSERT
                Assert.IsTrue(unitSelector.UnitIsFromFraction());
            }
            [Test]
            public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Space_Marines_Then_False_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;

                // ACT
                var unitSelector = SetUnitSelector(fraction);

                // ASSERT
                Assert.IsFalse(unitSelector.UnitIsFromFraction());
            }

            [Test]
            public void When_Enemy_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_True_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.Necrons;

                // ACT
                var unitSelector = SetUnitSelector(fraction);

                // ASSERT
                Assert.IsTrue(unitSelector.UnitIsFromFraction(fraction));
            }

            [Test]
            public void When_Enemy_Unit_With_Fraction_Necrons_Is_Compared_To_Space_Marines_Then_False_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;

                // ACT
                var unitSelector = SetUnitSelector(fraction);

                // ASSERT
                Assert.IsFalse(unitSelector.UnitIsFromFraction(fraction));
            }
        }
    }
}