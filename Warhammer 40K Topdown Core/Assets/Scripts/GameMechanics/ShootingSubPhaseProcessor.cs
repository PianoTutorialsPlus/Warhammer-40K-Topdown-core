using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// This script processes the communication between the shooting phase manager and the main shooting sub phases executables.
/// </summary>

public static class ShootingSubPhaseProcessor
{
    // Variables
    private static Dictionary<ShootingSubEvents, ShootingSubPhases> _shootingSubPhase = new Dictionary<ShootingSubEvents, ShootingSubPhases>();
    public static bool _initialized;

    private static void Initialize()
    {
        _shootingSubPhase.Clear();

        var allShootingSubPhases = Assembly.GetAssembly(typeof(ShootingSubPhases)).GetTypes()
            .Where(t => typeof(ShootingSubPhases).IsAssignableFrom(t) && t.IsAbstract == false);

        foreach (var subphase in allShootingSubPhases)
        {
            ShootingSubPhases shootingSubPhase = Activator.CreateInstance(subphase) as ShootingSubPhases;
            _shootingSubPhase.Add(shootingSubPhase.SubEvents, shootingSubPhase);
        }

        _initialized = true;
    }

    public static CalculationBaseSO SetCalculation(List<CalculationBaseSO> calculations, ShootingSubEvents subPhase)
    {
        if (!_initialized) Initialize();

        var shootingSubPhase = _shootingSubPhase[subPhase];
        return shootingSubPhase.SetCalculation(calculations);
    }


    public static void HandleShooting(List<int> parameter, CalculationBaseSO action, GameStatsSO gameStats, ShootingSubEvents subPhase)
    {
        if (!_initialized) Initialize();

        var shootingSubPhase = _shootingSubPhase[subPhase];
        shootingSubPhase.HandleShooting(parameter, action, gameStats);
    }

    public static List<int> ProcessResult(List<int> result, ShootingSubEvents subPhase)
    {
        if (!_initialized) Initialize();

        var shootingSubPhase = _shootingSubPhase[subPhase];
        return shootingSubPhase.ProcessResult(result);

    }
    public static ShootingSubEvents SetSubPhase(ShootingSubEvents subPhase)
    {
        if (!_initialized) Initialize();

        var shootingSubPhase = _shootingSubPhase[subPhase];
        return shootingSubPhase.SetSubPhase();
    }
    public static List<int> GetResult(int equalizer, List<int> result, ShootingSubEvents subPhase)
    {
        if (!_initialized) Initialize();

        var shootingSubPhase = _shootingSubPhase[subPhase];
        return shootingSubPhase.GetResult(equalizer, result);
    }


}