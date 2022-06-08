using System.Collections.Generic;

namespace WH40K.Gameplay.GamePhaseEvents
{
    public interface IInteractionManager
    {
        Dictionary<GamePhase, PhaseManagerBase> GamePhaseManagers { get; }
    }
}