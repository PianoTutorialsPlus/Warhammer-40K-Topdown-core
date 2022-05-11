using System.Collections.Generic;
using UnityEngine;
using WH40K.Core;
using WH40K.EventChannels;
using WH40K.InputEvents;

namespace WH40K.GamePhaseEvents
{

    /// <summary>
    /// this script manages the shooting phase, dependend on what subphase its currently in.
    /// When SetShootingPhase is called, it triggers the shooting phase processor.
    /// When the subphase "shooting" is enabled, it triggers the shooting sub phase processor.
    /// </summary>

    public class ShootingPhaseManager : PhaseManagerBase
    {
        public ShootingPhaseManager() { }

        // Gameplay
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private ShootingSubPhaseManager _shootingSubPhaseManager;

        //Events
        [SerializeField] private BattleroundEventChannelSO SetShootingPhaseEvent;

        //Enums
        private Queue<ShootingPhase> shootingPhase = new Queue<ShootingPhase>();
        public override GamePhase SubEvents => GamePhase.ShootingPhase;
        public InputReader InputReader { get => _inputReader; }

        private void Awake()
        {
            enabled = false;
            EnqueueShootingPhase();
        }
        private void EnqueueShootingPhase()
        {
            foreach (ShootingPhase phase in ShootingPhaseProcessor.GetAbilityByName())
            {
                shootingPhase.Enqueue(phase);
            }
        }

        public void OnEnable()
        {
            Debug.Log("Enable Shooting");
            if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised += SetShootingPhase;

        }
        public void OnDisable()
        {
            Debug.Log("Disable Shooting");
            if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised -= SetShootingPhase;
        }

        public void SetShootingPhase()
        {
            ClearPhase();
            ShootingPhaseProcessor.HandlePhase(shootingPhase.Peek());
            //Debug.Log("ShootingPhaseManager");
            if (GameStats.ActiveUnit != null) InputReader.ActivateEvent += NextPhase;
            if (shootingPhase.Peek() == ShootingPhase.Shoot) _shootingSubPhaseManager.enabled = true;
            if (ShootingPhaseProcessor.Next(shootingPhase.Peek())) NextPhase();
        }
        public override void ClearPhase()
        {
            ShootingPhaseProcessor.ClearPhase(shootingPhase.Peek());
            InputReader.ActivateEvent -= NextPhase;
            _shootingSubPhaseManager.enabled = false;
        }
        public void NextPhase()
        {
            shootingPhase.Enqueue(shootingPhase.Dequeue());
            SetShootingPhase();
        }
    }
}

