using UnityEngine;
using WH40K.Stats;

namespace WH40K.Gameplay.GamePhaseEvents
{
    public abstract class PhaseManagerBase : MonoBehaviour
    {
        public abstract GamePhase SubEvents { get; } // gets the active game phase
        public abstract void ClearPhase();
    }
}