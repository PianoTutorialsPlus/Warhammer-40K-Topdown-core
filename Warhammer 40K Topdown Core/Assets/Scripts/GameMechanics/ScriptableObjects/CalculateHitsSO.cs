using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Game/Hit Calculation Event")]
public class CalculateHitsSO : ICalculation
{

    private readonly RollTheDiceSO rollSubResult;
    private readonly RollTheDiceSO rollDices;
    private readonly RollTheDiceSO rollDiceResult;

    //public List<int> hitResult = new List<int>();
    int toHit;

    public CalculateHitsSO(List<RollTheDiceSO> rollTheDice)
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

    public void Action(GameStatsSO gameStats)
    {
        List<int> shots = new List<int>();

        toHit = gameStats.activeUnit._unitSO.BallisticSkill;

        for (int shot = 0; shot < gameStats.activeUnit._weaponSO.Shots; shot++)
        {
            shots.Add(shot);
        }
        rollDices.RaiseEvent(ShootingSubEvents.Hit, shots);
    }

    public void Result(ShootingSubEvents diceEvent, List<int> hitResult)
    {
        if (hitResult == null || hitResult.Count == 0) return;
        if (diceEvent != ShootingSubEvents.Hit) return;
        Debug.Log("CalculateHitsSO Result");

        List<int> hits = ShootingSubPhaseProcessor.GetResult(toHit, hitResult, diceEvent);
        rollDiceResult.RaiseEvent(diceEvent, hits);

    }

    public void Action(List<int> action, GameStatsSO gameStats)
    {
        //throw new System.NotImplementedException();
    }

    //public override void Result(ShootingSubEvents diceEvent, List<int> hitResult)
    //{
    //    List<int> hits = new List<int>();
    //    if (diceEvent == ShootingSubEvents.Hit)
    //    {
    //        if (hitResult != null)
    //        {

    //            foreach (int result in hitResult)
    //            {
    //                Debug.Log("Hit result: " + result);
    //                if (result >= toHit)
    //                    hits.Add(result);
    //            }
    //            rollDiceResult.RaiseEvent(ShootingSubEvents.Hit, hits);

    //        }
    //    }
    //}
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
