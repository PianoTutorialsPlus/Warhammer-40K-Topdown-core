using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;
using WH40K.UI;

namespace WH40K.GameMechanics
{

    /// <summary>
    /// this script manages the movement phase. 
    /// When SetMovementPhase is called, it triggers the movement phase processor.
    /// The manager handles the movement phase, dependend on what subphase its currently in.
    /// </summary>

    public class MovementPhaseManager : PhaseManagerBase
    {
        // Gameplay
        [SerializeField] private InputReader _inputReader;

        // Events
        [SerializeField] private BattleroundEventChannelSO SetMovementPhaseEvent;
        

        // Enums
        private Queue<MovementPhase> movementPhase = new Queue<MovementPhase>();

        public override GamePhase SubEvents => GamePhase.MovementPhase;

        
        public InputReader InputReader { get => _inputReader;  }
        

        private void Awake()
        {
            enabled = false;
            
            EnqueueMovementPhase();
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
            if (SetMovementPhaseEvent != null) SetMovementPhaseEvent.OnEventRaised += SetMovementPhase;
        }

        public void OnDisable()
        {
            Debug.Log("Disable Movement");
            if (SetMovementPhaseEvent != null) SetMovementPhaseEvent.OnEventRaised -= SetMovementPhase;
        }

        public void SetMovementPhase()
        {
            ClearPhase();
            Debug.Log("movementphase: " + movementPhase.Peek());
            MovementPhaseProcessor.HandlePhase(movementPhase.Peek());

            if (GameStats.ActiveUnit != null) InputReader.ActivateEvent += NextPhase;
            if (MovementPhaseProcessor.Next(movementPhase.Peek())) NextPhase();
        }

        public override void ClearPhase()
        {
            MovementPhaseProcessor.ClearPhase(movementPhase.Peek());
            InputReader.ActivateEvent -= NextPhase;
        }

        public void NextPhase()
        {
            movementPhase.Enqueue(movementPhase.Dequeue());
            SetMovementPhase();
        }
    }
}