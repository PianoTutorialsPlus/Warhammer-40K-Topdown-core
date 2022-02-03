using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Hit Calculation Event")]
public class CalculateHitsSO : ScriptableObject
{
    public RollTheDiceSO rollDices;
    public RollTheDiceSO rollHitsResult;
    public RollTheDiceSO rollDiceResult;

    //public List<int> hitResult = new List<int>();
    int toHit;

    private void OnEnable()
    {
        if (rollHitsResult != null) rollHitsResult.OnEventRaised += Result;
    }

    public void HandleToHit(GameStatsSO gameStats)
    {
        List<int> shots = new List<int>();

        toHit = gameStats.activeUnit._unitSO.BallisticSkill;

        for (int shot = 0; shot < gameStats.activeUnit._weaponSO.Shots; shot++)
        {
            shots.Add(shot);
        }
        rollDices.RaiseEvent(DiceEvent.HitEvent, shots);
    }

    private void Result(DiceEvent diceEvent, List<int> hitResult)
    {
        List<int> hits = new List<int>();
        if (diceEvent == DiceEvent.HitEvent)
        {
            if (hitResult != null)
            {

                foreach (int result in hitResult)
                {
                    Debug.Log("Hit result: " + result);
                    if (result >= toHit)
                        hits.Add(result);
                }
                rollDiceResult.RaiseEvent(DiceEvent.HitEvent, hits);
            }
        }
    }


    //public RollTheDiceSO rollDices;
    //public RollTheDiceSO rollDicesResult;

    //List<int> hitResult = new List<int>();

    //private void OnEnable()
    //{
    //    //if (rollDicesResult != null) rollDicesResult.OnEventRaised += Result;
    //}

    //public List<int> HandleToHit(GameStatsSO gameStats)
    //{
    //    List<int> shots = new List<int>();
    //    List<int> hits = new List<int>();


    //    int toHit = gameStats.activeUnit._unitSO.BallisticSkill;

    //    for (int shot = 0; shot < gameStats.activeUnit._weaponSO.Shots; shot++)
    //    {
    //        shots.Add(shot);
    //        //Debug.Log(shot);
    //    }

    //    rollDices.RaiseEvent(DiceEvent.HitEvent, shots);
    //    //hitResult = rollDices.RollTheDice(shots);

    //    if (hitResult != null)
    //    {
    //        foreach (int result in hitResult)
    //        {
    //            //Debug.Log("Hit result: " + result);
    //            if (result >= toHit)
    //                hits.Add(result);
    //        }
    //        return hits;
    //    }
    //    return null;
    //}

    //private void Result(DiceEvent diceEvent,List<int> result)
    //{
    //    if (diceEvent == DiceEvent.HitEvent) hitResult = result;
    //}
}
