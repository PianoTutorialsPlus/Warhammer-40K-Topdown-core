using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// This script processes the communication between the shooting phase manager and the main shooting phases executables.
/// </summary>

public static class ShootingPhaseProcessor
{
    // Variables
    private static Dictionary<ShootingPhase, ShootingPhases> _shootingPhases = new Dictionary<ShootingPhase, ShootingPhases>();
    public static bool _initialized;

    private static void Initialize()
    {
        _shootingPhases.Clear();

        var allShootingPhases = Assembly.GetAssembly(typeof(ShootingPhases)).GetTypes()
            .Where(t => typeof(ShootingPhases).IsAssignableFrom(t) && t.IsAbstract == false);

        foreach (var subphase in allShootingPhases)
        {
            ShootingPhases shootingPhases = Activator.CreateInstance(subphase) as ShootingPhases;
            _shootingPhases.Add(shootingPhases.SubEvents, shootingPhases);
        }

        _initialized = true;
    }

    public static bool HandlePhase(GameStatsSO gameStats, BattleRoundsSO _battleroundEvents, ShootingPhase subPhase)
    {
        if (!_initialized) Initialize();

        var shootingPhase = _shootingPhases[subPhase];
        return shootingPhase.HandlePhase(gameStats, _battleroundEvents);
    }

    public static bool HandlePhase(ShootingPhase subPhase)
    {
        if (!_initialized) Initialize();

        var shootingPhase = _shootingPhases[subPhase];
        return shootingPhase.HandlePhase();
    }

    public static ShootingPhase SetPhase(ShootingPhase subPhase)
    {
        if (!_initialized) Initialize();

        var shootingPhase = _shootingPhases[subPhase];
        return shootingPhase.SetPhase();
    }

    internal static bool Next(GameStatsSO gameStats, ShootingPhase subPhase)
    {
        if (!_initialized) Initialize();

        var shootingPhase = _shootingPhases[subPhase];
        return shootingPhase.Next(gameStats);
    }
}