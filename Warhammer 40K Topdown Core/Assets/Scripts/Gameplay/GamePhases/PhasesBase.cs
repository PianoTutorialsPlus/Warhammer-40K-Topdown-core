using System;

namespace WH40K.Gameplay.GamePhaseEvents
{
    public abstract class PhasesBase
    {
        public abstract Enum SubEvents { get; } // gets the active subphase
    }
}