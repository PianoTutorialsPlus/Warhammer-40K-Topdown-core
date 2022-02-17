using System.Collections.Generic;
using UnityEngine;

// Enum initialization
public enum InteractionType { None = 0, Activate, ShowStats }
public enum GamePhase { None = 0, MovementPhase, ShootingPhase }
public enum MovementPhase { None = 0, Selection, Move, Next }
public enum ShootingPhase { None = 0, Selection, Shoot, Next }
public enum ShootingSubEvents { None = 0, SelectEnemy, Hit, Wound, Save, Damage }

/// <summary>
/// This script takes care of the interactions. It communicates with the UI Manager and triggers the GamePhase Processor.
/// When "End Turn" button is pressed, a new gamephase is invoked.
/// </summary>

public class InteractionManager : MonoBehaviour
{
    //Initialization
    // Gameplay
    [SerializeField] private PlayerSO _player1;
    [SerializeField] private PlayerSO _player2;
    [SerializeField] private GameStatsSO _gameStats;

    // Events
    [SerializeField] private GameStatsEventChannelSO SetPhaseEvent = default;
    [SerializeField] private GameinfoUIEventChannelSO _toggleGameinfoUI = default;
    [SerializeField] private BattleroundEventChannelSO _toggleBattleRounds = default;

    // Lists
    [SerializeField] private List<PhaseManagerBase> gamePhases = new List<PhaseManagerBase>();

    //Enums
    GamePhase _gamePhase;

    private void Start()
    {
        _toggleBattleRounds.RaiseEvent(_gameStats); //Initialization    
    }

    private void OnEnable()
    {
        //Initialization
        _gamePhase = GamePhase.MovementPhase;
        _gameStats.turn = 1;
        _gameStats.activeUnit = null;
        _gameStats.enemyUnit = null;
        _gameStats.activePlayer = _player1;
        _gameStats.enemyPlayer = _player2;
        GamePhaseProcessor.HandlePhase(gamePhases, _gamePhase);

        if (SetPhaseEvent != null) SetPhaseEvent.OnEventRaised += SetPhase;

        _toggleGameinfoUI.RaiseEvent(true, _gameStats);
    }

    public void SetPhase(GameStatsSO gameStats)
    {
        GamePhaseProcessor.ResetPhase(gameStats, gamePhases, _gamePhase);
        _gamePhase = GamePhaseProcessor.SetPhase(gameStats, _gamePhase);
        gameStats.phase = _gamePhase;
        bool endOfPlayerTurn = GamePhaseProcessor.HandlePhase(gamePhases, _gamePhase);

        if (endOfPlayerTurn)
        {
            TogglePlayers();
            if (gameStats.activePlayer == _player1) gameStats.turn += 1;
        }
        _toggleGameinfoUI.RaiseEvent(true, gameStats);
        _toggleBattleRounds.RaiseEvent(gameStats);
    }

    public void TogglePlayers()
    {
        if (_gameStats.activePlayer == _player1)
        {
            _gameStats.activePlayer = _player2;
            _gameStats.enemyPlayer = _player1;
        }
        else
        {
            _gameStats.activePlayer = _player1;
            _gameStats.enemyPlayer = _player2;
        }
    }

    // public PlayerSO _activePlayer;
    // public PlayerSO _enemyPlayer;
    //public PhaseSO _phase; //Initialization
    //public TurnSO _turn; //Initialization
    //[SerializeField] MovementPhaseManager movementPhase;
    //[SerializeField] ShootingPhaseManager shootingPhase;
    //[SerializeField] TurnEventChannelSO SetTurnEvent = default;
    //[SerializeField] PhaseEventChannelSO SetPhaseEvent = default;

    // ---------------------------------------------------------------------

    //private void OnEnable()
    //{
    //    //Initialization
    //    //_gameStats.phase = GamePhase.MovementPhase;
    //    _gamePhase = GamePhase.MovementPhase;
    //    GamePhaseProcessor.HandlePhase(gamePhases, _gamePhase);

    //    //_gameStats.movementSubPhase = MovementPhase.Selection;
    //    //_gameStats.shootingSubPhase = ShootingPhase.Selection;
    //    _gameStats.turn = 1;
    //    _gameStats.activeUnit = null;
    //    _gameStats.enemyUnit = null;

    //    // StartCoroutine(Sets());
    //    _gameStats.activePlayer = _player1;
    //    _gameStats.enemyPlayer = _player2;

    //    //_gameStats.activePlayer._playerUnits = _player1._playerUnits;
    //    //_gameStats.enemyPlayer._playerUnits = _player2._playerUnits;



    //    if (SetPhaseEvent != null) SetPhaseEvent.OnEventRaised += SetPhase;

    //    //_toggleBattleRounds.RaiseEvent(_gameStats); //Initialization
    //    //_toggleGameinfoUI.RaiseEvent(true, _activePlayer._playerUnits[0],_phase,_turn);
    //    _toggleGameinfoUI.RaiseEvent(true, _gameStats);
    //}

    //private void EnableMovementPhase()
    //{
    //    shootingPhase.enabled = false;
    //    movementPhase.enabled = true; 
    //}

    //private void EnableShootingPhase()
    //{
    //    movementPhase.enabled = false;
    //    shootingPhase.enabled = true;
    //}


    //public void SetPhase(GameStatsSO gameStats)
    //{

    //    if (gameStats.phase == GamePhase.MovementPhase)
    //    {
    //        gameStats.phase = GamePhase.ShootingPhase;
    //        EnableShootingPhase();
    //        movementPhase.ClearMovementPhase(gameStats);
    //        movementPhase.ResetUnits(gameStats);

    //    }
    //    else if (gameStats.phase == GamePhase.ShootingPhase)
    //    {
    //        gameStats.phase = GamePhase.MovementPhase;
    //        //gameStats.movementSubPhase = MovementPhase.Selection;
    //        EnableMovementPhase();
    //        shootingPhase.ClearShootingPhase(gameStats);
    //        shootingPhase.ResetUnits(gameStats);

    //        TogglePlayers();
    //        if (gameStats.activePlayer == _player1) gameStats.turn += 1;
    //    }
    //    _toggleGameinfoUI.RaiseEvent(true, gameStats);
    //    _toggleBattleRounds.RaiseEvent(gameStats);
    //}
    //public IEnumerator Sets()
    //{
    //    yield return new WaitForEndOfFrame();
    //    //InteractionType ongoingInteractionType = InteractionType.Activate;

    //    foreach (GameObject child in _player1._player1Units)
    //    {
    //        //_ongoingInteractions.AddFirst(new Interaction(ongoingInteractionType, child));
    //        child.GetComponent<Unit>().onPointerEnter += DisplayInteractionUI;
    //        child.GetComponent<Unit>().onPointerExit += ResetInteraction;
    //    }
    //}

    //public void SetPhase(PhaseSO phase)
    //{

    //    if (phase.phase == GamePhase.MovementPhase)
    //    {
    //        EnableShootingPhase();
    //        phase.phase = GamePhase.ShootingPhase;
    //        if (SetTurnEvent != null && _activePlayer._playerUnits == _player2._playerUnits) SetTurnEvent.OnEventRaised += SetTurn;
    //    }
    //    else if (phase.phase == GamePhase.ShootingPhase)
    //    {
    //        TogglePlayers();
    //        EnableMovementPhase();
    //        phase.phase = GamePhase.MovementPhase;
    //        if (SetTurnEvent != null) SetTurnEvent.OnEventRaised -= SetTurn;
    //    }
    //    _toggleGameinfoUI.RaiseEvent(true,_activePlayer._playerUnits[0],phase, _turn);
    //}

    //public void SetTurn(TurnSO turn)
    //{
    //    turn.turn += 1;
    //}

    //public void TogglePlayers()
    //{
    //    if(_gameStats.activePlayer._playerUnits == _player1._playerUnits)
    //    {
    //        _gameStats.activePlayer._playerUnits = _player2._playerUnits;
    //        _gameStats.enemyPlayer._playerUnits = _player1._playerUnits;
    //    }
    //    else
    //    {
    //        _gameStats.activePlayer._playerUnits = _player1._playerUnits;
    //        _gameStats.enemyPlayer._playerUnits = _player2._playerUnits;
    //    }
    //}
}
