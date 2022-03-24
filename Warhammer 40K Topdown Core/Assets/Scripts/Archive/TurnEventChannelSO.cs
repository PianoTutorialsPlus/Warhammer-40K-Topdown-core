using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events to switch the gameturns.
/// Example: Switch from turn 1 to turn 2
/// </summary>

//[CreateAssetMenu(menuName ="Events/Switch gameturn")]
public class TurnEventChannelSO : ScriptableObject
{
    public UnityAction<TurnSO> OnEventRaised;

    public void RaiseEvent(TurnSO turn)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(turn);
    }
}

