using UnityEngine;

public abstract class PhaseManagerBase : MonoBehaviour
{
    public abstract GamePhase SubEvents { get; } // gets the active game phase
    public abstract void ClearPhase();
}