using System.Collections.Generic;

namespace WH40K.Combat
{
    public class SelectEnemies : ICalculation
    {
        private GameStatsSO _gameStats => _results.GameStats;
        private readonly IResult _results;
        private RollTheDiceSO DiceResult => _results.DiceResult;

        public SelectEnemies(IResult results)
        {
            _results = results;
        }

        public void Action(List<int> action)
        {
            List<int> item = new List<int>();
            item.Add(1);
            _gameStats.enemyUnit = _gameStats.enemyPlayer._playerUnits[0];
            Result(ShootingSubEvents.SelectEnemy, item);
        }


        public void Result(ShootingSubEvents diceEvent, List<int> result)
        {
            DiceResult.RaiseEvent(diceEvent, result);
        }

    }
}