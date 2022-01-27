using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Saverole Calculation Event")]
public class CalculateSaverolesSO : ScriptableObject
{
    public RollTheDiceSO rollDices;

    public List<int> HandleSaveRoles(List<int> wounds, GameStatsSO gameStats)
    {
        List<int> saveResult = new List<int>();
        List<int> notSaved = new List<int>();

        int saves = gameStats.enemyUnit._unitSO.ArmourSave;
        int modifier = gameStats.activeUnit._weaponSO.ArmourPen;

        saveResult = rollDices.RollTheDice(wounds);

        foreach (int result in saveResult)
        {
            Debug.Log("saveResult: " + result);
            if (result < (saves - modifier))
                notSaved.Add(result);
        }
        return notSaved;
    }
}
