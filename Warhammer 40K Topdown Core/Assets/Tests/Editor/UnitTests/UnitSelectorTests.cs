using NSubstitute;
using NUnit.Framework;
using WH40K.Essentials;

namespace Editor.Unit
{
    public class UnitSelectorTests
    {
        protected IUnit unit;
        protected UnitSelector unitSelector;
        protected IUnit Target;
        public Fraction TargetFraction => Target.Fraction;

        [SetUp]
        public void BeforeEveryTest()
        {
            unit = Substitute.For<IUnit>();
            unit.Fraction.Returns(Fraction.Necrons);
        }

        public void SetUnitSelector(Fraction fraction)
        {
            unitSelector = new UnitSelector(fraction, unit);
            Target = unitSelector.GetUnit();
        }

        public bool UnitIsFromFraction(Fraction fraction = Fraction.None)
        {
            return unitSelector.UnitIsFromFraction(fraction);
        }

        public class TheGetUnitMethod : UnitSelectorTests
        {
            [Test]
            public void When_Unit_With_Fraction_Space_Marines_Is_Compared_To_Necrons_Then_Null_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;

                // ACT
                SetUnitSelector(fraction);

                // ASSERT
                Assert.IsNull(Target);
            }

            [Test]
            public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_Unit_With_Fraction_Necrons_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.Necrons;

                // ACT
                SetUnitSelector(fraction);

                // ASSERT
                Assert.AreEqual(Fraction.Necrons, TargetFraction);
            }

            [Test]
            public void When_Enemy_Unit_With_Fraction_Space_Marines_Is_Compared_To_Necrons_Then_Null_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;

                // ACT
                SetUnitSelector(fraction);

                // ASSERT
                Assert.IsNull(Target);
            }

            [Test]
            public void When_Enemy_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_Unit_With_Fraction_Necrons_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.Necrons;

                // ACT
                SetUnitSelector(fraction);

                // ASSERT
                Assert.AreEqual(Fraction.Necrons, TargetFraction);
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
                SetUnitSelector(fraction);

                // ASSERT
                Assert.IsTrue(unitSelector.UnitIsFromFraction());
            }
            [Test]
            public void When_Unit_With_Fraction_Necrons_Is_Compared_To_Space_Marines_Then_False_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;

                // ACT
                SetUnitSelector(fraction);

                // ASSERT
                Assert.IsFalse(UnitIsFromFraction());
            }

            [Test]
            public void When_Enemy_Unit_With_Fraction_Necrons_Is_Compared_To_Necrons_Then_True_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.Necrons;

                // ACT
                SetUnitSelector(fraction);

                // ASSERT
                Assert.IsTrue(UnitIsFromFraction(fraction));
            }

            [Test]
            public void When_Enemy_Unit_With_Fraction_Necrons_Is_Compared_To_Space_Marines_Then_False_Is_Returned()
            {
                // ARRANGE
                var fraction = Fraction.SpaceMarines;

                // ACT
                SetUnitSelector(fraction);

                // ASSERT
                Assert.IsFalse(UnitIsFromFraction(fraction));
            }
        }
    }
}