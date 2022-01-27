using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/Dice rolling Event")]
public class RollTheDiceSO : ScriptableObject
{
    public InputReader inputReader;

    public List<int> RollTheDice(List<int> values)
    {

        List<int> result = new List<int>();

        foreach (int value in values)
        {
            result.Add(Random.Range(1, 7));
        }

        return result;
    }
}
