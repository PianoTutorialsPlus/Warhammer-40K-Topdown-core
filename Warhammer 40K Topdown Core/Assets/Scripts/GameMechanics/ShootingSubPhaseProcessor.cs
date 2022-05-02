﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WH40K.Essentials;
using WH40K.GameMechanics.Combat;

namespace WH40K.GameMechanics
{

    /// <summary>
    /// This script processes the communication between the shooting phase manager and the main shooting sub phases executables.
    /// </summary>

    public class ShootingSubPhaseProcessor
    {
        // Variables
        private static Dictionary<ShootingSubEvents, ShootingSubPhases> _shootingSubPhase = new Dictionary<ShootingSubEvents, ShootingSubPhases>();
        public static bool _initialized;
        private static IResult _result;

        public ShootingSubPhaseProcessor(IResult result)
        {
            _result = result;
        }

        private static void Initialize()
        {
            if (_initialized) return;
                _shootingSubPhase.Clear();

            var allShootingSubPhases = Assembly.GetAssembly(typeof(ShootingSubPhases)).GetTypes()
                .Where(t => typeof(ShootingSubPhases).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var subphase in allShootingSubPhases)
            {
                ShootingSubPhases shootingSubPhase = Activator.CreateInstance(subphase, _result) as ShootingSubPhases;
                _shootingSubPhase.Add(shootingSubPhase.SubEvents, shootingSubPhase);
            }

            _initialized = true;
        }
        public static void SetCalculation(ShootingSubEvents subPhase)
        {
            Initialize();

            var shootingSubPhase = _shootingSubPhase[subPhase];
            shootingSubPhase.SetCalculation(_result);
        }
        public static void HandleShooting(ShootingSubEvents subPhase)
        {
            Initialize();

            var shootingSubPhase = _shootingSubPhase[subPhase];
            shootingSubPhase.HandleShooting();
        }
    }
}