using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;

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

public class InteractionManager : MonoBehaviour//, IInteractionManager
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

    //Queues
    private Queue<GamePhase> _gamePhase = new Queue<GamePhase>();

    private void Awake()
    {
        EnqueueGamePhase();
    }

    private void Start()
    {
        _toggleBattleRounds.RaiseEvent(_gameStats); //Initialization    

    }

    private void OnEnable()
    {
        //Initialization
        //Initialize();
        Debug.Log("interActionManager");
        
        GamePhaseProcessor.EnableNextPhase(_gamePhase.Peek());

        if (SetPhaseEvent != null) SetPhaseEvent.OnEventRaised += SetPhase;

        _toggleGameinfoUI.RaiseEvent(true, _gameStats);

    }
    private void EnqueueGamePhase()
    {
        foreach (GamePhase phase in GamePhaseProcessor.GetAbilityByName())
        {
            _gamePhase.Enqueue(phase);
        }
    }

    public void SetPhase(GameStatsSO gameStats)
    {
        ResetPreviousPhase(gameStats);
        SetNextPhaseToActive(gameStats);
        GamePhaseProcessor.EnableNextPhase(_gamePhase.Peek());

        if (IsEndOfPlayerTurn(_gamePhase.Peek()))
        {
            TogglePlayers(gameStats);
            SetNextBattleRound(gameStats);
        }
        ToggleBattleRoundsAndUI(gameStats);
    }

    private void ResetPreviousPhase(GameStatsSO gameStats)
    {
        GamePhaseProcessor.ResetPreviousPhase(_gamePhase.Peek());
        GamePhaseProcessor.ResetActivePlayerUnits(gameStats, _gamePhase.Peek());
    }

    private void SetNextPhaseToActive(GameStatsSO gameStats)
    {
        _gamePhase.Enqueue(_gamePhase.Dequeue());
        gameStats.phase = _gamePhase.Peek();
    }

    private bool IsEndOfPlayerTurn(GamePhase gamePhases)
    {
        return GamePhaseProcessor.IsEndOfPlayerTurn(gamePhases);
    }

    public void TogglePlayers(GameStatsSO gameStats)
    {
        if (gameStats.ActivePlayer == _player1)
        {
            gameStats.ActivePlayer = _player2;
            gameStats.EnemyPlayer = _player1;
        }
        else
        {
            gameStats.ActivePlayer = _player1;
            gameStats.EnemyPlayer = _player2;
        }
    }

    private void SetNextBattleRound(GameStatsSO gameStats)
    {
        if (gameStats.ActivePlayer == _player1) gameStats.turn += 1;
    }

    private void ToggleBattleRoundsAndUI(GameStatsSO gameStats)
    {
        _toggleGameinfoUI.RaiseEvent(true, gameStats);
        _toggleBattleRounds.RaiseEvent(gameStats);
    }
}
