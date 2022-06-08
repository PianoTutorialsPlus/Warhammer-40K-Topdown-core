using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WH40K.Gameplay.GamePhaseEvents
{

    /// <summary>
    /// This script processes the communication between the shooting phase manager and the main shooting sub phases executables.
    /// </summary>

    public class ShootingSubPhaseProcessor
    {
        // Variables
        private static Dictionary<ShootingSubEvents, ShootingSubPhases> _shootingSubPhase = new Dictionary<ShootingSubEvents, ShootingSubPhases>();
        public static bool _initialized;

        public bool Initialized { get => _initialized; protected set => _initialized = value; }

        public ShootingSubPhaseProcessor() { }

        private static void Initialize()
        {
            if (_initialized) return;
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
        public static void HandleShooting(ShootingSubEvents subPhase, List<int> parameter)
        {
            Initialize();

            var shootingSubPhase = _shootingSubPhase[subPhase];
            shootingSubPhase.HandleShooting(parameter);
        }
        public static void Next(ShootingSubEvents subPhase)
        {
            Initialize();

            var shootingSubPhase = _shootingSubPhase[subPhase];
            shootingSubPhase.Next();
        }
        internal static IEnumerable<ShootingSubEvents> GetAbilityByName()
        {
            Initialize();

            return _shootingSubPhase.Keys;
        }
    }
}