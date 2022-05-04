namespace WH40K.Essentials
{
    public class UnitSelector
    {
        private GameStatsSO _gameStats;
        private IUnit _unit;

        private Fraction _playerFraction => _gameStats.ActivePlayer.Fraction;
        private Fraction _enemyFraction => _gameStats.EnemyPlayer.Fraction;

        public UnitSelector(GameStatsSO gameStats, IUnit unit)
        {
            _gameStats = gameStats;
            _unit = unit;
        }
        public void SelectUnit()
        {
            SetActiveUnit(GetUnit());
        }

        private void SetActiveUnit(IUnit unit)
        {
            _gameStats.ActiveUnit = unit;
        }
        public void SelectEnemyUnit()
        {
            SetEnemyUnit(GetUnit(_enemyFraction));
        }
        private void SetEnemyUnit(IUnit unit)
        {
            _gameStats.EnemyUnit = unit;
        }
        public IUnit GetUnit(Fraction enemyFraction = Fraction.None)
        {
            return UnitIsFromFraction(enemyFraction)
                ? _unit
                : null;
        }
        public bool UnitIsFromFraction(Fraction enemyFraction = Fraction.None)
        {
            //Debug.Log("player Fraction: "+ _playerFraction);
            return enemyFraction == Fraction.None
                ? _unit.Fraction == _playerFraction
                : _unit.Fraction == enemyFraction;
        }
    }
}