using System;
using System.Collections.Generic;
using UnityEngine;
using WH40K.Core;
using WH40K.EventChannels;
using WH40K.InputEvents;
using Zenject;

namespace WH40K.GamePhaseEvents
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

        // Events
        private BattleroundEventChannelSO _setMovementPhaseEvent;
        private Settings _settings;

        // Enums
        private Queue<MovementPhase> movementPhase = new Queue<MovementPhase>();

        public override GamePhase SubEvents => GamePhase.MovementPhase;

        [Inject]
        public void Construct(
            BattleroundEventChannelSO battleroundEventChannel,
            Settings settings,
            InputReader inputReader)
        {
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

            if (GameStats.ActiveUnit != null) _inputReader.ActivateEvent += NextPhase;
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