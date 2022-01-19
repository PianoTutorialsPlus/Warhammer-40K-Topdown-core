using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events to toggle the gameinfo UI.
/// Example: Dispaly or hide the gameinfo UI via a bool.
/// </summary>

[CreateAssetMenu(menuName ="Events/Toggle Gameinfo UI Events Channel")]
public class GameinfoUIEventChannelSO : ScriptableObject
{
    public UnityAction<bool, Unit,PhaseSO,TurnSO> OnEventRaised;
    public void RaiseEvent(bool state, Unit unit, PhaseSO phase, TurnSO turn)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(state, unit,phase,turn);
    }

    internal void RaiseEvent(bool v, Unit unit, PhaseSO phase)
    {
        throw new NotImplementedException();
    }

    internal void RaiseEvent(bool v, Unit unit, TurnSO turn)
    {
        throw new NotImplementedException();
    }
}
