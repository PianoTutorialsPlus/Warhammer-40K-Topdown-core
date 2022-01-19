using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InteractionType { None = 0, Activate,ShowStats}
public enum GamePhase { None = 0, MovementPhase,ShootingPhase}

public class InteractionManager : MonoBehaviour
{
    public PlayerSO _activePlayer;
    public PlayerSO _enemyPlayer;
    public PlayerSO _player1;
    public PlayerSO _player2;

    public PhaseSO _phase; //Initialization
    public TurnSO _turn; //Initialization

    [SerializeField] PhaseEventChannelSO SetPhaseEvent = default;
    [SerializeField] TurnEventChannelSO SetTurnEvent = default;
    [SerializeField] private GameinfoUIEventChannelSO _toggleGameinfoUI = default;

    [SerializeField] MovementPhaseManager movementPhase;
    [SerializeField] ShootingPhaseManager shootingPhase;

    private void OnEnable()
    {
        //Initialization
        _phase.phase = GamePhase.MovementPhase;
        _turn.turn = 1;

        // StartCoroutine(Sets());
        _activePlayer._playerUnits = _player1._playerUnits;
        _enemyPlayer._playerUnits = _player2._playerUnits;

        movementPhase.enabled = true;
        shootingPhase.enabled = false;

        if (SetPhaseEvent != null) SetPhaseEvent.OnEventRaised += SetPhase;
        
        _toggleGameinfoUI.RaiseEvent(true, _activePlayer._playerUnits[0],_phase,_turn);
    }


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

    public void SetPhase(PhaseSO phase)
    {
      
        if (phase.phase == GamePhase.MovementPhase)
        {
            movementPhase.enabled = false;
            shootingPhase.enabled = true;
            phase.phase = GamePhase.ShootingPhase;
            if (SetTurnEvent != null) SetTurnEvent.OnEventRaised -= SetTurn;
        }
        else if (phase.phase == GamePhase.ShootingPhase)
        {
            TogglePlayers();
            movementPhase.enabled = true;       
            shootingPhase.enabled = false;
            phase.phase = GamePhase.MovementPhase;
            if (SetTurnEvent != null && _activePlayer._playerUnits == _player1._playerUnits) SetTurnEvent.OnEventRaised += SetTurn;
        }
        _toggleGameinfoUI.RaiseEvent(true,_activePlayer._playerUnits[0],phase, _turn);
    }

    public void SetTurn(TurnSO turn)
    {
        turn.turn += 1;
        _toggleGameinfoUI.RaiseEvent(true, _activePlayer._playerUnits[0], _phase, turn);
    }

    public void TogglePlayers()
    {
        if(_activePlayer._playerUnits == _player1._playerUnits)
        {
            _activePlayer._playerUnits = _player2._playerUnits;
            _enemyPlayer._playerUnits = _player1._playerUnits;
        }
        else
        {
            _activePlayer._playerUnits = _player1._playerUnits;
            _enemyPlayer._playerUnits = _player2._playerUnits;
        }
    }

    
}
