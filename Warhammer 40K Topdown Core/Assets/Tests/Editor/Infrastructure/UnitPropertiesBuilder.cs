using UnityEngine;
using WH40K.Essentials;

namespace Editor.Infrastructure
{
    public class UnitPropertiesBuilder : TestDataBuilder<UnitSO>
    {
        private int _movement = 5;
        private Fraction _fraction = Fraction.Necrons;

        //public UnitPropertiesBuilder()
        //{

        //}

        public UnitPropertiesBuilder WithMovement(int movement)
        {
            _movement = movement;
            return this;
        }

        public UnitPropertiesBuilder WithFraction(Fraction fraction)
        {
            _fraction = fraction;
            return this;
        }

        public override UnitSO Build()
        {
            var newUnitProperties = ScriptableObject.CreateInstance<NecronWarriorSO>();
            //newUnitProperties.SetPrivate(up => up.Movement, _movement);
            newUnitProperties.SetPrivate(up => up.Fraction, _fraction);

            //var newUnitProperties = Substitute.For<IUnitStats>();
            //newUnitProperties.Fraction.Returns(_fraction);
            //_unit.Movement.Returns(5);
            return newUnitProperties;
        }


    }

}