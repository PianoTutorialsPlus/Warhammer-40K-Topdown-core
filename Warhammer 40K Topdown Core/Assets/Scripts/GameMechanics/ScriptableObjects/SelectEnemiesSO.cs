using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Game/Enemy Selection Event")]
public class SelectEnemiesSO : ICalculation
{
    //public GameStatsSO _gameStats;
    private readonly RollTheDiceSO rollDiceResult;

    public SelectEnemiesSO(List<RollTheDiceSO> diceRoll)
    {
        this.rollDiceResult = diceRoll[2];
    }

    public void Action(GameStatsSO gameStats)
    {
        List<int> item = new List<int>();
        item.Add(1);
        gameStats.enemyUnit = gameStats.enemyPlayer._playerUnits[0];
        Result(ShootingSubEvents.SelectEnemy, item);
    }

    public void Action(List<int> action, GameStatsSO gameStats)
    {
        //throw new System.NotImplementedException();
    }

    public void Result(ShootingSubEvents diceEvent, List<int> result)
    {
        rollDiceResult.RaiseEvent(diceEvent, result);
    }
}
