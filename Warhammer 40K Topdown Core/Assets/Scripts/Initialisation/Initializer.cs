using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WH40K.Core;
using WH40K.EventChannels;
using WH40K.Events;
using WH40K.GamePhaseEvents;
using WH40K.PlayerEvents;

namespace WH40K
{
    public class Initializer : MonoBehaviour, IInteractionManager, IGamePhase, IResult
    {
        public static bool _initialized = false;

        [SerializeField] private PlayerSO _player1;
        [SerializeField] private PlayerSO _player2;
        //[SerializeField] private GameTableSO _gameTable;

        [SerializeField] private BattleRoundsSO _battleroundEvents;
        [SerializeField] private RollTheDiceEventChannelSO _diceAction;
        [SerializeField] private RollTheDiceEventChannelSO _diceSubResult;
        [SerializeField] private RollTheDiceEventChannelSO _diceResult;
        public RollTheDiceEventChannelSO DiceAction => _diceAction;
        public RollTheDiceEventChannelSO DiceSubResult => _diceSubResult;
        public RollTheDiceEventChannelSO DiceResult => _diceResult;

        // Dictionaries
        private static Dictionary<GamePhase, PhaseManagerBase> _gamePhaseManagers = new Dictionary<GamePhase, PhaseManagerBase>();
        public Dictionary<GamePhase, PhaseManagerBase> GamePhaseManagers { get => _gamePhaseManagers; }

        public IPhase BattleroundEvents { get => _battleroundEvents; }
        private void Awake()
        {
            Initialize();
            InitializeManager();
            InitializeProcessors();
            AddUnitPhases();

            _initialized = true;
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
            GameStats.Turn = 1;
            GameStats.ActiveUnit = null;
            GameStats.EnemyUnit = null;
            GameStats.ActivePlayer = _player1;
            GameStats.EnemyPlayer = _player2;
            //GameStats.GameTable = _gameTable;
        }
        protected void AddUnitPhases()
        {
            if (_initialized != false) return;
            foreach (Unit child in GameStats.ActivePlayer.PlayerUnits)
            {
                child.gameObject.AddComponent<UnitMovementPhase>();
                child.unitMovementPhase = child.GetComponent<UnitMovementPhase>();

                child.gameObject.AddComponent<UnitShootingPhase>();
                child.unitShootingPhase = child.GetComponent<UnitShootingPhase>();
            }
            foreach (Unit child in GameStats.EnemyPlayer.PlayerUnits)
            {
                child.gameObject.AddComponent<UnitMovementPhase>();
                child.unitMovementPhase = child.GetComponent<UnitMovementPhase>();

                child.gameObject.AddComponent<UnitShootingPhase>();
                child.unitShootingPhase = child.GetComponent<UnitShootingPhase>();
            }
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
