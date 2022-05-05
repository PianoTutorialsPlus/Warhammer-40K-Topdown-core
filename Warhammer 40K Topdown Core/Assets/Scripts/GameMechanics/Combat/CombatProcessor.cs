using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WH40K.GameMechanics;
using WH40K.GameMechanics.Combat;

namespace GameMechanics.Combat
{
    public class CombatProcessor
    {
        // Variables
        private static Dictionary<ShootingSubEvents, CombatPhases> _combatPhase = new Dictionary<ShootingSubEvents, CombatPhases>();
        public static bool _initialized;
        private static IResult _result;

        public bool Initialized { get => _initialized; protected set => _initialized = value; }

        public CombatProcessor(IResult result)
        {
            _result = result;
        }

        private static void Initialize()
        {
            if (_initialized) return;
            _combatPhase.Clear();

            var allShootingSubPhases = Assembly.GetAssembly(typeof(CombatPhases)).GetTypes()
                .Where(t => typeof(CombatPhases).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var subphase in allShootingSubPhases)
            {
                CombatPhases combatPhase = Activator.CreateInstance(subphase, _result) as CombatPhases;
                _combatPhase.Add(combatPhase.SubEvents, combatPhase);
            }

            _initialized = true;
        }
        public static void Action(ShootingSubEvents subPhase, List<int> parameter)
        {
            Initialize();

            var combatPhase = _combatPhase[subPhase];
            combatPhase.Action(parameter);
        }
    }
}
