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
    /// this script manages the shooting phase, dependend on what subphase its currently in.
    /// When SetShootingPhase is called, it triggers the shooting phase processor.
    /// When the subphase "shooting" is enabled, it triggers the shooting sub phase processor.
    /// </summary>

    public class ShootingPhaseManager : PhaseManagerBase
    {
        public ShootingPhaseManager() { }

        // Gameplay
        private InputReader _inputReader;
        private ShootingSubPhaseManager _shootingSubPhaseManager;
        private GameStatsSO _gameStats;
        private Settings _settings;

        //Events
        private BattleroundEventChannelSO _setShootingPhaseEvent;

        //Enums
        private Queue<ShootingPhase> shootingPhase = new Queue<ShootingPhase>();
        public override GamePhase SubEvents => GamePhase.ShootingPhase;
        public InputReader InputReader { get => _inputReader; }

        private void Awake()
        {
            EnqueueShootingPhase();
        }
        private void Start()
        {
            enabled = _settings.Enabled;
        }

        [Inject]
        public void Construct(
            Settings settings,
            GameStatsSO gameStats,
            BattleroundEventChannelSO battleroundEventChannel,
            ShootingSubPhaseManager shootingSubPhaseManager,
            InputReader inputReader)
        {
            _gameStats = gameStats;
            _settings = settings;
            _setShootingPhaseEvent = battleroundEventChannel;
            _shootingSubPhaseManager = shootingSubPhaseManager;
            _inputReader = inputReader;
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
            if (_setShootingPhaseEvent != null) _setShootingPhaseEvent.OnEventRaised += SetShootingPhase;

        }
        public void OnDisable()
        {
            Debug.Log("Disable Shooting");
            if (_setShootingPhaseEvent != null) _setShootingPhaseEvent.OnEventRaised -= SetShootingPhase;
        }

        public void SetShootingPhase()
        {
            ClearPhase();
            ShootingPhaseProcessor.HandlePhase(shootingPhase.Peek());
            //Debug.Log("ShootingPhaseManager");
            if (_gameStats.ActiveUnit != null) InputReader.ActivateEvent += NextPhase;
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

        [Serializable]
        public class Settings
        {
            public bool Enabled;
        }
    }
}

