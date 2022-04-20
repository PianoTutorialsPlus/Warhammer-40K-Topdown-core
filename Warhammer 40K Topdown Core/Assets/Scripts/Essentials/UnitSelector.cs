using UnityEngine;

namespace WH40K.Essentials
{
    public class UnitSelector
    {
        private Fraction _playerFraction;
        private IUnit _unit;

        public bool IsSelected => UnitIsFromFraction();

        public UnitSelector(Fraction fraction, IUnit unit)
        {
            _playerFraction = fraction;
            _unit = unit;
        }

        public IUnit GetUnit(Fraction enemyFraction = Fraction.None)
        {
            return UnitIsFromFraction(enemyFraction)
                ? _unit
                : null;
        }

        public bool UnitIsFromFraction(Fraction enemyFraction = Fraction.None)
        {
            Debug.Log("player Fraction: "+ _playerFraction);
            return enemyFraction == Fraction.None
                ? _unit.Fraction == _playerFraction
                : _unit.Fraction == enemyFraction;
        }
    }
}