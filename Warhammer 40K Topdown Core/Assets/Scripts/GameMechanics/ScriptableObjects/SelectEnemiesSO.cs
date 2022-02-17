using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Selection Event")]
public class SelectEnemiesSO : CalculationBaseSO
{
    public GameStatsSO _gameStats;

    public override void Action(GameStatsSO gameStats)
    {
        List<int> item = new List<int>();
        item.Add(1);
        _gameStats.enemyUnit = gameStats.enemyPlayer._playerUnits[0];
        Result(ShootingSubEvents.SelectEnemy, item);
    }

    public override void Result(ShootingSubEvents diceEvent, List<int> result)
    {
        rollDiceResult.RaiseEvent(diceEvent, result);
    }
}
