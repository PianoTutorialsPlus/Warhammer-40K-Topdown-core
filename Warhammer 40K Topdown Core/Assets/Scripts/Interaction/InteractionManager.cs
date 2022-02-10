using System.Collections.Generic;
using UnityEngine;


public enum InteractionType { None = 0, Activate, ShowStats }
public enum GamePhase { None = 0, MovementPhase, ShootingPhase }
public enum MovementPhase { None = 0, Selection, Move, Next }
public enum ShootingPhase { None = 0, Selection, Shoot, Next }
//public enum DiceEvent { None = 0, HitEvent,ShootEvent, SaveEvent}



public class InteractionManager : MonoBehaviour
{
    // public PlayerSO _activePlayer;
    // public PlayerSO _enemyPlayer;
    public PlayerSO _player1;
    public PlayerSO _player2;

    //public PhaseSO _phase; //Initialization
    //public TurnSO _turn; //Initialization
    public GameStatsSO _gameStats; //Initialization

    //[SerializeField] PhaseEventChannelSO SetPhaseEvent = default;
    [SerializeField] GameStatsEventChannelSO SetPhaseEvent = default;
    [SerializeField] TurnEventChannelSO SetTurnEvent = default;
    [SerializeField] private GameinfoUIEventChannelSO _toggleGameinfoUI = default;
    [SerializeField] BattleroundEventChannelSO _toggleBattleRounds = default;

    [SerializeField] List<PhaseManagerBase> gamePhases = new List<PhaseManagerBase>();
    //[SerializeField] MovementPhaseManager movementPhase;
    //[SerializeField] ShootingPhaseManager shootingPhase;

    GamePhase _gamePhase;

    private void Start()
    {
        //EnableMovementPhase();
        _toggleBattleRounds.RaiseEvent(_gameStats); //Initialization    
    }

    private void OnEnable()
    {
        //Initialization
        //_gameStats.phase = GamePhase.MovementPhase;
        _gamePhase = GamePhase.MovementPhase;
        _gameStats.movementSubPhase = MovementPhase.Selection;
        _gameStats.shootingSubPhase = ShootingPhase.Selection;
        _gameStats.turn = 1;
        _gameStats.activeUnit = null;
        _gameStats.enemyUnit = null;

        // StartCoroutine(Sets());
        _gameStats.activePlayer = _player1;
        _gameStats.enemyPlayer = _player2;

        //_gameStats.activePlayer._playerUnits = _player1._playerUnits;
        //_gameStats.enemyPlayer._playerUnits = _player2._playerUnits;



        if (SetPhaseEvent != null) SetPhaseEvent.OnEventRaised += SetPhase;

        //_toggleBattleRounds.RaiseEvent(_gameStats); //Initialization
        //_toggleGameinfoUI.RaiseEvent(true, _activePlayer._playerUnits[0],_phase,_turn);
        _toggleGameinfoUI.RaiseEvent(true, _gameStats);
    }

    public void SetPhase(GameStatsSO gameStats)
    {
        GamePhaseProcessor.ResetPhase(gameStats, gamePhases, _gamePhase);
        _gamePhase = GamePhaseProcessor.SetPhase(gameStats, _gamePhase);
        //gameStats.phase = _gamePhase;
        bool endOfPlayerTurn = GamePhaseProcessor.HandlePhase(gamePhases,_gamePhase);

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
        Debug.Log("Toggle Player");
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
