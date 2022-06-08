using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WH40K.Gameplay.Core;
using WH40K.Gameplay.Events;
using WH40K.Gameplay.GamePhaseEvents;
using Zenject;
using WH40K.Gameplay.PlayerEvents;

namespace WH40K.Gameplay
{
    public class Initializer : MonoBehaviour, IInteractionManager, IGamePhase/*, IResult*/
    {
        public static bool _initialized = false;

        private PlayerSO _player1;
        private PlayerSO _player2;
        //[SerializeField] private GameTableSO _gameTable;

        //[SerializeField] private BattleRoundsSO _battleroundEvents;
        //[SerializeField] private RollTheDiceEventChannelSO _diceAction;
        //[SerializeField] private RollTheDiceEventChannelSO _diceSubResult;
        //[SerializeField] private RollTheDiceEventChannelSO _diceResult;
        //public RollTheDiceEventChannelSO DiceAction => _diceAction;
        //public RollTheDiceEventChannelSO DiceSubResult => _diceSubResult;
        //public RollTheDiceEventChannelSO DiceResult => _diceResult;

        // Dictionaries
        private static Dictionary<GamePhase, PhaseManagerBase> _gamePhaseManagers = new Dictionary<GamePhase, PhaseManagerBase>();
        public Dictionary<GamePhase, PhaseManagerBase> GamePhaseManagers { get => _gamePhaseManagers; }

        public IPhase BattleroundEvents { get; set; }/* => _battleroundEvents; }*/
        private void Awake()
        {
            Initialize();
            InitializeManager();

            _initialized = true;
        }

        [Inject]
        public void Construct(

            List<PlayerSO> players)
        {
            _player1 = players[0];
            _player2 = players[1];
            Debug.Log(_player2.name);
        }

        private void Initialize()
        {
            GameStats.Turn = 1;
            GameStats.ActiveUnit = null;
            GameStats.EnemyUnit = null;
            GameStats.ActivePlayer = _player1;
            GameStats.EnemyPlayer = _player2;
            Debug.Log(GameStats.EnemyPlayer);
            //GameStats.GameTable = _gameTable;
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
