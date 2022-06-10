using System.Collections.Generic;
using WH40K.Stats;

namespace WH40K.Gameplay.GamePhaseEvents
{
    public interface IInteractionManager
    {
        Dictionary<GamePhase, PhaseManagerBase> GamePhaseManagers { get; }
    }
}