﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// This script processes the communication between the movement phase manager and the main movement phases executables.
/// </summary>

public static class MovementPhaseProcessor
{
    // Variables
    private static Dictionary<MovementPhase, MovementPhases> _movementPhases = new Dictionary<MovementPhase, MovementPhases>();
    public static bool _initialized;

    private static GameStatsSO _gamesStats;
    private static IPhase _battleroundEvents;
    private static InputReader _inputReader;

    //private static 

    private static void Initialize()
    {
        if (_initialized) return;
        _movementPhases.Clear();

        var allShootingPhases = Assembly.GetAssembly(typeof(MovementPhases)).GetTypes()
            .Where(t => typeof(MovementPhases).IsAssignableFrom(t) && t.IsAbstract == false);

        foreach (var subphase in allShootingPhases)
        {
            MovementPhases movementPhases = Activator.CreateInstance(subphase, _battleroundEvents) as MovementPhases;
            _movementPhases.Add(movementPhases.SubEvents, movementPhases);
        }

        _initialized = true;
    }
    public static void InjectParamers(GameStatsSO gameStats, BattleRoundsSO battleroundEvents, InputReader inputReader)
    {
        _gamesStats = gameStats;
        _battleroundEvents = battleroundEvents;
        _inputReader = inputReader;
    }
    public static bool HandleSelection(MovementPhase subPhase)
    {
        Initialize();

        var movementPhase = _movementPhases[subPhase];
        return movementPhase.HandlePhase(_gamesStats);
    }

    internal static bool HandleMovement(MovementPhase subPhase)
    {
        Initialize();

        var movementPhase = _movementPhases[subPhase];
        return movementPhase.HandlePhase(_gamesStats);
    }

    public static MovementPhase SetPhase(MovementPhase subPhase)
    {
        Initialize();

        var movementPhase = _movementPhases[subPhase];
        return movementPhase.SetPhase();
    }

    public static bool Next(MovementPhase subPhase)
    {
        Initialize();

        var movementPhase = _movementPhases[subPhase];
        return movementPhase.Next(_gamesStats);
    }

    //public static void InjectParamers(GameStatsSO gameStats, BattleRoundsSO battleroundEvents, InputReader inputReader)
    //{
    //    _gamesStats = gameStats;
    //    _battleroundEvents = battleroundEvents;
    //    _inputReader = inputReader;
    //}
    //public static bool HandleSelection(GameStatsSO gameStats, BattleRoundsSO battleroundEvents, MovementPhase subPhase)
    //{
    //    Initialize();

    //    var movementPhase = _movementPhases[subPhase];
    //    return movementPhase.HandlePhase(gameStats, battleroundEvents);
    //}

    //internal static bool HandleMovement(GameStatsSO gameStats, BattleRoundsSO battleroundEvents, MovementPhase subPhase)
    //{
    //    Initialize();

    //    var movementPhase = _movementPhases[subPhase];
    //    return movementPhase.HandleMove(gameStats, battleroundEvents);
    //}

    //public static MovementPhase SetPhase(MovementPhase subPhase)
    //{
    //    Initialize();

    //    var movementPhase = _movementPhases[subPhase];
    //    return movementPhase.SetPhase();
    //}

    //public static bool Next(GameStatsSO gameStats, MovementPhase subPhase)
    //{
    //    Initialize();

    //    var movementPhase = _movementPhases[subPhase];
    //    return movementPhase.Next(gameStats);
    //}
}