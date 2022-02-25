using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Enum initialization
public enum InteractionType { None = 0, Activate, ShowStats }
public enum GamePhase { MovementPhase, ShootingPhase }
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
    [SerializeField] public GameStatsSO _gameStats;

    // Events
    [SerializeField] private GameStatsEventChannelSO SetPhaseEvent = default;
    [SerializeField] private GameinfoUIEventChannelSO _toggleGameinfoUI = default;
    [SerializeField] private BattleroundEventChannelSO _toggleBattleRounds = default;

    // Lists
    [SerializeField] private List<PhaseManagerBase> _gamePhases = new List<PhaseManagerBase>();

    // Dictionaries
    private static Dictionary<GamePhase, PhaseManagerBase> _gamePhaseManagers = new Dictionary<GamePhase, PhaseManagerBase>();

    //Enums
    GamePhase _gamePhase;
    //[SerializeField] private Queue<GamePhase> _gamePhase = new Queue<GamePhase>();

    public static bool _initializedManager;

    private void Start()
    {      
        _toggleBattleRounds.RaiseEvent(_gameStats); //Initialization    
    }

    private void OnEnable()
    {
        //Initialization
        _gamePhase = GamePhase.MovementPhase;

        
        //foreach(GamePhase phase in GamePhaseProcessor.GetAbilityByName())
        //{
        //    _gamePhase.Enqueue(phase);
        //}

        _gameStats.turn = 1;
        _gameStats.activeUnit = null;
        _gameStats.enemyUnit = null;
        _gameStats.activePlayer = _player1;
        _gameStats.enemyPlayer = _player2;
        InitializeManager();

        GamePhaseProcessor.EnableNextPhase(_gamePhaseManagers,_gamePhase);

        if (SetPhaseEvent != null) SetPhaseEvent.OnEventRaised += SetPhase;

        _toggleGameinfoUI.RaiseEvent(true, _gameStats);

    }

    public void SetPhase(GameStatsSO gameStats) // 
    {
        ResetPreviousPhase(gameStats);
        SetNextPhaseToActive(gameStats);      
        GamePhaseProcessor.EnableNextPhase(_gamePhaseManagers, _gamePhase);

        if(IsEndOfPlayerTurn(_gamePhase))
        {
            TogglePlayers(gameStats);
            SetNextBattleRound(gameStats);
        }

        ToggleBattleRoundsAndUI(gameStats);
        
    }

    private void ResetPreviousPhase(GameStatsSO gameStats)
    {
        GamePhaseProcessor.ResetPreviousPhase(_gamePhaseManagers, _gamePhase);
        GamePhaseProcessor.ResetActivePlayerUnits(gameStats, _gamePhase);
    }

    private void SetNextPhaseToActive(GameStatsSO gameStats)
    {
        _gamePhase = GamePhaseProcessor.SetNextPhaseToActive(_gamePhase);
        gameStats.phase = _gamePhase;
    }

    private bool IsEndOfPlayerTurn(GamePhase gamePhases)
    {
        return GamePhaseProcessor.IsEndOfPlayerTurn(gamePhases);
    }

    public void TogglePlayers(GameStatsSO gameStats)
    {
        if (gameStats.activePlayer == _player1)
        {
            gameStats.activePlayer = _player2;
            gameStats.enemyPlayer = _player1;
        }
        else
        {
            gameStats.activePlayer = _player1;
            gameStats.enemyPlayer = _player2;
        }
    }

    private void SetNextBattleRound(GameStatsSO gameStats)
    {
        if (gameStats.activePlayer == _player1) gameStats.turn += 1;
    }

    private void ToggleBattleRoundsAndUI(GameStatsSO gameStats)
    {
        _toggleGameinfoUI.RaiseEvent(true, gameStats);
        _toggleBattleRounds.RaiseEvent(gameStats);
    }

    
    // Initialization
    private void InitializeManager()
    {
        if (_initializedManager) return;
        _gamePhaseManagers.Clear();

        var allPhases = Assembly.GetAssembly(typeof(PhaseManagerBase)).GetTypes()
            .Where(t => typeof(PhaseManagerBase).IsAssignableFrom(t) && t.IsAbstract == false);

        foreach (var subphase in allPhases)
        {
            PhaseManagerBase instance = gameObject.GetComponentInChildren(subphase) as PhaseManagerBase;
            _gamePhaseManagers.Add(instance.SubEvents, instance);
        }
        //_gamePhaseManagers = _gamePhases.ToDictionary(key => key.SubEvents, value => value);

        _initializedManager = true;
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
    //private void InitializeManager()
    //{
    //    if (_initializedManager) return;

    //    _gamePhaseManagers.Clear();

    //    var allPhases = Assembly.GetAssembly(typeof(PhaseManagerBase)).GetTypes()
    //        .Where(t => typeof(PhaseManagerBase).IsAssignableFrom(t) && t.IsAbstract == false);


    //    foreach (var subphase in allPhases)
    //    {
    //        GameObject gamePhaseManager = new GameObject();
    //        //GameObject gamePhaseManager = Instantiate()

    //        Component instance = gamePhaseManager.AddComponent(subphase);
    //        PhaseManagerBase test = instance as PhaseManagerBase;



    //        Debug.Log(test.name);
    //        //PhaseManagerBase gamePhaseManager = Activator.CreateInstance(subphase) as PhaseManagerBase;
    //        _gamePhaseManagers.Add(test.SubEvents, test);
    //    }

    //    //_gamePhaseManagers = _gamePhases.ToDictionary(key => key.SubEvents, value => value);

    //    _initializedManager = true;
    //}
    //private static void InitializeManager()
    //{
    //    if (_initializedManager) return;
    //    _gamePhaseManagers.Clear();

    //    var allPhases = Assembly.GetAssembly(typeof(PhaseManagerBase)).GetTypes()
    //        .Where(t => typeof(PhaseManagerBase).IsAssignableFrom(t) && t.IsAbstract == false);

    //    foreach (var subphase in allPhases)
    //    {
    //        PhaseManagerBase gamePhaseManager = Activator.CreateInstance(subphase) as PhaseManagerBase;
    //        _gamePhaseManagers.Add(gamePhaseManager.SubEvents, gamePhaseManager);
    //    }

    //    _initializedManager = true;
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
