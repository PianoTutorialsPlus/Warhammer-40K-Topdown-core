using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events to toggle the info UI.
/// Example: Dispaly or hide the interaction UI via a bool and the interaction type from the enum via int
/// </summary>

[CreateAssetMenu(menuName ="Events/Toggle Info UI Events Channel")]
public class InfoUIEventChannelSO : ScriptableObject
{
    public UnityAction<bool, Unit> OnEventRaised;
    public void RaiseEvent(bool state, Unit unit)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(state, unit);
    }
}
