using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Wound Calculation Event")]
public class CalculateWoundsSO : ScriptableObject
{
    public RollTheDiceSO rollDices;
    public DataTablesSO dataTable;

    public List<int> HandleToWound(List<int> hits, GameStatsSO gameStats)
    {
        List<int> wounds = new List<int>();
        List<int> woundResult = new List<int>();

        int toWound = dataTable.WoundTable(gameStats.activeUnit._weaponSO.Strength, gameStats.enemyUnit._unitSO.Toughness);

        woundResult = rollDices.RollTheDice(hits);

        foreach (int result in woundResult)
        {
            Debug.Log("Wounds: " + result);
            if (result >= toWound)
                wounds.Add(result);
        }
        return wounds;
    }
}
