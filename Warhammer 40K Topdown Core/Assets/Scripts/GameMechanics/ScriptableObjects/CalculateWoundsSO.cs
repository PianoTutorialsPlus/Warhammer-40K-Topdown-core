using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Game/Wound Calculation Event")]
public class CalculateWoundsSO : ICalculation
{
    private readonly RollTheDiceSO rollSubResult;
    private readonly RollTheDiceSO rollDices;
    private readonly RollTheDiceSO rollDiceResult;

    int toWound;

    public CalculateWoundsSO(List<RollTheDiceSO> rollTheDice)
    {
        this.rollDices = rollTheDice[0];
        this.rollSubResult = rollTheDice[1];
        this.rollDiceResult = rollTheDice[2];
        OnEnable();
    }

    private void OnEnable()
    {
        if (rollSubResult != null) rollSubResult.OnEventRaised += Result;
    }

    public void Action(List<int> hits, GameStatsSO gameStats)
    {
        //List<int> wounds = new List<int>();
        if (hits == null || hits.Count == 0) return;
        DataTablesSO dataTable = gameStats.dataTable;
        toWound = dataTable.WoundTable(gameStats.activeUnit._weaponSO.Strength, gameStats.enemyUnit._unitSO.Toughness);
        Debug.Log("to Wound: " + toWound);
        Debug.Log("CalculateWoundsSO");
        rollDices.RaiseEvent(ShootingSubEvents.Wound, hits);

    }
    public void Result(ShootingSubEvents diceEvent, List<int> woundResult)
    {

        if (woundResult == null || woundResult.Count == 0) return;
        if (diceEvent != ShootingSubEvents.Wound) return;
        List<int> wounds = new List<int>();
        Debug.Log("CalculateWoundsSO Result");

        wounds = ShootingSubPhaseProcessor.GetResult(toWound, woundResult, diceEvent);
        rollDiceResult.RaiseEvent(diceEvent, wounds);
    }

    public void Action(GameStatsSO gameStats)
    {
        //throw new System.NotImplementedException();
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
