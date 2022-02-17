using System.Collections.Generic;

/// <summary>
/// This script executes the calls from the interaction manager in the specific state.
/// When HandlePhase is called, it enables to the specific game phase manager responsible for that case.
/// All other game phase managers will be disabled.
/// </summary>

public abstract class GamePhases
{
    public abstract GamePhase SubEvents { get; } // gets the active game phase
    public abstract GamePhase SetPhase(GameStatsSO gameStats, GamePhase subPhase); // sets the next game phase
    public abstract bool HandlePhase(List<PhaseManagerBase> gamePhases); // Refers to the specific game phase manager
    public abstract void ResetPhase(GameStatsSO gameStats, List<PhaseManagerBase> gamePhases); // clears all dependencies of the game phase
    public abstract void ResetUnits(GameStatsSO gameStats); // clears all dependencies of the game phase
}

public class MovementPhaseBase : GamePhases
{
    public MovementPhaseBase() { }
    public override GamePhase SubEvents => GamePhase.MovementPhase;

    public override GamePhase SetPhase(GameStatsSO gameStats, GamePhase subPhase)
    {
        gameStats.phase = GamePhase.ShootingPhase;
        return GamePhase.ShootingPhase;
    }

    public override bool HandlePhase(List<PhaseManagerBase> gamePhases)
    {
        foreach (PhaseManagerBase phase in gamePhases)
        {
            if (phase.GetComponent<MovementPhaseManager>() ? phase.enabled = true : phase.enabled = false) { }
        }
        return true;
    }

    public override void ResetPhase(GameStatsSO gameStats, List<PhaseManagerBase> gamePhases)
    {
        MovementPhaseManager movementPhase = (MovementPhaseManager)gamePhases[0];
        movementPhase.ClearMovementPhase(gameStats);
        gameStats.activeUnit = null;
    }

    public override void ResetUnits(GameStatsSO gameStats)
    {
        foreach (Unit child in gameStats.activePlayer._playerUnits)
        {
            child.ResetData();
            child.PrepareShootingPhase();
        }
    }
}

public class ShootingPhaseBase : GamePhases
{
    public ShootingPhaseBase() { }
    public override GamePhase SubEvents => GamePhase.ShootingPhase;

    public override GamePhase SetPhase(GameStatsSO gameStats, GamePhase subPhase)
    {
        gameStats.phase = GamePhase.MovementPhase;
        return GamePhase.MovementPhase;
    }

    public override bool HandlePhase(List<PhaseManagerBase> gamePhases)
    {
        foreach (PhaseManagerBase phase in gamePhases)
        {
            if (phase.GetComponent<ShootingPhaseManager>() ? phase.enabled = true : phase.enabled = false) { }
        }
        return false;
    }

    public override void ResetPhase(GameStatsSO gameStats, List<PhaseManagerBase> gamePhases)
    {
        ShootingPhaseManager shootingPhase = (ShootingPhaseManager)gamePhases[1];
        shootingPhase.ClearShootingPhase(gameStats);
        gameStats.activeUnit = null;
        gameStats.enemyUnit = null;
    }

    public override void ResetUnits(GameStatsSO gameStats)
    {
        foreach (Unit child in gameStats.enemyPlayer._playerUnits)
        {
            child.ResetData();
            child.PrepareMovementPhase();
        }
    }
}
