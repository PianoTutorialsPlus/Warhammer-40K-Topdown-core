using GameMechanics.Combat;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WH40K.Essentials;
using WH40K.GameMechanics;
using WH40K.UI;

namespace WH40K.Initialize
{
    public class Initializer : MonoBehaviour, IInteractionManager, IGamePhase, IResult
    {
        public static bool _initializedManager = false;

        [SerializeField] private PlayerSO _player1;
        [SerializeField] private PlayerSO _player2;
        [SerializeField] public GameStatsSO _gameStats;
        [SerializeField] private BattleRoundsSO _battleroundEvents;
        [SerializeField] private RollTheDiceSO _diceAction;
        [SerializeField] private RollTheDiceSO _diceSubResult;
        [SerializeField] private RollTheDiceSO _diceResult;
        public RollTheDiceSO DiceAction => _diceAction;
        public RollTheDiceSO DiceSubResult => _diceSubResult;
        public RollTheDiceSO DiceResult => _diceResult;
        public GameStatsSO GameStats { get => _gameStats; set => _gameStats = value; }
        // Dictionaries
        private static Dictionary<GamePhase, PhaseManagerBase> _gamePhaseManagers = new Dictionary<GamePhase, PhaseManagerBase>();
        public Dictionary<GamePhase, PhaseManagerBase> GamePhaseManagers { get => _gamePhaseManagers; }

        public IPhase BattleroundEvents { get => _battleroundEvents; }
        private void Awake()
        {
            Initialize();
            InitializeManager();
            InitializeProcessors();
        }

        private void InitializeProcessors()
        {
            new GamePhaseProcessor(this);
            new MovementPhaseProcessor(this);
            new ShootingPhaseProcessor(this);
            new ShootingSubPhaseProcessor(this);
            new CombatProcessor(this);
        }

        private void Initialize()
        {
            _gameStats.turn = 1;
            _gameStats.ActiveUnit = null;
            _gameStats.EnemyUnit = null;
            _gameStats.ActivePlayer = _player1;
            _gameStats.EnemyPlayer = _player2;
        }
        private void InitializeManager()
        {
            if (_initializedManager) return;
            _gamePhaseManagers.Clear();

            var allPhases = Assembly.GetAssembly(typeof(PhaseManagerBase)).GetTypes()
                .Where(t => typeof(PhaseManagerBase).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var subphase in allPhases)
            {
                PhaseManagerBase instance = gameObject.GetComponentInChildren(subphase) as PhaseManagerBase;
                _gamePhaseManagers.Add(instance.SubEvents, instance);
            }
            //_gamePhaseManagers = _gamePhases.ToDictionary(key => key.SubEvents, value => value);

            _initializedManager = true;
        }
    }
}
