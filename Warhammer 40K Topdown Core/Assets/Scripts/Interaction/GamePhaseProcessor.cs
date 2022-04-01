using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WH40K.GamePhaseHandling;
using WH40K.ShootingPhaseHandler;

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
        if (_initialized) return;
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

    public static void EnableNextPhase(Dictionary<GamePhase, PhaseManagerBase> gamePhaseManagers, GamePhase subPhase)
    {
        Initialize();

        var gamePhase = _gamePhases[subPhase];
        var gamePhaseManager = gamePhaseManagers[subPhase];

        gamePhase.EnableNextPhase(gamePhaseManager);
    }

    public static void ResetPreviousPhase(Dictionary<GamePhase, PhaseManagerBase> gamePhaseManagers, GamePhase subPhase)
    {
        Initialize();

        var gamePhase = _gamePhases[subPhase];
        var gamePhaseManager = gamePhaseManagers[subPhase];

        gamePhase.ResetPreviousPhase(gamePhaseManager);
    }

    public static void ResetActivePlayerUnits(GameStatsSO gameStats, GamePhase subPhase)
    {
        Initialize();

        var gamePhase = _gamePhases[subPhase];
        gamePhase.ResetActivePlayerUnits(gameStats);
    }

    public static bool IsEndOfPlayerTurn(GamePhase subPhase)
    {
        Initialize();

        var gamePhase = _gamePhases[subPhase];
        return gamePhase.IsEndOfPlayerTurn();
    }

    internal static IEnumerable<GamePhase> GetAbilityByName()
    {
        Initialize();

        return _gamePhases.Keys;
    }

    public static GamePhase SetNextPhaseToActive(GamePhase subPhase)
    {
        Initialize();

        var gamePhase = _gamePhases[subPhase];
        return gamePhase.SetNextPhaseToActive();
    }
}