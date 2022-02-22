using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Deal Damage Event")]
public class DealDamageSO : ICalculation
{
    private readonly RollTheDiceSO rollDiceResult;

    public DealDamageSO(List<RollTheDiceSO> diceRoll)
    {
        this.rollDiceResult = diceRoll[2];
    }

    public void Action(List<int> notSaved, GameStatsSO gameStats)
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

    public void Action(GameStatsSO gameStats)
    {
        //throw new System.NotImplementedException();
    }

    public void Result(ShootingSubEvents diceEvent, List<int> result)
    {
        rollDiceResult.RaiseEvent(diceEvent, result);
    }
}

