using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Saverole Calculation Event")]
public class CalculateSaverolesSO : ScriptableObject
{
    public RollTheDiceSO rollDices;
    public RollTheDiceSO rollSaveResult;
    public RollTheDiceSO rollDiceResult;

    //public List<int> hitResult = new List<int>();
    int saves;
    int modifier;

    private void OnEnable()
    {
        if (rollSaveResult != null) rollSaveResult.OnEventRaised += Result;
    }

    public void HandleSaveRoles(List<int> wounds, GameStatsSO gameStats)
    { 

        saves = gameStats.enemyUnit._unitSO.ArmourSave;
        modifier = gameStats.activeUnit._weaponSO.ArmourPen;

        rollDices.OnEventRaised(DiceEvent.SaveEvent ,wounds);
    }

    private void Result(DiceEvent diceEvent, List<int> SaveResult)
    {
        List<int> notSaved = new List<int>();
        if (diceEvent == DiceEvent.SaveEvent)
        {
            if (SaveResult != null)
            {

                foreach (int result in SaveResult)
                {
                    Debug.Log("saveResult: " + result);
                    if (result < (saves - modifier))
                        notSaved.Add(result);
                }
                rollDiceResult.RaiseEvent(DiceEvent.SaveEvent, notSaved);
            }
        }
    }

    //public List<int> HandleSaveRoles(List<int> wounds, GameStatsSO gameStats)
    //{
    //    List<int> saveResult = new List<int>();
    //    List<int> notSaved = new List<int>();

    //    int saves = gameStats.enemyUnit._unitSO.ArmourSave;
    //    int modifier = gameStats.activeUnit._weaponSO.ArmourPen;

    //    saveResult = rollDices.RollTheDice(wounds);

    //    foreach (int result in saveResult)
    //    {
    //        Debug.Log("saveResult: " + result);
    //        if (result < (saves - modifier))
    //            notSaved.Add(result);
    //    }
    //    return notSaved;
    //}
}
