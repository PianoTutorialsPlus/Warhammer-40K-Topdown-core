using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WH40K.Events;

namespace WH40K.GamePhaseEvents
{
    /// <summary>
    /// This script processes the communication between the shooting phase manager and the main shooting phases executables.
    /// </summary>

    public class ShootingPhaseProcessor
    {
        // Variables
        private static Dictionary<ShootingPhase, ShootingPhases> _shootingPhases = new Dictionary<ShootingPhase, ShootingPhases>();
        public static bool _initialized;
        private static IPhase _gamePhase;

        public bool Initialized { get => _initialized; protected set => _initialized = value; }

        public ShootingPhaseProcessor(IPhase gamePhase)
        {
            _gamePhase = gamePhase;
        }

        private static void Initialize()
        {
            if (_initialized) return;
            _shootingPhases.Clear();

            var allShootingPhases = Assembly.GetAssembly(typeof(ShootingPhases)).GetTypes()
                .Where(t => typeof(ShootingPhases).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var subphase in allShootingPhases)
            {
                ShootingPhases shootingPhases = Activator.CreateInstance(subphase, _gamePhase) as ShootingPhases;
                _shootingPhases.Add(shootingPhases.SubEvents, shootingPhases);
            }

            _initialized = true;
        }
        public static void HandlePhase(ShootingPhase subPhase)
        {
            Initialize();

            var shootingPhase = _shootingPhases[subPhase];
            shootingPhase.HandlePhase();
        }
        public static bool Next(ShootingPhase subPhase)
        {
            Initialize();

            var shootingPhase = _shootingPhases[subPhase];
            return shootingPhase.Next();
        }
        public static void ClearPhase(ShootingPhase subPhase)
        {
            Initialize();

            var shootingPhase = _shootingPhases[subPhase];
            shootingPhase.ClearPhase();
        }
        internal static IEnumerable<ShootingPhase> GetAbilityByName()
        {
            Initialize();

            return _shootingPhases.Keys;
        }
    }
}