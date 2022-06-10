using System.Collections.Generic;
using UnityEngine;
using WH40K.Gameplay.EventChannels;
using Zenject;
using WH40K.Stats.Player;
using WH40K.Stats;

namespace WH40K.Gameplay.GamePhaseEvents
{
    // Enum initialization
    public enum InteractionType { None = 0, Activate, ShowStats }



    /// <summary>
    /// This script takes care of the interactions. It communicates with the UI Manager and triggers the GamePhase Processor.
    /// When "End Turn" button is pressed, a new gamephase is invoked.
    /// </summary>

    public class InteractionManager : MonoBehaviour//, IInteractionManager
    {
        //Initialization
        // Gameplay
        private PlayerSO _player1;
        private PlayerSO _player2;
        private GameStatsSO _gameStats;

        // Events
        private GameStatsEventChannelSO _setPhaseEvent = default;
        private GameInfoUIEventChannelSO _toggleGameinfoUI = default;
        private BattleroundEventChannelSO _toggleBattleRounds = default;

        //Queues
        private Queue<GamePhase> _gamePhase = new Queue<GamePhase>();

        private void Awake()
        {
            EnqueueGamePhase();
        }

        private void Start()
        {
            _toggleBattleRounds.RaiseEvent(); //Initialization    

        }

        private void OnEnable()
        {
            Debug.Log("interActionManager");
            if (_setPhaseEvent != null) _setPhaseEvent.OnEventRaised += SetPhase;

            _toggleGameinfoUI.RaiseEvent(true);

        }
        [Inject]
        public void Construct(
            List<PlayerSO> players,
            GameStatsSO gameStats,
            BattleroundEventChannelSO battleroundEventChannel,
            GameInfoUIEventChannelSO gameinfoUIEventChannel,
            GameStatsEventChannelSO gameStatsEventChannel)
        {
            _player1 = players[0];
            _player2 = players[1];
            _gameStats = gameStats;
            _toggleBattleRounds = battleroundEventChannel;
            _toggleGameinfoUI = gameinfoUIEventChannel;
            _setPhaseEvent = gameStatsEventChannel;
        }

        private void EnqueueGamePhase()
        {
            foreach (GamePhase phase in GamePhaseProcessor.GetAbilityByName())
            {
                _gamePhase.Enqueue(phase);
            }
        }

        public void SetPhase()
        {
            ResetPreviousPhase();
            SetNextPhaseToActive();
            GamePhaseProcessor.EnableNextPhase(_gamePhase.Peek());

            if (IsEndOfPlayerTurn(_gamePhase.Peek()))
            {
                TogglePlayers();
                SetNextBattleRound();
            }
            ToggleBattleRoundsAndUI();
        }

        private void ResetPreviousPhase()
        {
            GamePhaseProcessor.ResetPreviousPhase(_gamePhase.Peek());
        }

        private void SetNextPhaseToActive()
        {
            _gamePhase.Enqueue(_gamePhase.Dequeue());
            _gameStats.Phase = _gamePhase.Peek();
            GamePhaseProcessor.SetActivePlayerUnits(_gamePhase.Peek());
        }

        private bool IsEndOfPlayerTurn(GamePhase gamePhases)
        {
            return GamePhaseProcessor.IsEndOfPlayerTurn(gamePhases);
        }

        public void TogglePlayers()
        {
            if (_gameStats.ActivePlayer == _player1)
            {
                _gameStats.ActivePlayer = _player2;
                _gameStats.EnemyPlayer = _player1;
            }
            else
            {
                _gameStats.ActivePlayer = _player1;
                _gameStats.EnemyPlayer = _player2;
            }
        }

        private void SetNextBattleRound()
        {
            if (_gameStats.ActivePlayer == _player1) _gameStats.Turn += 1;
        }

        private void ToggleBattleRoundsAndUI()
        {
            _toggleGameinfoUI.RaiseEvent(true);
            _toggleBattleRounds.RaiseEvent();
        }
    }
}
