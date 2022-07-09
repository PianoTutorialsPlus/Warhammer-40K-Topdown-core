using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WH40K.Stats;

namespace WH40K.Gameplay.GamePhaseEvents
{

    /// <summary>
    /// This script processes the communication between the shooting phase manager and the main shooting sub phases executables.
    /// </summary>

    public class ShootingSubPhaseProcessor
    {
        // Variables
        private static Dictionary<Enum, ShootingSubPhases> _shootingSubPhase = new Dictionary<Enum, ShootingSubPhases>();
        public static bool _initialized;
        private static GamePhaseFactory _factory;

        public bool Initialized { get => _initialized; protected set => _initialized = value; }

        public ShootingSubPhaseProcessor(GamePhaseFactory factory) 
        {
            _factory = factory;
        }

        private static void Initialize()
        {
            if (_initialized) return;
            _shootingSubPhase = _factory.Create(_shootingSubPhase);

            //var allShootingSubPhases = Assembly.GetAssembly(typeof(ShootingSubPhases)).GetTypes()
            //    .Where(t => typeof(ShootingSubPhases).IsAssignableFrom(t) && t.IsAbstract == false);

            //foreach (var subphase in allShootingSubPhases)
            //{
            //    ShootingSubPhases shootingSubPhase = Activator.CreateInstance(subphase) as ShootingSubPhases;
            //    _shootingSubPhase.Add(shootingSubPhase.SubEvents, shootingSubPhase);
            //}

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
        internal static IEnumerable<Enum> GetAbilityByName()
        {
            Initialize();

            return _shootingSubPhase.Keys;
        }
    }
}