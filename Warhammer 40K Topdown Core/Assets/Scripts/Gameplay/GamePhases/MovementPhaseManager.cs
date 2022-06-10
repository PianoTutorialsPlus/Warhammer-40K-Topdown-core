using System;
using System.Collections.Generic;
using UnityEngine;
using WH40K.Gameplay.EventChannels;
using WH40K.InputEvents;
using Zenject;
using WH40K.Stats;

namespace WH40K.Gameplay.GamePhaseEvents
{

    /// <summary>
    /// this script manages the movement phase. 
    /// When SetMovementPhase is called, it triggers the movement phase processor.
    /// The manager handles the movement phase, dependend on what subphase its currently in.
    /// </summary>

    public class MovementPhaseManager : PhaseManagerBase
    {
        // Gameplay
        private InputReader _inputReader;
        private GameStatsSO _gameStats;

        // Events
        private BattleroundEventChannelSO _setMovementPhaseEvent;
        private Settings _settings;

        // Enums
        private Queue<MovementPhase> movementPhase = new Queue<MovementPhase>();

        public override GamePhase SubEvents => GamePhase.MovementPhase;

        [Inject]
        public void Construct(
            GameStatsSO gameStats,
            BattleroundEventChannelSO battleroundEventChannel,
            Settings settings,
            InputReader inputReader)
        {
            _gameStats = gameStats;
            _setMovementPhaseEvent = battleroundEventChannel;
            _settings = settings;
            _inputReader = inputReader;
        }

        private void Awake()
        {
            EnqueueMovementPhase();

        }
        private void Start()
        {
            enabled = _settings.Enabled;
        }

        private void EnqueueMovementPhase()
        {
            foreach (MovementPhase phase in MovementPhaseProcessor.GetAbilityByName())
            {
                movementPhase.Enqueue(phase);
            }
        }

        public void OnEnable()
        {
            Debug.Log("Enable Movement");
            if (_setMovementPhaseEvent != null) _setMovementPhaseEvent.OnEventRaised += SetMovementPhase;
        }

        public void OnDisable()
        {
            Debug.Log("Disable Movement");
            if (_setMovementPhaseEvent != null) _setMovementPhaseEvent.OnEventRaised -= SetMovementPhase;
        }

        public void SetMovementPhase()
        {
            ClearPhase();
            Debug.Log("movementphase: " + movementPhase.Peek());
            MovementPhaseProcessor.HandlePhase(movementPhase.Peek());

            if (_gameStats.ActiveUnit != null) _inputReader.ActivateEvent += NextPhase;
            if (MovementPhaseProcessor.Next(movementPhase.Peek())) NextPhase();
        }

        public override void ClearPhase()
        {
            MovementPhaseProcessor.ClearPhase(movementPhase.Peek());
            _inputReader.ActivateEvent -= NextPhase;
        }
        public void NextPhase()
        {
            movementPhase.Enqueue(movementPhase.Dequeue());
            SetMovementPhase();
        }

        [Serializable]
        public class Settings
        {
            public bool Enabled;
        }
    }
}