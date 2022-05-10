using System.Collections.Generic;
using WH40K.GameMechanics;

public interface IInteractionManager
{
    Dictionary<GamePhase, PhaseManagerBase> GamePhaseManagers { get; }
}