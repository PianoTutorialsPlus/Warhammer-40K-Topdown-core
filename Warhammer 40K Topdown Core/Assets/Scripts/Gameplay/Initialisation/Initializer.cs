using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WH40K.Gameplay.Events;
using WH40K.Gameplay.GamePhaseEvents;
using Zenject;
using WH40K.Stats.Player;
using WH40K.Stats;

namespace WH40K.Gameplay
{
    public class Initializer : MonoBehaviour, IInteractionManager, IGamePhase/*, IResult*/
    {
        public static bool _initialized = false;

        // Dictionaries
        private static Dictionary<GamePhase, PhaseManagerBase> _gamePhaseManagers = new Dictionary<GamePhase, PhaseManagerBase>();
        public Dictionary<GamePhase, PhaseManagerBase> GamePhaseManagers { get => _gamePhaseManagers; }

        public IPhase BattleroundEvents { get; set; }/* => _battleroundEvents; }*/
        private void Awake()
        {
            InitializeManager();

            _initialized = true;
        }

        private void InitializeManager()
        {
            if (_initialized) return;
            _gamePhaseManagers.Clear();

            var allPhases = Assembly.GetAssembly(typeof(PhaseManagerBase)).GetTypes()
                .Where(t => typeof(PhaseManagerBase).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var subphase in allPhases)
            {
                PhaseManagerBase instance = gameObject.GetComponentInChildren(subphase) as PhaseManagerBase;
                _gamePhaseManagers.Add(instance.SubEvents, instance);
            }
            //_gamePhaseManagers = _gamePhases.ToDictionary(key => key.SubEvents, value => value);
        }
    }
}
