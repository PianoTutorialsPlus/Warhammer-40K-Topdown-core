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
        _toggleBattleRounds.RaiseEvent(); //Initialization    

    }

    private void OnEnable()
    {
        //Initialization
        //Initialize();
        Debug.Log("interActionManager");
        
        GamePhaseProcessor.EnableNextPhase(_gamePhase.Peek());

        if (SetPhaseEvent != null) SetPhaseEvent.OnEventRaised += SetPhase;

        _toggleGameinfoUI.RaiseEvent(true);

    }
    private void EnqueueGamePhase()
    {
        foreach (GamePhase phase in GamePhaseProcessor.GetAbilityByName())
        {
            _gamePhase.Enqueue(phase);
        }
    }

    public void SetPhase()
    {
        ResetPreviousPhase();
        SetNextPhaseToActive();
        GamePhaseProcessor.EnableNextPhase(_gamePhase.Peek());

        if (IsEndOfPlayerTurn(_gamePhase.Peek()))
        {
            TogglePlayers();
            SetNextBattleRound();
        }
        ToggleBattleRoundsAndUI();
    }

    private void ResetPreviousPhase()
    {
        GamePhaseProcessor.ResetPreviousPhase(_gamePhase.Peek());
        GamePhaseProcessor.ResetActivePlayerUnits(_gamePhase.Peek());
    }

    private void SetNextPhaseToActive()
    {
        _gamePhase.Enqueue(_gamePhase.Dequeue());
        GameStats.Phase = _gamePhase.Peek();
    }

    private bool IsEndOfPlayerTurn(GamePhase gamePhases)
    {
        return GamePhaseProcessor.IsEndOfPlayerTurn(gamePhases);
    }

    public void TogglePlayers()
    {
        if (GameStats.ActivePlayer == _player1)
        {
            GameStats.ActivePlayer = _player2;
            GameStats.EnemyPlayer = _player1;
        }
        else
        {
            GameStats.ActivePlayer = _player1;
            GameStats.EnemyPlayer = _player2;
        }
    }

    private void SetNextBattleRound()
    {
        if (GameStats.ActivePlayer == _player1) GameStats.Turn += 1;
    }

    private void ToggleBattleRoundsAndUI()
    {
        _toggleGameinfoUI.RaiseEvent(true);
        _toggleBattleRounds.RaiseEvent();
    }
}
