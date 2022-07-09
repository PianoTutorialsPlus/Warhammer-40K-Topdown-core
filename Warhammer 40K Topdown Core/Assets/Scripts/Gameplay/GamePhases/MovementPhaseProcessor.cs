using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WH40K.Gameplay.Events;
using WH40K.Stats;
using Zenject;

namespace WH40K.Gameplay.GamePhaseEvents
{

    /// <summary>
    /// This script processes the communication between the movement phase manager and the main movement phases executables.
    /// </summary>

    public class MovementPhaseProcessor 
    {
        // Variables
        private static Dictionary<Enum, MovementPhases> _movementPhases = new Dictionary<Enum, MovementPhases>();
        private static bool _initialized;
        private static GamePhaseFactory _factory;
        public bool Initialized { get => _initialized; protected set => _initialized = value; }

        public MovementPhaseProcessor(
            GamePhaseFactory factory)
        {
            _factory = factory;
        }

        private static void Initialize()
        {
            if (_initialized) return;
            _movementPhases = _factory.Create(_movementPhases);
            //_movementPhases.Clear();


            //var allShootingPhases = Assembly.GetAssembly(typeof(MovementPhases)).GetTypes()
            //    .Where(t => typeof(MovementPhases).IsAssignableFrom(t) && t.IsAbstract == false);
            //foreach (var subphase in allShootingPhases)
            //{
            //    MovementPhases movementPhases = _factory.Create(subphase);
            //    //MovementPhases movementPhases = Activator.CreateInstance(subphase, _gameStats, _gamePhase) as MovementPhases;
            //    _movementPhases.Add(movementPhases.SubEvents, movementPhases);
            //}

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
        internal static IEnumerable<Enum> GetAbilityByName()
        {
            Initialize();

            return _movementPhases.Keys;
        }
    }
}