using Editor.Infrastructure;
using NUnit.Framework;
using WH40K.Essentials;

namespace Editor.Units
{
    public class UnitSelectorTests
    {
        public UnitSelector SetUnitSelector(Fraction fraction)
        {
            GetGameStats(fraction);
            return A.UnitSelector
                .WithUnit(A.Unit.Build());
        }

        private void GetGameStats(Fraction fraction)
        {
            A.GameStats
                .WithActivePlayer(A.Player.WithFraction(fraction))
                .WithEnemyPlayer(A.Player.WithFraction(fraction))
                .Build();
        }

        public UnitSelector SetUnitSelector()
        {
            return A.UnitSelector
                .WithUnit(A.Unit.Build());
        }
        public void SetGameStats(Fraction fraction)
        {
            A.GameStats
                .WithActivePlayer(A.Player.WithFraction(fraction)).Build();
        }

        public class TheSelectUnitMethod : UnitSelectorTests
        {
            [Test]
            public void When_Unit_Is_From_Enemy_Fraction_Then_Active_Unit_Is_Null()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;
                SetGameStats(fraction);

                // ACT
                var unitSelector = SetUnitSelector();
                unitSelector.SelectUnit();

                // ASSERT
                Assert.IsNull(GameStats.ActiveUnit);
            }
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_Active_Unit_Has_Value()
            {
                // ARRANGE
                var fraction = Fraction.Necrons;
                SetGameStats(fraction);

                // ACT
                var unitSelector = SetUnitSelector();
                unitSelector.SelectUnit();

                // ASSERT
                Assert.IsNotNull(GameStats.ActiveUnit);
            }
        }
        public class TheSelectEnemyUnitMethod : UnitSelectorTests
        {
            [Test]
            public void When_Unit_Is_From_Player_Fraction_Then_Enemy_Unit_Is_Null()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;
                SetGameStats(fraction);

                // ACT
                var unitSelector = SetUnitSelector();
                unitSelector.SelectEnemyUnit();

                // ASSERT
                Assert.IsNull(GameStats.EnemyUnit);
            }
            [Test]
            public void When_Unit_Is_From_Enemy_Fraction_Then_Enemy_Unit_Has_Value()
            {
                // ARRANGE
                var fraction = Fraction.Necrons;
                SetGameStats(fraction);

                // ACT
                var unitSelector = SetUnitSelector();
                unitSelector.SelectEnemyUnit();

                // ASSERT
                Assert.IsNotNull(GameStats.EnemyUnit);
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