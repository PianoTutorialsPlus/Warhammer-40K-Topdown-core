using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPhaseManager : PhaseManagerBase
{
    public GameStatsSO _gameStats;
    public InputReader _inputReader;
    public BattleRoundsSO _battleroundEvents;


    [SerializeField] private List<CalculationBaseSO> calculations = new List<CalculationBaseSO>();

    //[SerializeField] private CalculateHits calculateHits;
    //[SerializeField] private CalculationBaseSO calculateHits;
    //[SerializeField] private CalculationBaseSO calculateWounds;
    //[SerializeField] private CalculationBaseSO calculateSaves;
    //[SerializeField] private DealDamageSO dealDamage;

    //public List<int> hits = new List<int>();
    //public List<int> wounds = new List<int>();
    //public List<int> saves = new List<int>();
    public List<int> parameter = new List<int>();
    

    ShootingSubEvents shootingSubPhase;
    ShootingPhase _shootingPhase;

    [SerializeField] private BattleroundEventChannelSO SetShootingPhaseEvent;

    public RollTheDiceSO diceRollingResult;

    public void OnEnable()
    {
        //Debug.Log(_gameStats.phase);
        //if (_gameStats.phase == GamePhase.ShootingPhase)
        //{
        Debug.Log("Enable Shooting");
        if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised += SetShootingPhase;
        if (diceRollingResult != null) diceRollingResult.OnEventRaised += ProcessResult;

        //if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised += ResetUnits;
        //if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised -= ClearShootingPhase;
        //}
    }

    public void OnDisable()
    {
        //if (_gameStats.phase == GamePhase.MovementPhase)
        //{
            Debug.Log("Disable Shooting");
            if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised -= SetShootingPhase;
            //if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised += ResetUnits;
            //if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised += ClearShootingPhase;
        //}
    }

    public void SetShootingPhase(GameStatsSO gameStats)
    {

        //if (_gameStats.phase != GamePhase.ShootingPhase) return;

        //Debug.Log("Shooting Setup");
        // Debug.Log(gameStats.activePlayer.name);
        ClearShootingPhase(gameStats);
        _shootingPhase = _gameStats.shootingSubPhase;
        bool selection = ShootingPhaseProcessor.HandlePhase(gameStats,_battleroundEvents, _shootingPhase);
        bool shooting = ShootingPhaseProcessor.HandlePhase(_shootingPhase);
        
        if(selection) _inputReader.activateEvent += NextPhase;
        
        if (shooting)
        {
            _inputReader.activateEvent += HandleShooting;
            _inputReader.ExecuteEvent += Wait;
        }
    }

    public void ClearShootingPhase(GameStatsSO gameStats)
    {
        //Debug.Log("Clear Shooting");
        foreach (Unit child in gameStats.activePlayer._playerUnits) _battleroundEvents.FillMethods(child, false, false, false, false);
        foreach (Unit child in gameStats.enemyPlayer._playerUnits) _battleroundEvents.FillMethods(child, false, false, false, false);
        _inputReader.activateEvent -= NextPhase;
        _inputReader.activateEvent -= HandleShooting;
        _inputReader.ExecuteEvent -= Wait;
        shootingSubPhase = ShootingSubEvents.SelectEnemy;
    }

    private void NextPhase()
    {
        _gameStats.shootingSubPhase = ShootingPhaseProcessor.SetPhase(_shootingPhase);
        //Debug.Log("Phase");
        //_gameStats.shootingSubPhase = ShootingPhase.Shoot;
        SetShootingPhase(_gameStats);
    }

    private void HandleShooting()
    {
        Debug.Log("Handle Shooting: " + shootingSubPhase);
        CalculationBaseSO calculation = ShootingSubPhaseProcessor.SetCalculation(calculations, shootingSubPhase);      

        if (calculation == null) return;  
        ShootingSubPhaseProcessor.HandleShooting(parameter, calculation, _gameStats, shootingSubPhase);
    }

    private void ProcessResult(ShootingSubEvents diceEvent, List<int> result)
    {
        Debug.Log("Process Result");
        parameter = ShootingSubPhaseProcessor.ProcessResult(result, diceEvent);
        shootingSubPhase = ShootingSubPhaseProcessor.SetSubPhase(diceEvent);
        
        Debug.Log(shootingSubPhase);
        if(parameter != null)Debug.Log(parameter.Count);
        CheckNullValues(parameter);
    }

    private void CheckNullValues(List<int> values)
    {
        if (values == null || values.Count == 0)
        {
            _gameStats.shootingSubPhase = ShootingPhase.Selection;
            SetShootingPhase(_gameStats);
        }
    }

    public void ResetUnits(GameStatsSO gameStats)
    {
        foreach (Unit child in gameStats.activePlayer._playerUnits) child.ResetData();
    }

    private void Wait()
    {
        StartCoroutine(WaitForButton());
    }

    public IEnumerator WaitForButton()
    {
        Debug.Log("Waiting");
        yield return null;
        HandleShooting();
    }
}

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
