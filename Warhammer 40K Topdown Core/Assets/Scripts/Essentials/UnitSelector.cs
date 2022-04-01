public class UnitSelector
{
    private Fraction _playerFraction;
    private IUnitStats _unit;

    public UnitSelector(Fraction fraction, IUnitStats unit)
    {
        _playerFraction = fraction;
        _unit = unit;
    }

    public IUnitStats GetUnit(Fraction enemyFraction = Fraction.None)
    {
        return (UnitIsFromFraction(enemyFraction))
            ? _unit
            : null;
    }

    public bool UnitIsFromFraction(Fraction enemyFraction = Fraction.None)
    {
        return (enemyFraction == Fraction.None)
            ? _unit.Fraction == _playerFraction
            : _unit.Fraction == enemyFraction;

    }
}
