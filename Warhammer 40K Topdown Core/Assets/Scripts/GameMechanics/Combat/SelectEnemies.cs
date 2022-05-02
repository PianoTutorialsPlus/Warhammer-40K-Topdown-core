using System.Collections.Generic;
using WH40K.Essentials;

namespace WH40K.GameMechanics.Combat
{
    public class SelectEnemies : ICalculation
    {
        private readonly IResult _results;
        private GameStatsSO _gameStats => _results.GameStats;
        private RollTheDiceSO DiceResult => _results.DiceResult;

        public SelectEnemies(IResult results)
        {
            _results = results;
        }

        public void Action(List<int> action)
        {
            List<int> item = new List<int>();
            item.Add(1);
            _gameStats.EnemyUnit = _gameStats.EnemyPlayer.PlayerUnits[0];
            Result(ShootingSubEvents.SelectEnemy, item);
        }


        public void Result(ShootingSubEvents diceEvent, List<int> result)
        {
            DiceResult.RaiseEvent(diceEvent, result);
        }

    }
}