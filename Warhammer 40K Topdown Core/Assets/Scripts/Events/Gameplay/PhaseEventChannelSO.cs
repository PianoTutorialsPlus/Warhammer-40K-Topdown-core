using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events to switch the gamephases.
/// Example: Switch from movement phase to shooting phase
/// </summary>

[CreateAssetMenu(menuName ="Events/Switch gamephase")]
public class PhaseEventChannelSO : ScriptableObject
{
    public UnityAction<PhaseSO> OnEventRaised;

    public void RaiseEvent(PhaseSO phase)
    {
        if (OnEventRaised != null)
        OnEventRaised.Invoke(phase);
    }
}

