using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Game/Dice rolling Event")]
public class RollTheDiceSO : ScriptableObject
{
    public InputReader inputReader;
    public UnityAction<DiceEvent, List<int>> OnEventRaised;

    //public List<int> RollTheDice(List<int> values)
    //{

    //    List<int> result = new List<int>();

    //    foreach (int value in values)
    //    {
    //        result.Add(Random.Range(1, 7));
    //    }

    //    return result;
    //}

    public void RaiseEvent(DiceEvent diceEvent, List<int> values)
    {
        Debug.Log("Roll The Dice SO");
        if (OnEventRaised != null)
            OnEventRaised.Invoke(diceEvent,values);
    }
}
