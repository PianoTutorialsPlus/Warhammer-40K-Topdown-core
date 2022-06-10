using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WH40K.Gameplay.Events;
using WH40K.Stats;

namespace WH40K.Gameplay.GamePhaseEvents
{

    /// <summary>
    /// This script processes the communication between the movement phase manager and the main movement phases executables.
    /// </summary>

    public class MovementPhaseProcessor
    {
        // Variables
        private static Dictionary<MovementPhase, MovementPhases> _movementPhases = new Dictionary<MovementPhase, MovementPhases>();
        private static bool _initialized;
        private static GameStatsSO _gameStats;
        private static IPhase _gamePhase;
        public bool Initialized { get => _initialized; protected set => _initialized = value; }

        public MovementPhaseProcessor(GameStatsSO gameStats, IPhase gamePhase)
        {
            _gameStats = gameStats;
            _gamePhase = gamePhase;
        }

        private static void Initialize()
        {
            if (_initialized) return;
            _movementPhases.Clear();

            var allShootingPhases = Assembly.GetAssembly(typeof(MovementPhases)).GetTypes()
                .Where(t => typeof(MovementPhases).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var subphase in allShootingPhases)
            {
                MovementPhases movementPhases = Activator.CreateInstance(subphase, _gameStats, _gamePhase) as MovementPhases;
                _movementPhases.Add(movementPhases.SubEvents, movementPhases);
            }

            _initialized = true;
        }

        public static void HandlePhase(MovementPhase subPhase)
        {
            Initialize();

            var movementPhase = _movementPhases[subPhase];
            movementPhase.HandlePhase();
        }
        public static bool Next(MovementPhase subPhase)
        {
            Initialize();

            var movementPhase = _movementPhases[subPhase];
            return movementPhase.Next();
        }

        public static void ClearPhase(MovementPhase subPhase)
        {
            Initialize();

            var movementPhase = _movementPhases[subPhase];
            movementPhase.ClearPhase();
        }
        internal static IEnumerable<MovementPhase> GetAbilityByName()
        {
            Initialize();

            return _movementPhases.Keys;
        }
    }
}