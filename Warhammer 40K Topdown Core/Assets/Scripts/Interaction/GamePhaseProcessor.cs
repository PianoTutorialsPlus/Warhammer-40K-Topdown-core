using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class GamePhaseProcessor
{
    private static Dictionary<GamePhase, GamePhases> _gamePhases = new Dictionary<GamePhase, GamePhases>();

    public static bool _initialized;

    private static void Initialize()
    {
        _gamePhases.Clear();

        var allPhases = Assembly.GetAssembly(typeof(GamePhases)).GetTypes()
            .Where(t => typeof(GamePhases).IsAssignableFrom(t) && t.IsAbstract == false);

        foreach (var subphase in allPhases)
        {
            Debug.Log("Dictionary: " + subphase.Name);
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


    public static GamePhase SetPhase(GameStatsSO gameStats,GamePhase subPhase)
    {
        if (!_initialized) Initialize();

        var gamePhase = _gamePhases[subPhase];
        return gamePhase.SetPhase(gameStats,subPhase);
    }
}