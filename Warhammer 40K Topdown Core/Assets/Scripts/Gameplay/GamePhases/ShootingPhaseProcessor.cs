using System;
using System.Collections.Generic;
using WH40K.Stats;

namespace WH40K.Gameplay.GamePhaseEvents
{
    /// <summary>
    /// This script processes the communication between the shooting phase manager and the main shooting phases executables.
    /// </summary>

    public class ShootingPhaseProcessor
    {
        // Variables
        private static Dictionary<Enum, ShootingPhases> _shootingPhases = new Dictionary<Enum, ShootingPhases>();
        public static bool _initialized;
        private static GamePhaseFactory _factory;

        public bool Initialized { get => _initialized; protected set => _initialized = value; }

        public ShootingPhaseProcessor(GamePhaseFactory factory)
        {
            _factory = factory;
        }

        private static void Initialize()
        {
            if (_initialized) return;
            _shootingPhases = _factory.Create(_shootingPhases);

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
        internal static IEnumerable<Enum> GetAbilityByName()
        {
            Initialize();

            return _shootingPhases.Keys;
        }
    }
}