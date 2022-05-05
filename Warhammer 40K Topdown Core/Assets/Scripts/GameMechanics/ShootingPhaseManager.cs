using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;
using WH40K.UI;

namespace WH40K.GameMechanics
{

    /// <summary>
    /// this script manages the shooting phase, dependend on what subphase its currently in.
    /// When SetShootingPhase is called, it triggers the shooting phase processor.
    /// When the subphase "shooting" is enabled, it triggers the shooting sub phase processor.
    /// </summary>

    public class ShootingPhaseManager : PhaseManagerBase, IGamePhase
    {
        public ShootingPhaseManager() { }

        // Gameplay
        [SerializeField] private GameStatsSO _gameStats;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private ShootingSubPhaseManager _shootingSubPhaseManager;

        //Events
        [SerializeField] private BattleroundEventChannelSO SetShootingPhaseEvent;
        [SerializeField] private BattleRoundsSO _battleroundEvents;    

        //Enums
        private Queue<ShootingPhase> shootingPhase = new Queue<ShootingPhase>();

        public override GamePhase SubEvents => GamePhase.ShootingPhase;

        public IPhase BattleroundEvents { get => _battleroundEvents; }
        public InputReader InputReader { get => _inputReader; }
        public GameStatsSO GameStats { get => _gameStats; set => _gameStats = value; }

        private void Awake()
        {
            enabled = false;
            EnqueueShootingPhase();
            new ShootingPhaseProcessor(this);
        }
        private void EnqueueShootingPhase()
        {
            shootingPhase.Enqueue(ShootingPhase.Selection);
            shootingPhase.Enqueue(ShootingPhase.Shoot);
            shootingPhase.Enqueue(ShootingPhase.Next);
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

        public void SetShootingPhase(GameStatsSO gameStats)
        {
            ClearPhase();
            ShootingPhaseProcessor.HandlePhase(shootingPhase.Peek());

            if (gameStats.ActiveUnit != null) InputReader.ActivateEvent += NextPhase;
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
            SetShootingPhase(GameStats);
        }
    }
}

