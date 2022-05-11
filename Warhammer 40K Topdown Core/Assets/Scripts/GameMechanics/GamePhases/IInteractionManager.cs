using System.Collections.Generic;

namespace WH40K.GamePhaseEvents
{
    public interface IInteractionManager
    {
        Dictionary<GamePhase, PhaseManagerBase> GamePhaseManagers { get; }
    }
}