using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class ShootingPhaseProcessor
{
    private static Dictionary<ShootingPhase, ShootingPhases> _shootingPhases = new Dictionary<ShootingPhase, ShootingPhases>();

    public static bool _initialized;


    private static void Initialize()
    {
        _shootingPhases.Clear();

        var allShootingPhases = Assembly.GetAssembly(typeof(ShootingPhases)).GetTypes()
            .Where(t => typeof(ShootingPhases).IsAssignableFrom(t) && t.IsAbstract == false);

        foreach (var subphase in allShootingPhases)
        {
            Debug.Log("Dictionary: " + subphase.Name);
            ShootingPhases shootingPhases = Activator.CreateInstance(subphase) as ShootingPhases;
            _shootingPhases.Add(shootingPhases.SubEvents, shootingPhases);
        }

        _initialized = true;
    }

    public static bool HandlePhase(GameStatsSO gameStats,BattleRoundsSO _battleroundEvents, ShootingPhase subPhase)
    {
        if (!_initialized) Initialize();

        var shootingPhase = _shootingPhases[subPhase];
        return shootingPhase.HandlePhase(gameStats, _battleroundEvents);
    }

    public static bool HandlePhase(ShootingPhase subPhase)
    {
        var shootingPhase = _shootingPhases[subPhase];
        return shootingPhase.HandlePhase();
    }

    public static ShootingPhase SetPhase(ShootingPhase subPhase)
    {
        var shootingPhase = _shootingPhases[subPhase];
        return shootingPhase.SetPhase();
    }


}