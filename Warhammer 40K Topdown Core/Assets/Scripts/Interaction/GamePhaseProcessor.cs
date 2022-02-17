using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// This script processes the communication between the interaction manager and the main game phases executables.
/// </summary>
public static class GamePhaseProcessor
{
    // Variables
    private static Dictionary<GamePhase, GamePhases> _gamePhases = new Dictionary<GamePhase, GamePhases>();
    public static bool _initialized;

    private static void Initialize()
    {
        // Reset
        _gamePhases.Clear();

        var allPhases = Assembly.GetAssembly(typeof(GamePhases)).GetTypes()
            .Where(t => typeof(GamePhases).IsAssignableFrom(t) && t.IsAbstract == false);

        foreach (var subphase in allPhases)
        {
            GamePhases gamePhases = Activator.CreateInstance(subphase) as GamePhases;
            _gamePhases.Add(gamePhases.SubEvents, gamePhases);
        }

        _initialized = true;
    }

    public static bool HandlePhase(List<PhaseManagerBase> gamePhases, GamePhase subPhase)
    {
        if (!_initialized) Initialize();

        var gamePhase = _gamePhases[subPhase];
        return gamePhase.HandlePhase(gamePhases);
    }

    public static void ResetPhase(GameStatsSO gameStats, List<PhaseManagerBase> gamePhases, GamePhase subPhase)
    {
        if (!_initialized) Initialize();

        var gamePhase = _gamePhases[subPhase];
        gamePhase.ResetPhase(gameStats, gamePhases);
        gamePhase.ResetUnits(gameStats);
    }


    public static GamePhase SetPhase(GameStatsSO gameStats, GamePhase subPhase)
    {
        if (!_initialized) Initialize();

        var gamePhase = _gamePhases[subPhase];
        return gamePhase.SetPhase(gameStats, subPhase);
    }
}