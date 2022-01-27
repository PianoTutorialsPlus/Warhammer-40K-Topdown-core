using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPhaseManager : MonoBehaviour
{
    public PlayerSO _player1;
    public PlayerSO _player2;
    public GameStatsSO _gameStats;
    //public DataTablesSO dataTables;
    public InputReader _inputReader;

    [SerializeField] private CalculateHitsSO calculateHits;
    [SerializeField] private CalculateWoundsSO calculateWounds;
    [SerializeField] private CalculateSaverolesSO calculateSaves;
    [SerializeField] private DealDamageSO dealDamage;

    public List<int> hits = new List<int>();
    public List<int> wounds = new List<int>();
    public List<int> saves = new List<int>();
    enum ShootingSubEvents { none = 0, SelectEnemy, Shoot, Hit, Wound, Save, Damage }
    ShootingSubEvents shootingSubPhase;

    [SerializeField] private BattleroundEventChannelSO SetShootingPhaseEvent;

    //UI event
    [SerializeField] private InteractionUIEventChannelSO _toggleInteractionUI = default;
    [SerializeField] private InfoUIEventChannelSO _toggleInfoUI = default;
    [SerializeField] private InfoUIEventChannelSO _toggleEnemyInfoUI = default;
    [SerializeField] private IndicatorUIEventChannelSO _toggleIndicatorConnectionUI = default;

    public void OnEnable()
    {
        if (_gameStats.phase == GamePhase.ShootingPhase)
        {
            //Debug.Log("Enable");
            if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised += SetShootingPhase;
            if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised -= ClearShootingPhase;
        }
    }

    public void OnDisable()
    {
        //Debug.Log("Disable");
        if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised -= SetShootingPhase;
        if (SetShootingPhaseEvent != null) SetShootingPhaseEvent.OnEventRaised += ClearShootingPhase;
    }

    public void SetShootingPhase(GameStatsSO gameStats)
    {
        // Reset
        ClearShootingPhase(gameStats);

        switch (_gameStats.shootingSubPhase)
        {
            case (ShootingPhase.Selection):
                {
                    foreach (Unit child in gameStats.activePlayer._playerUnits)
                    {
                        if (child.done)
                        {
                            FillMethods(child, false, true, false, false);
                            continue;
                        }
                        if (child == gameStats.activeUnit)
                        {
                            FillMethods(child, true, true, true, true);
                            _inputReader.activateEvent += NextPhase;
                            //Debug.Log("Element");
                        }
                        else
                        {
                            FillMethods(child, false, true, true, true);
                        }
                    }
                    foreach (Unit child in gameStats.enemyPlayer._playerUnits) FillMethods(child, false, true, true, false);
                    break;
                }
            case (ShootingPhase.Shoot):
                {
                    Debug.Log("Shoot");
                    _inputReader.activateEvent += HandleShooting;
                    _inputReader.ExecuteEvent += Wait;
                    break;
                }
        }
    }

    public void ClearShootingPhase(GameStatsSO gameStats)
    {
        Debug.Log("Reset");
        foreach (Unit child in gameStats.activePlayer._playerUnits) FillMethods(child, false, false, false, false);
        foreach (Unit child in gameStats.enemyPlayer._playerUnits) FillMethods(child, false, false, false, false);
        _inputReader.activateEvent -= NextPhase;
        _inputReader.activateEvent -= HandleShooting;
        _inputReader.ExecuteEvent -= Wait;
        shootingSubPhase = ShootingSubEvents.SelectEnemy;
    }

    public void FillMethods(Unit child, bool displayInteraction, bool resetInteraction, bool displayInfo, bool connectIndicator)
    {
        if (displayInteraction) child.onPointerEnter += DisplayInteractionUI;
        else child.onPointerEnter -= DisplayInteractionUI;

        if (resetInteraction) child.onPointerExit += ResetInteraction;
        else child.onPointerExit -= ResetInteraction;

        if (displayInfo) child.onPointerEnterInfo += DisplayInfoUI;
        else child.onPointerEnterInfo -= DisplayInfoUI;

        if (connectIndicator) child.onTapDownAction += ConnectIndicator;
        else child.onTapDownAction -= ConnectIndicator;
    }

    private void DisplayInfoUI(Unit unit)
    {
        if (_gameStats.activePlayer._playerUnits[0].tag == unit.tag)
            _toggleInfoUI.RaiseEvent(true, unit);
        if (_gameStats.enemyPlayer._playerUnits[0].tag == unit.tag)
            _toggleEnemyInfoUI.RaiseEvent(true, unit);
    }
    private void DisplayInteractionUI()
    {
        //Raise event to display UI
        _toggleInteractionUI.RaiseEvent(true, InteractionType.Activate);
    }

    private void ResetInteraction(Unit unit)
    {
        if (!unit.selected) _toggleInteractionUI.RaiseEvent(false, InteractionType.None);
        if (!unit.activated) _toggleInfoUI.RaiseEvent(false, unit);
        _toggleEnemyInfoUI.RaiseEvent(false, unit);
    }
    private void ConnectIndicator(Unit unit)
    {
        //Debug.Log("Connect");
        SetShootingPhase(_gameStats);
        _toggleIndicatorConnectionUI.RaiseEvent(true, unit);

    }

    private void NextPhase()
    {
        
        //Debug.Log("Phase");
        _gameStats.shootingSubPhase = ShootingPhase.Shoot;
        SetShootingPhase(_gameStats);
    }

    private void HandleShooting()
    {
        switch (shootingSubPhase)
        {
            case (ShootingSubEvents.SelectEnemy):
                {
                    Debug.Log("Enemy Selection");
                    _gameStats.enemyUnit = _gameStats.enemyPlayer._playerUnits[0];
                    shootingSubPhase = ShootingSubEvents.Hit;
                    break;
                }
            case (ShootingSubEvents.Hit):
                {
                    hits = calculateHits.HandleToHit(_gameStats);
                    shootingSubPhase = ShootingSubEvents.Wound;
                    CheckNullValues(hits);
                    break;
                }
            case (ShootingSubEvents.Wound):
                {
                    wounds = calculateWounds.HandleToWound(hits, _gameStats);
                    shootingSubPhase = ShootingSubEvents.Save;
                    CheckNullValues(wounds);
                    break;
                }
            case (ShootingSubEvents.Save):
                {
                    saves = calculateSaves.HandleSaveRoles(wounds, _gameStats);
                    shootingSubPhase = ShootingSubEvents.Damage;
                    CheckNullValues(saves);
                    break;
                }
            case (ShootingSubEvents.Damage):
                {
                    dealDamage.DealDamage(saves, _gameStats);
                    CheckNullValues(null);
                    break;
                }
        }

    }

    private void CheckNullValues(List<int> values)
    {
        if (values == null)
        {
            _gameStats.shootingSubPhase = ShootingPhase.Selection;
            SetShootingPhase(_gameStats);
        }
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
