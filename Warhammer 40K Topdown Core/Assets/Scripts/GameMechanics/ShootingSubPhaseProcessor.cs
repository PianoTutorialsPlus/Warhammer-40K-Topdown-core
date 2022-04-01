using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WH40K.Combat;

namespace WH40K.ShootingPhaseHandler
{

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
        public static ICalculation SetCalculation(IResult calculations, ShootingSubEvents subPhase)
        {
            if (!_initialized) Initialize();

            var shootingSubPhase = _shootingSubPhase[subPhase];
            return shootingSubPhase.SetCalculation(calculations);
        }
        public static void HandleShooting(List<int> parameter, ICalculation action, GameStatsSO gameStats, ShootingSubEvents subPhase)
        {
            if (!_initialized) Initialize();

            var shootingSubPhase = _shootingSubPhase[subPhase];
            shootingSubPhase.HandleShooting(parameter, action, gameStats);
        }
        public static ShootingSubEvents SetSubPhase(ShootingSubEvents subPhase)
        {
            if (!_initialized) Initialize();

            var shootingSubPhase = _shootingSubPhase[subPhase];
            return shootingSubPhase.SetSubPhase();
        }
    }
}