using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.GameMechanics.Combat
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
            DiceResult.RaiseEvent(result);
        }
    }
}