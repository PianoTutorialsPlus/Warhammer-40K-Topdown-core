using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.Stats;

namespace WH40K.Gameplay.GamePhaseEvents
{
    public class CombatProcessor
    {
        // Variables
        private static Dictionary<Enum, CombatPhases> _combatPhase = new Dictionary<Enum, CombatPhases>();
        public static bool _initialized;
        private static GamePhaseFactory _factory;
        //private static GameStatsSO _gameStats;
        //private static IResult _result;

        public bool Initialized { get => _initialized; set => _initialized = value; }

        public CombatProcessor(
            GamePhaseFactory factory)
            //GameStatsSO gameStats, 
            //IResult result)
        {
            Debug.Log("Do I get Here");
            _factory = factory;
            //_gameStats = gameStats;
            //_result = result;
        }

        public static void Initialize()
        {
            if (_initialized) return;
            _combatPhase = _factory.Create(_combatPhase);

            //var allCombatSubPhases = Assembly.GetAssembly(typeof(CombatPhases)).GetTypes()
            //    .Where(t => typeof(CombatPhases).IsAssignableFrom(t) && t.IsAbstract == false);

            //foreach (var subphase in allCombatSubPhases)
            //{
            //    Debug.Log("CombatProcessor");
            //    CombatPhases combatPhase = Activator.CreateInstance(subphase, _result, _gameStats) as CombatPhases;
            //    _combatPhase.Add(combatPhase.SubEvents, combatPhase);
            //}

            _initialized = true;
        }
        public static void Action(ShootingSubEvents subPhase, List<int> parameter = null)
        {
            Initialize();

            var combatPhase = _combatPhase[subPhase];
            combatPhase.Action(parameter);
        }
        public static void Next(ShootingSubEvents subPhase)
        {
            Initialize();

            var combatPhase = _combatPhase[subPhase];
            combatPhase.Next();
        }
    }
}
