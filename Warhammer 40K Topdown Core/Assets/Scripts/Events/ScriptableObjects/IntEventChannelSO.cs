using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have integer arguments (Example: )
/// </summary>

[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannelSO : EventChannelBaseSO
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
