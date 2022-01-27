using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Deal Damage Event")]
public class DealDamageSO : ScriptableObject
{
    public void DealDamage(List<int> notSaved, GameStatsSO gameStats)
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
    }
}

