using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GamePhases
{
    public abstract GamePhase SubEvents { get; }
    public abstract GamePhase SetPhase(GameStatsSO gameStats,GamePhase subPhase);
    public abstract bool HandlePhase(List<PhaseManagerBase> gamePhases);
    public abstract void ResetPhase(GameStatsSO gameStats, List<PhaseManagerBase> gamePhases);
    public abstract void ResetUnits(GameStatsSO gameStats);
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
        Debug.Log("Reset Units: " + gameStats.activePlayer.name);
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
        Debug.Log("Reset Units: " + gameStats.activePlayer.name);
        foreach (Unit child in gameStats.enemyPlayer._playerUnits)
        {
            child.ResetData();
            child.PrepareMovementPhase();
        }
    }
}
