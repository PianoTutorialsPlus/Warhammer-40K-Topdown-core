using System.Collections.Generic;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.Gameplay.Core;
using WH40K.Gameplay.GamePhaseEvents;

namespace WH40K.Gameplay.Combat
{
    public class SelectEnemies : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;

        public SelectEnemies(IResult results) : base(results) { }

        public override void Action(List<int> action)
        {
            OnEnable();
            List<int> item = new List<int>() { 1 };
            GameStats.EnemyUnit = GameStats.EnemyPlayer.PlayerUnits[0];
            Result(item);
        }
        public override void Result(List<int> result)
        {
            Debug.Log("SelectEnemies Result");
            _diceResult.RaiseEvent(result);
        }
    }
}