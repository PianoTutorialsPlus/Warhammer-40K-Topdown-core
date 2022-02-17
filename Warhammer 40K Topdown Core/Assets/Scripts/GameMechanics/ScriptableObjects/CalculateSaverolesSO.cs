using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Saverole Calculation Event")]
public class CalculateSaverolesSO : CalculationBaseSO
{
    //public List<int> hitResult = new List<int>();
    int saves;
    int modifier;

    private void OnEnable()
    {
        if (rollSubResult != null) rollSubResult.OnEventRaised += Result;
    }

    public override void Action(List<int> wounds, GameStatsSO gameStats)
    {

        saves = gameStats.enemyUnit._unitSO.ArmourSave;
        modifier = gameStats.activeUnit._weaponSO.ArmourPen;

        rollDices.OnEventRaised(ShootingSubEvents.Save, wounds);
    }

    public override void Result(ShootingSubEvents diceEvent, List<int> saveResult)
    {
        List<int> notSaved = new List<int>();

        if (saveResult == null || saveResult.Count == 0) return;
        if (diceEvent != ShootingSubEvents.Save) return;
        Debug.Log("CalculateSavesSO Result");

        notSaved = ShootingSubPhaseProcessor.GetResult(saves - modifier, saveResult, diceEvent);
        rollDiceResult.RaiseEvent(ShootingSubEvents.Save, notSaved);
    }
    //public override void Result(ShootingSubEvents diceEvent, List<int> SaveResult)
    //{
    //    List<int> notSaved = new List<int>();
    //    if (diceEvent == ShootingSubEvents.Save)
    //    {
    //        if (SaveResult != null)
    //        {

    //            foreach (int result in SaveResult)
    //            {
    //                Debug.Log("saveResult: " + result);
    //                if (result < (saves - modifier))
    //                    notSaved.Add(result);
    //            }
    //            rollDiceResult.RaiseEvent(ShootingSubEvents.Save, notSaved);
    //        }
    //    }
    //}
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
