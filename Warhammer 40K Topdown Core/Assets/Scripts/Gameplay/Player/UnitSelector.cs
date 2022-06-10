using UnityEngine;
using WH40K.Stats;
using WH40K.Stats.Player;

namespace WH40K.Gameplay.PlayerEvents
{
    public class UnitSelector
    {
        private IUnit _unit;
        private GameStatsSO _gameStats;

        private Fraction _playerFraction => _gameStats.ActivePlayer.Fraction;
        private Fraction _enemyFraction => _gameStats.EnemyPlayer.Fraction;

        public UnitSelector(
            IUnit unit, 
            GameStatsSO gameStats)
        {
            _unit = unit;
            _gameStats = gameStats;
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
            Debug.Log("player Fraction: " + _playerFraction);
            Debug.Log("Unit Fraction: " + _unit.Fraction);
            return enemyFraction == Fraction.None
                ? _unit.Fraction == _playerFraction
                : _unit.Fraction == enemyFraction;
        }
    }
}