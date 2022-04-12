namespace WH40K.Essentials
{
    public class UnitSelector
    {
        private Fraction _playerFraction;
        private IStats _unit;

        public bool IsSelected => UnitIsFromFraction();

        public UnitSelector(Fraction fraction, IStats unit)
        {
            _playerFraction = fraction;
            _unit = unit;
        }

        public IStats GetUnit(Fraction enemyFraction = Fraction.None)
        {
            return UnitIsFromFraction(enemyFraction)
                ? _unit
                : null;
        }

        public bool UnitIsFromFraction(Fraction enemyFraction = Fraction.None)
        {
            return enemyFraction == Fraction.None
                ? _unit.Fraction == _playerFraction
                : _unit.Fraction == enemyFraction;
        }
    }
}