using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events to set the distance Indicator UI.
/// Example: Dispaly or hide the distance Indicator via a bool.
/// </summary>

[CreateAssetMenu(menuName = "Events/Toggle Distance Indicator Connection UI Events Channel")]
public class IndicatorUIEventChannelSO : ScriptableObject
{
    public UnityAction<bool, Unit> OnEventRaised;
    public void RaiseEvent(bool state, Unit unit)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(state, unit);
    }
}
