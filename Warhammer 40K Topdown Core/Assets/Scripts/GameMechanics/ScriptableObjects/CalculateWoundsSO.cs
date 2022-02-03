using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Wound Calculation Event")]
public class CalculateWoundsSO : ScriptableObject
{
    public RollTheDiceSO rollDices;
    public RollTheDiceSO rollWoundsResult;
    public RollTheDiceSO rollDiceResult;
    public DataTablesSO dataTable;

    int toWound;

    private void OnEnable()
    {
        if (rollWoundsResult != null) rollWoundsResult.OnEventRaised += Result;
    }

    public void HandleToWound(List<int> hits, GameStatsSO gameStats)
    {
        //List<int> wounds = new List<int>();

        int toWound = dataTable.WoundTable(gameStats.activeUnit._weaponSO.Strength, gameStats.enemyUnit._unitSO.Toughness);
        Debug.Log("CalculateWoundsSO");
        rollDices.RaiseEvent(DiceEvent.ShootEvent,hits);     
    }
    private void Result(DiceEvent diceEvent, List<int> woundResult)
    {
        List<int> wounds = new List<int>();
        Debug.Log("CalculateWoundsSO Result");
        if (diceEvent == DiceEvent.ShootEvent)
        {
            if (woundResult != null)
            {

                foreach (int result in woundResult)
                {
                    Debug.Log("Wounds: " + result);
                    if (result >= toWound)
                        wounds.Add(result);
                }
                rollDiceResult.RaiseEvent(DiceEvent.ShootEvent, wounds);
            }
        }
    }

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
