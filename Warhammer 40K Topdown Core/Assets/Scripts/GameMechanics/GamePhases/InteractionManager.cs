using System.Collections.Generic;
using UnityEngine;
using WH40K.Core;
using WH40K.EventChannels;
using WH40K.PlayerEvents;
using Zenject;

namespace WH40K.GamePhaseEvents
{
    // Enum initialization
    public enum InteractionType { None = 0, Activate, ShowStats }
    public enum GamePhase { MovementPhase, ShootingPhase }
    public enum MovementPhase { None = 0, Selection, Move, Next }
    public enum ShootingPhase { None = 0, Selection, Shoot, Next }
    public enum ShootingSubEvents { None = 0, SelectEnemy, Hit, Wound, Save, Damage }


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
            BattleroundEventChannelSO battleroundEventChannel,
            GameInfoUIEventChannelSO gameinfoUIEventChannel,
            GameStatsEventChannelSO gameStatsEventChannel)
        {
            _player1 = players[0];
            _player2 = players[1];
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
            GameStats.Phase = _gamePhase.Peek();
            GamePhaseProcessor.SetActivePlayerUnits(_gamePhase.Peek());
        }

        private bool IsEndOfPlayerTurn(GamePhase gamePhases)
        {
            return GamePhaseProcessor.IsEndOfPlayerTurn(gamePhases);
        }

        public void TogglePlayers()
        {
            if (GameStats.ActivePlayer == _player1)
            {
                GameStats.ActivePlayer = _player2;
                GameStats.EnemyPlayer = _player1;
            }
            else
            {
                GameStats.ActivePlayer = _player1;
                GameStats.EnemyPlayer = _player2;
            }
        }

        private void SetNextBattleRound()
        {
            if (GameStats.ActivePlayer == _player1) GameStats.Turn += 1;
        }

        private void ToggleBattleRoundsAndUI()
        {
            _toggleGameinfoUI.RaiseEvent(true);
            _toggleBattleRounds.RaiseEvent();
        }
    }
}
