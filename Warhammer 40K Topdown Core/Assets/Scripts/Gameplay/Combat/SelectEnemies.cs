using System.Collections.Generic;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats;
using Zenject;

namespace WH40K.Gameplay.Combat
{
    public class SelectEnemies : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;

        public SelectEnemies(IResult results, GameStatsSO gameStats) : base(results, gameStats) { }
        
        public SelectEnemies()
        {
        }

        public override void Action(List<int> action)
        {
            OnEnable();
            List<int> item = new List<int>() { 1 };
            //Debug.Log(_gameStats);
            var enemyUnit = _gameStats.EnemyPlayer.PlayerUnits[0].GetComponent<UnitFacade>();
            _gameStats.EnemyUnit = enemyUnit;
            Result(item);
        }
        public override void Result(List<int> result)
        {
            _diceResult.RaiseEvent(result);
        }
    }
}