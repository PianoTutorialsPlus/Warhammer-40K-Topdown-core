using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Hit Calculation Event")]
public class CalculateHitsSO : ScriptableObject
{
    public RollTheDiceSO rollDices;

    public List<int> HandleToHit(GameStatsSO gameStats)
    {
        List<int> shots = new List<int>();
        List<int> hits = new List<int>();
        List<int> hitResult = new List<int>();

        int toHit = gameStats.activeUnit._unitSO.BallisticSkill;

        for (int shot = 0; shot < gameStats.activeUnit._weaponSO.Shots; shot++)
        {
            shots.Add(shot);
            //Debug.Log(shot);
        }

        hitResult = rollDices.RollTheDice(shots);

        foreach (int result in hitResult)
        {
            Debug.Log("Hit result: " + result);
            if (result >= toHit)
                hits.Add(result);
        }
        return hits;
    }
}
