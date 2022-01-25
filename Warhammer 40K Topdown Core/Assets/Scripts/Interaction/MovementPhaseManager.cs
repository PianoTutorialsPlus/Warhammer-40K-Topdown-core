using System.Collections.Generic;
using UnityEngine;



public class MovementPhaseManager : MonoBehaviour
{
    // public PlayerSO _activePlayer;
    // public PlayerSO _enemyPlayer;
    public PlayerSO _player1;
    public PlayerSO _player2;
    public GameStatsSO _gameStats;

    public InputReader _inputReader;

    [HideInInspector] public InteractionType currentInteraction;
    //To store the object we are currently interacting with
    private LinkedList<Interaction> _ongoingInteractions = new LinkedList<Interaction>();

    [SerializeField] private BattleroundEventChannelSO SetMovementPhaseEvent;

    //UI event
    [SerializeField] private InteractionUIEventChannelSO _toggleInteractionUI = default;
    [SerializeField] private InfoUIEventChannelSO _toggleInfoUI = default;
    [SerializeField] private InfoUIEventChannelSO _toggleEnemyInfoUI = default;
    [SerializeField] private IndicatorUIEventChannelSO _toggleIndicatorConnectionUI = default;

    //[Header("Listening to")]
    //Check if the interaction ended 
    //[SerializeField] private BattleroundEventChannelSO _onPhaseEnded = default;

    public void OnEnable()
    {
        //Debug.Log("Enable");
        if (SetMovementPhaseEvent != null) SetMovementPhaseEvent.OnEventRaised += SetMovementPhase;
        if (SetMovementPhaseEvent != null) SetMovementPhaseEvent.OnEventRaised -= ClearMovementPhase;
    }

    public void OnDisable()
    {
        //Debug.Log("Disable");
        if (SetMovementPhaseEvent != null) SetMovementPhaseEvent.OnEventRaised += ClearMovementPhase;
        if (SetMovementPhaseEvent != null) SetMovementPhaseEvent.OnEventRaised -= SetMovementPhase;
    }

    public void SetMovementPhase(GameStatsSO gameStats)
    {
        ClearMovementPhase(gameStats);
        _inputReader.activateEvent -= NextPhase;
        switch (gameStats.movementSubPhase)
        {
            case MovementPhase.Selection:
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
            case MovementPhase.Move:
                {
                    FillMethods(_gameStats.activeUnit, true, true, true, false);
                    //Debug.Log("Move");
                    gameStats.activeUnit.activated = true;
                    gameStats.gameTable.gameTable.onTapDownAction += Move;

                    //gameStats.movementSubPhase = MovementPhase.Selection;
                    break;
                }
        }
    }

    public void Move(Vector3 position)
    {
        _gameStats.activeUnit.SetDestination(position);
    }

    public void ClearMovementPhase(GameStatsSO gameStats)
    {
        foreach (Unit child in gameStats.activePlayer._playerUnits) FillMethods(child, false, false, false, false);
        foreach (Unit child in gameStats.enemyPlayer._playerUnits) FillMethods(child, false, false, false, false);
        gameStats.gameTable.gameTable.onTapDownAction -= Move;
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
        if(!unit.selected)_toggleInteractionUI.RaiseEvent(false, InteractionType.None);
        if (!unit.activated) _toggleInfoUI.RaiseEvent(false, unit);
          _toggleEnemyInfoUI.RaiseEvent(false, unit);
    }
    private void ConnectIndicator(Unit unit)
    {
        //Debug.Log("Connect");
        SetMovementPhase(_gameStats);
        _toggleIndicatorConnectionUI.RaiseEvent(true, unit);

    }

    private void NextPhase()
    {
        //Debug.Log("Phase");
        _gameStats.movementSubPhase = MovementPhase.Move;
        SetMovementPhase(_gameStats);
    }

    private void ActivateUnit()
    {
            _gameStats.activeUnit.activated = true;
            SetMovementPhase(_gameStats);

    }


    //public void HandleAction()
    //{
    //    var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        m_Selected.AddMovedDistance();
    //        m_Selected.GoTo(hit.point);
    //        m_Selected.GetDistance(hit.point);

    //    }
    //}

    //public void OnEnable()
    //{
    //    if (SetMovementPhaseEvent != null) SetMovementPhaseEvent.OnEventRaised += SetMovementPhase;
    //    if (SetMovementPhaseEvent != null) SetMovementPhaseEvent.OnEventRaised -= ClearMovementPhase;

    //    //_inputReader.activateEvent += ConnectIndicator;
    //    //Debug.Log("OnEnable");
    //    //foreach (Unit child in _gameStats.activePlayer._playerUnits)
    //    //{
    //    //    //_ongoingInteractions.AddFirst(new Interaction(ongoingInteractionType, child));
    //    //    child.onPointerEnter += DisplayInteractionUI;
    //    //    child.onPointerExit += ResetInteraction;
    //    //    child.onPointerEnterInfo += DisplayInfoUI;
    //    //    child.onTapDownAction += ConnectIndicator;

    //    //}
    //    //foreach (Unit child in _gameStats.enemyPlayer._playerUnits)
    //    //{
    //    //    //_ongoingInteractions.AddFirst(new Interaction(ongoingInteractionType, child));
    //    //    //child.GetComponent<Unit>().onPointerEnter += DisplayInteractionUI;
    //    //    child.onPointerExit += ResetInteraction;
    //    //    child.onPointerEnterInfo += DisplayInfoUI;
    //    //}
    //}

    //private void DisplayInteractionUI()
    //{
    //    //Raise event to display UI
    //    //Interaction ongoingInteraction = _ongoingInteractions.First.Value;
    //    //_toggleInteractionUI.RaiseEvent(true, ongoingInteraction.Type);
    //    _toggleInteractionUI.RaiseEvent(true, InteractionType.Activate);
    //}

    //private void ResetInteraction(Unit unit)
    //{
    //    _toggleInteractionUI.RaiseEvent(false, InteractionType.None);

    //    //if (_activePlayer._playerUnits[0].tag == unit.tag)
    //    _toggleInfoUI.RaiseEvent(false, unit);
    //    //if (_enemyPlayer._playerUnits[0].tag == unit.tag)
    //    _toggleEnemyInfoUI.RaiseEvent(false, unit);

    //    //LinkedListNode<Interaction> currentNode = _ongoingInteractions.First;
    //    //Debug.Log(_ongoingInteractions.Count);
    //    //while (currentNode != null)
    //    //{
    //    //    if (currentNode.Value.InteractableObject == obj)
    //    //    {
    //    //        Debug.Log(obj.name);
    //    //        _ongoingInteractions.Remove(currentNode);
    //    //        break;
    //    //    }
    //    //    currentNode = currentNode.Next;
    //    //}
    //    //Debug.Log(_ongoingInteractions.Count);
    //    //if (_ongoingInteractions.Count > 0)
    //    //{
    //    //    _toggleInteractionUI.RaiseEvent(true, _ongoingInteractions.First.Value.Type);
    //    //}
    //    //else
    //    //{
    //    //    _toggleInteractionUI.RaiseEvent(false, InteractionType.None);
    //    //}
    //}
}
