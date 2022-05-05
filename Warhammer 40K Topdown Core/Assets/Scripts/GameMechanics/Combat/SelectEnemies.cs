using System.Collections.Generic;

namespace WH40K.GameMechanics.Combat
{
    public class SelectEnemies : CombatPhases, ICalculation
    {
        public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;

        public SelectEnemies(IResult results) : base(results) { }
        //{
        //    _results = results;
        //}

        public override void Action(List<int> action)
        {
            List<int> item = new List<int>() { 1 };
            //_gameStats.EnemyUnit = _gameStats.EnemyPlayer.PlayerUnits[0];
            Result(ShootingSubEvents.SelectEnemy, item);
        }
        public override void Result(ShootingSubEvents diceEvent, List<int> result)
        {
            if (diceEvent != ShootingSubEvents.SelectEnemy) return;

            DiceResult.RaiseEvent(diceEvent, result);
        }
    }
}