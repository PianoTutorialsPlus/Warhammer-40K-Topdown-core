using UnityEngine;

namespace WH40K.Essentials
{
    public class UnitSelector
    {
        private GameStatsSO _gameStats;
        private IUnit _unit;

        public bool IsSelected => UnitIsFromFraction();
        private Fraction _playerFraction => _gameStats.ActivePlayer.Fraction;

        //private IUnit GetActiveUnit()
        //{
        //    return _gameStats.ActiveUnit;
        //}

        private void SetActiveUnit(IUnit value)
        {
            _gameStats.ActiveUnit = value;
        }

        public UnitSelector(GameStatsSO gameStats, IUnit unit)
        {
            //_playerFraction = gameStats
            _gameStats = gameStats;
            _unit = unit;
        }
        public void SelectUnit()
        {
            SetActiveUnit(GetUnit());
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