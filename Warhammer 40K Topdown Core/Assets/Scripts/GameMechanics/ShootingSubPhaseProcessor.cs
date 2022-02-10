using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class ShootingSubPhaseProcessor
{
    private static Dictionary<ShootingSubEvents, ShootingSubPhase> _shootingSubPhase = new Dictionary<ShootingSubEvents, ShootingSubPhase>();

    public static bool _initialized;


    private static void Initialize()
    {
        _shootingSubPhase.Clear();

        var allShootingSubPhases = Assembly.GetAssembly(typeof(ShootingSubPhase)).GetTypes()
            .Where(t => typeof(ShootingSubPhase).IsAssignableFrom(t) && t.IsAbstract == false);

        foreach (var subphase in allShootingSubPhases)
        {
            Debug.Log("Dictionary: " + subphase.Name);
            ShootingSubPhase shootingSubPhase = Activator.CreateInstance(subphase) as ShootingSubPhase;
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

        //if (!_initialized) Initialize();

        var shootingSubPhase = _shootingSubPhase[subPhase];
        shootingSubPhase.HandleShooting(parameter, action, gameStats);
    }

    public static List<int> ProcessResult(List<int> result, ShootingSubEvents subPhase)
    {
        Debug.Log(subPhase.ToString());
        var shootingSubPhase = _shootingSubPhase[subPhase];
        return shootingSubPhase.ProcessResult(result);

    }
    public static ShootingSubEvents SetSubPhase(ShootingSubEvents subPhase)
    {
        var shootingSubPhase = _shootingSubPhase[subPhase];
        return shootingSubPhase.SetSubPhase();
    }
    public static List<int> GetResult(int equalizer, List<int> result, ShootingSubEvents subPhase)
    {
        var shootingSubPhase = _shootingSubPhase[subPhase];
        return shootingSubPhase.GetResult(equalizer,result);
    }

    
}