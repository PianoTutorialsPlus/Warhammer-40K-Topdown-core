//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CalculateHits : MonoBehaviour
//{
//    public RollTheDiceSO rollDices;
//    public RollTheDiceSO rollHitsResult;
//    public RollTheDiceSO rollDiceResult;

//    public List<int> hitResult = new List<int>();
//    int toHit;

//    private void OnEnable()
//    {
//        if (rollHitsResult != null) rollHitsResult.OnEventRaised += Result;
//    }

//    public void HandleToHit(GameStatsSO gameStats)
//    {
//        List<int> shots = new List<int>();

//        toHit = gameStats.activeUnit._unitSO.BallisticSkill;

//        for (int shot = 0; shot < gameStats.activeUnit._weaponSO.Shots; shot++)
//        {
//            shots.Add(shot);
//        }  
//        rollDices.RaiseEvent(DiceEvent.HitEvent, shots);
//    }

//    private void Result(DiceEvent diceEvent, List<int> hitResult)
//    {
//        List<int> hits = new List<int>();
//        //if (diceEvent == DiceEvent.HitEvent) hitResult = hitResult;
//        if (hitResult != null) {

//            foreach (int result in hitResult)
//            {
//                Debug.Log("Hit result: " + result);
//                if (result >= toHit)
//                    hits.Add(result);
//            }
//            rollDiceResult.RaiseEvent(DiceEvent.HitEvent, hits);
//        }
//    }
//}
