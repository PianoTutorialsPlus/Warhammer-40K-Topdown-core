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


        // Lists
        //[SerializeField] private List<CalculationBaseSO> calculations = new List<CalculationBaseSO>();
        

        //Enums
        private Queue<ShootingPhase> shootingPhase = new Queue<ShootingPhase>();

        public override GamePhase SubEvents => GamePhase.ShootingPhase;

        public IPhase BattleroundEvents { get => _battleroundEvents;/* set => _battleroundEvents = value;*/ }
        public InputReader InputReader { get => _inputReader; /*set => _inputReader = value;*/ }
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
            //shootingPhase = ShootingPhase.Selection;
            //shootingSubPhase = ShootingSubEvents.SelectEnemy;

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

            if (shootingPhase.Peek() == ShootingPhase.Shoot)
            {
                //_inputReader.ActivateEvent += HandleShooting;
                _inputReader.ExecuteEvent += Wait;
            }
            if (ShootingPhaseProcessor.Next(shootingPhase.Peek())) NextPhase();
        }

        public override void ClearPhase()
        {
            ShootingPhaseProcessor.ClearPhase(shootingPhase.Peek());
            InputReader.ActivateEvent -= NextPhase;
            //InputReader.ActivateEvent -= HandleShooting;
            InputReader.ExecuteEvent -= Wait;
        }
        public void NextPhase()
        {
            shootingPhase.Enqueue(shootingPhase.Dequeue());
            SetShootingPhase(GameStats);
        }
        //private void HandleShooting()
        //{
        //    Debug.Log(shootingSubPhase.Peek());
        //    //CalculationBaseSO calculation = ShootingSubPhaseProcessor.SetCalculation(calculations, shootingSubPhase);
        //    //ICalculation calculation = ShootingSubPhaseProcessor.SetCalculation(shootingSubPhase.Peek()); ;
        //    ShootingSubPhaseProcessor.SetCalculation(shootingSubPhase.Peek());

        //    //if (calculation == nul89l) return;
        //    ShootingSubPhaseProcessor.HandleShooting(shootingSubPhase.Peek());
        //}
        //private void ProcessResult(ShootingSubEvents diceEvent, List<int> result)
        //{
        //    if (shootingSubPhase.Peek() == diceEvent) shootingSubPhase.Enqueue(shootingSubPhase.Dequeue());

        //    if (Parameter != null) Debug.Log(shootingSubPhase.Peek());

        //    CheckNullValues(result);
        //}
        private void CheckNullValues(List<int> values)
        {
            if (values == null || values.Count == 0)
            {
                NextPhase();
            }
        }
        private void Wait()
        {
            StartCoroutine(WaitForButtonCoroutine());
        }
        public IEnumerator WaitForButtonCoroutine()
        {
            //Debug.Log("Waiting");
            yield return null;
            //HandleShooting();
        }
    }
}

//[SerializeField] private CalculateHits calculateHits;
//[SerializeField] private CalculationBaseSO calculateHits;
//[SerializeField] private CalculationBaseSO calculateWounds;
//[SerializeField] private CalculationBaseSO calculateSaves;
//[SerializeField] private DealDamageSO dealDamage;

//public List<int> hits = new List<int>();
//public List<int> wounds = new List<int>();
//public List<int> saves = new List<int>();

// ---------------------------------------------------------------

//public void ResetUnits(GameStatsSO gameStats)
//{
//    foreach (Unit child in gameStats.activePlayer._playerUnits) child.ResetData();
//}
//public void SetShootingPhase(GameStatsSO gameStats)
//{

//    if (_gameStats.phase != GamePhase.ShootingPhase) return;

//    //Debug.Log("Shooting Setup");
//    // Debug.Log(gameStats.activePlayer.name);
//    ClearShootingPhase(gameStats);

//    //_inputReader.activateEvent += NextPhase;
//    //switch (_gameStats.shootingSubPhase)
//    //{
//    //    case (ShootingPhase.Selection):
//    //        { 
//    //            foreach (Unit child in gameStats.activePlayer._playerUnits)
//    //            {
//    //                //if (child.done)
//    //                //{
//    //                //    _battleroundEvents.FillMethods(child, false, true, false, false);
//    //                //    continue;
//    //                //}
//    //                if (child == gameStats.activeUnit)
//    //                {
//    //                    //_battleroundEvents.FillMethods(child, true, true, true, true);
//    //                    _inputReader.activateEvent += NextPhase;

//    //                }
//    //                else
//    //                {
//    //                    //Debug.Log("Element");
//    //                    //_battleroundEvents.FillMethods(child, false, true, true, true);
//    //                }
//    //            }
//    //            //foreach (Unit child in gameStats.enemyPlayer._playerUnits) _battleroundEvents.FillMethods(child, false, true, true, false);
//    //            break;
//    //        }
//    //    case (ShootingPhase.Shoot):
//    //        {
//    //            Debug.Log("Shoot");
//    //            _inputReader.activateEvent += HandleShooting;
//    //            _inputReader.ExecuteEvent += Wait;
//    //            break;
//    //        }
//    //}

//}

//private void ProcessResult(ShootingSubEvents diceEvent, List<int> result)
//{
//    List<int> parameter = new List<int>();
//    //Debug.Log("Process Result");
//    switch (diceEvent)
//    {
//        case (ShootingSubEvents.Hit):
//            {
//                Debug.Log("Process Hit Result: " + result.Count);
//                hits = result;
//                shootingSubPhase = ShootingSubEvents.Wound;
//                CheckNullValues(hits);
//                break;
//            }
//        case (ShootingSubEvents.Shoot):
//            {
//                Debug.Log("Process Wound Result:" + result.Count);
//                wounds = result;
//                shootingSubPhase = ShootingSubEvents.Save;
//                CheckNullValues(wounds);
//                break;
//            }
//        case (ShootingSubEvents.Save):
//            {
//                Debug.Log("Process Save Result: " + result.Count);
//                saves = result;
//                shootingSubPhase = ShootingSubEvents.Damage;
//                CheckNullValues(saves);
//                break;
//            }
//    }

//    Debug.Log(shootingSubPhase);
//    HandleShooting();
//}
//public void FillMethods(Unit child, bool displayInteraction, bool resetInteraction, bool displayInfo, bool connectIndicator)
//{
//    if (displayInteraction) child.onPointerEnter += DisplayInteractionUI;
//    else child.onPointerEnter -= DisplayInteractionUI;

//    if (resetInteraction) child.onPointerExit += ResetInteraction;
//    else child.onPointerExit -= ResetInteraction;

//    if (displayInfo) child.onPointerEnterInfo += DisplayInfoUI;
//    else child.onPointerEnterInfo -= DisplayInfoUI;

//    if (connectIndicator) child.onTapDownAction += ConnectIndicator;
//    else child.onTapDownAction -= ConnectIndicator;
//}

//private void DisplayInfoUI(Unit unit)
//{
//    if (_gameStats.activePlayer._playerUnits[0].tag == unit.tag)
//        _toggleInfoUI.RaiseEvent(true, unit);
//    if (_gameStats.enemyPlayer._playerUnits[0].tag == unit.tag)
//        _toggleEnemyInfoUI.RaiseEvent(true, unit);
//}
//private void DisplayInteractionUI()
//{
//    //Raise event to display UI
//    _toggleInteractionUI.RaiseEvent(true, InteractionType.Activate);
//}

//private void ResetInteraction(Unit unit)
//{
//    if (!unit.selected) _toggleInteractionUI.RaiseEvent(false, InteractionType.None);
//    if (!unit.activated) _toggleInfoUI.RaiseEvent(false, unit);
//    _toggleEnemyInfoUI.RaiseEvent(false, unit);
//}
//private void ConnectIndicator(Unit unit)
//{
//    //Debug.Log("Connect");
//    SetShootingPhase(_gameStats);
//    _toggleIndicatorConnectionUI.RaiseEvent(true, unit);

//}

//private void HandleShooting()
//{


//switch (shootingSubPhase)
//{
//    case (ShootingSubEvents.SelectEnemy):
//        {
//            Debug.Log("Enemy Selection");
//            _gameStats.enemyUnit = _gameStats.enemyPlayer._playerUnits[0];
//            shootingSubPhase = ShootingSubEvents.Hit;
//            break;
//        }
//    //case (ShootingSubEvents.Hit):
//    //    {
//    //        calculateHits.Action(_gameStats);
//    //        //shootingSubPhase = ShootingSubEvents.Wound;
//    //        //CheckNullValues(hits);
//    //        break;
//    //    }
//    //case (ShootingSubEvents.Wound):
//    //    {
//    //        Debug.Log("Wounds case");
//    //        calculateWounds.Action(hits, _gameStats);
//    //        //shootingSubPhase = ShootingSubEvents.Save;
//    //        //CheckNullValues(wounds);
//    //        break;
//    //    }
//    //case (ShootingSubEvents.Save):
//    //    {
//    //        calculateSaves.Action(wounds, _gameStats);
//    //        //shootingSubPhase = ShootingSubEvents.Damage;
//    //        //CheckNullValues(saves);
//    //        break;
//    //    }
//    case (ShootingSubEvents.Damage):
//        {
//            dealDamage.DealDamage(saves, _gameStats);
//            CheckNullValues(null);
//            break;
//        }
//}
//}
