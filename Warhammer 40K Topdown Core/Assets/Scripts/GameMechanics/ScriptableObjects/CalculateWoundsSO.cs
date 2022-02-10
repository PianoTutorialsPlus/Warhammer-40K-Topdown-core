using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Wound Calculation Event")]
public class CalculateWoundsSO : CalculationBaseSO
{
    public DataTablesSO dataTable;

    int toWound;

    private void OnEnable()
    {
        if (rollSubResult != null) rollSubResult.OnEventRaised += Result;
    }

    public override void Action(List<int> hits, GameStatsSO gameStats)
    {
        //List<int> wounds = new List<int>();
        if (hits == null || hits.Count == 0) return;
        toWound = dataTable.WoundTable(gameStats.activeUnit._weaponSO.Strength, gameStats.enemyUnit._unitSO.Toughness);
        Debug.Log("to Wound: " + toWound);
        Debug.Log("CalculateWoundsSO");
        rollDices.RaiseEvent(ShootingSubEvents.Wound,hits);  
        
    }
    public override void Result(ShootingSubEvents diceEvent, List<int> woundResult)
    {
        
        if (woundResult == null || woundResult.Count == 0) return;
        if (diceEvent != ShootingSubEvents.Wound) return;
        List<int> wounds = new List<int>();
        Debug.Log("CalculateWoundsSO Result");

        wounds = ShootingSubPhaseProcessor.GetResult(toWound, woundResult, diceEvent);
        rollDiceResult.RaiseEvent(diceEvent, wounds);
    }
    //public override void Result(ShootingSubEvents diceEvent, List<int> woundResult)
    //{
    //    List<int> wounds = new List<int>();

    //    Debug.Log("CalculateWoundsSO Result");

    //    if (woundResult == null) return;

    //    if (diceEvent == ShootingSubEvents.Wound)
    //    {
    //        if (woundResult != null)
    //        {

    //            foreach (int result in woundResult)
    //            {
    //                Debug.Log("Wounds: " + result);
    //                if (result >= toWound)
    //                {
    //                    Debug.Log("toWounds: " + toWound);
    //                    wounds.Add(result);
    //                }
    //            }
    //            rollDiceResult.RaiseEvent(ShootingSubEvents.Wound, wounds);

    //        }
    //    }
    //}
    //public List<int> HandleToWound(List<int> hits, GameStatsSO gameStats)
    //{
    //    List<int> wounds = new List<int>();
    //    List<int> woundResult = new List<int>();

    //    int toWound = dataTable.WoundTable(gameStats.activeUnit._weaponSO.Strength, gameStats.enemyUnit._unitSO.Toughness);

    //    woundResult = rollDices.RollTheDice(hits);

    //    foreach (int result in woundResult)
    //    {
    //        Debug.Log("Wounds: " + result);
    //        if (result >= toWound)
    //            wounds.Add(result);
    //    }
    //    return wounds;
    //}
}
