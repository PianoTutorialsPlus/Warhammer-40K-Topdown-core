using UnityEngine;
using WH40K.Gameplay.Core;
using WH40K.Stats.Player;

namespace WH40K.Gameplay.PlayerEvents
{
    public class UnitSelector
    {
        private IUnit _unit;

        private Fraction _playerFraction => GameStats.ActivePlayer.Fraction;
        private Fraction _enemyFraction => GameStats.EnemyPlayer.Fraction;

        public UnitSelector(IUnit unit)
        {
            _unit = unit;
        }
        public void SelectUnit()
        {
            SetActiveUnit(GetUnit());
        }

        private void SetActiveUnit(IUnit unit)
        {
            GameStats.ActiveUnit = unit;
        }
        public void SelectEnemyUnit()
        {
            SetEnemyUnit(GetUnit(_enemyFraction));
        }
        private void SetEnemyUnit(IUnit unit)
        {
            GameStats.EnemyUnit = unit;
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