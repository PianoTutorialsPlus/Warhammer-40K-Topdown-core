using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Deal Damage Event")]
public class DealDamageSO : CalculationBaseSO
{
    public override void Action(List<int> notSaved, GameStatsSO gameStats)
    {
        int damage = gameStats.activeUnit._weaponSO.Damage;

        foreach (int save in notSaved)
        {
            gameStats.enemyUnit._unitSO.Wounds = damage;
            Debug.Log("Wounds left: " + gameStats.enemyUnit._unitSO.Wounds);
            Debug.Log("Wounds taken: " + gameStats.enemyUnit._unitSO.takenWounds);
        }
        if (gameStats.enemyUnit._unitSO.Wounds <= 0)
            gameStats.enemyUnit.Destroy();

        Result(ShootingSubEvents.Damage, null);
    }


    public override void Result(ShootingSubEvents diceEvent, List<int> result)
    {
        rollDiceResult.RaiseEvent(diceEvent, result);
    }
}

