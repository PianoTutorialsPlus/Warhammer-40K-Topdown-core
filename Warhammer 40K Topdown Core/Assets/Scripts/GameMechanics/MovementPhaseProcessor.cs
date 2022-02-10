using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class MovementPhaseProcessor
{
    private static Dictionary<MovementPhase, MovementPhases> _movementPhases = new Dictionary<MovementPhase, MovementPhases>();

    public static bool _initialized;

    private static void Initialize()
    {
        _movementPhases.Clear();

        var allShootingPhases = Assembly.GetAssembly(typeof(MovementPhases)).GetTypes()
            .Where(t => typeof(MovementPhases).IsAssignableFrom(t) && t.IsAbstract == false);

        foreach (var subphase in allShootingPhases)
        {
            Debug.Log("Dictionary: " + subphase.Name);
            MovementPhases movementPhases = Activator.CreateInstance(subphase) as MovementPhases;
            _movementPhases.Add(movementPhases.SubEvents, movementPhases);
        }

        _initialized = true;
    }

    public static bool HandleMovement(GameStatsSO gameStats,BattleRoundsSO _battleroundEvents, MovementPhase subPhase)
    {
        if (!_initialized) Initialize();

        var movementPhase = _movementPhases[subPhase];
        return movementPhase.HandlePhase(gameStats, _battleroundEvents);
    }

    internal static bool HandleMove(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents, MovementPhase subPhase)
    {
        var movementPhase = _movementPhases[subPhase];
        return movementPhase.HandleMove(gameStats, _battleroundEvents);
    }
    public static MovementPhase SetPhase(MovementPhase subPhase)
    {
        var movementPhase = _movementPhases[subPhase];
        return movementPhase.SetPhase();
    }
}