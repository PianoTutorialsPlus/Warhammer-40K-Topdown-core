using UnityEngine;

namespace WH40K.GameMechanics
{
    public abstract class PhaseManagerBase : MonoBehaviour
    {
        public abstract GamePhase SubEvents { get; } // gets the active game phase
        public abstract void ClearPhase();
    }
}