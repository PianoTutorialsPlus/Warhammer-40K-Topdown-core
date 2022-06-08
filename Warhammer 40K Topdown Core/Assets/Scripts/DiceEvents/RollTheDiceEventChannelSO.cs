using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WH40K.DiceEvents
{
    [CreateAssetMenu(menuName = "Game/Dice rolling Event")]
    public class RollTheDiceEventChannelSO : ScriptableObject
    {
        public UnityAction<List<int>> OnEventRaised;

        public void RaiseEvent(List<int> values)
        {
            Debug.Log("Roll The Dice SO");
            if (OnEventRaised != null)
                OnEventRaised.Invoke(values);
        }
    }
}