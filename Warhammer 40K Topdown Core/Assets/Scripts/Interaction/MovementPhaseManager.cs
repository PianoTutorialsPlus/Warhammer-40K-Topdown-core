using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPhaseManager : MonoBehaviour
{
    public PlayerSO _activePlayer;
    public PlayerSO _enemyPlayer;
    public PlayerSO _player1;
    public PlayerSO _player2;

    public InputReader _inputReader;

    [HideInInspector] public InteractionType currentInteraction;
    //To store the object we are currently interacting with
    private LinkedList<Interaction> _ongoingInteractions = new LinkedList<Interaction>();
    //UI event
    [SerializeField] private InteractionUIEventChannelSO _toggleInteractionUI = default;
    [SerializeField] private InfoUIEventChannelSO _toggleInfoUI = default;
    [SerializeField] private InfoUIEventChannelSO _toggleEnemyInfoUI = default;
    [SerializeField] private IndicatorUIEventChannelSO _toggleIndicatorConnectionUI = default;
    

    [Header("Listening to")]
    //Check if the interaction ended 
    [SerializeField] private VoidEventChannelSO _onInteractionEnded = default;



    //public PhaseEventChannelSO SetPhaseEvent;

    public void OnEnable()
    {
        
        _inputReader.activateEvent += ConnectIndicator;
        //_inputReader.activateEvent += ConnectIndicator;
        //Debug.Log("OnEnable");
        foreach (Unit child in _activePlayer._playerUnits)
        {
            //_ongoingInteractions.AddFirst(new Interaction(ongoingInteractionType, child));
            child.onPointerEnter += DisplayInteractionUI;
            child.onPointerExit += ResetInteraction;
            child.onPointerEnterInfo += DisplayInfoUI;
            child.onTapDownAction += ConnectIndicator;

        }
        foreach (Unit child in _enemyPlayer._playerUnits)
        {
            //_ongoingInteractions.AddFirst(new Interaction(ongoingInteractionType, child));
            //child.GetComponent<Unit>().onPointerEnter += DisplayInteractionUI;
            child.onPointerExit += ResetInteraction;
            child.onPointerEnterInfo += DisplayInfoUI;
        }
    }

    public void OnDisable()
    {
        _inputReader.activateEvent -= ConnectIndicator;
        //Debug.Log("OnDisable");
        foreach (Unit child in _activePlayer._playerUnits)
        {
            //_ongoingInteractions.AddFirst(new Interaction(ongoingInteractionType, child));
            child.onPointerEnter -= DisplayInteractionUI;
            child.onPointerExit -= ResetInteraction;
            child.onPointerEnterInfo -= DisplayInfoUI;
            child.onTapDownAction -= ConnectIndicator;
        }
        foreach (Unit child in _enemyPlayer._playerUnits)
        {
            //_ongoingInteractions.AddFirst(new Interaction(ongoingInteractionType, child));
            child.onPointerEnter -= DisplayInteractionUI;
            child.onTapDownAction -= ConnectIndicator;
            child.onPointerExit -= ResetInteraction;
            child.onPointerEnterInfo -= DisplayInfoUI;
          
        }
    }

    private void DisplayInfoUI(Unit unit)
    {
        if (_activePlayer._playerUnits[0].tag == unit.tag)
            _toggleInfoUI.RaiseEvent(true, unit);
        if (_enemyPlayer._playerUnits[0].tag == unit.tag)
            _toggleEnemyInfoUI.RaiseEvent(true, unit);
    }
    private void DisplayInteractionUI()
    {
        //Raise event to display UI
        //Interaction ongoingInteraction = _ongoingInteractions.First.Value;
        //_toggleInteractionUI.RaiseEvent(true, ongoingInteraction.Type);
        _toggleInteractionUI.RaiseEvent(true, InteractionType.Activate);
    }

    private void ResetInteraction(Unit unit)
    {
        _toggleInteractionUI.RaiseEvent(false, InteractionType.None);

        //if (_activePlayer._playerUnits[0].tag == unit.tag)
        _toggleInfoUI.RaiseEvent(false, unit);
        //if (_enemyPlayer._playerUnits[0].tag == unit.tag)
        _toggleEnemyInfoUI.RaiseEvent(false, unit);

        //LinkedListNode<Interaction> currentNode = _ongoingInteractions.First;
        //Debug.Log(_ongoingInteractions.Count);
        //while (currentNode != null)
        //{
        //    if (currentNode.Value.InteractableObject == obj)
        //    {
        //        Debug.Log(obj.name);
        //        _ongoingInteractions.Remove(currentNode);
        //        break;
        //    }
        //    currentNode = currentNode.Next;
        //}
        //Debug.Log(_ongoingInteractions.Count);
        //if (_ongoingInteractions.Count > 0)
        //{
        //    _toggleInteractionUI.RaiseEvent(true, _ongoingInteractions.First.Value.Type);
        //}
        //else
        //{
        //    _toggleInteractionUI.RaiseEvent(false, InteractionType.None);
        //}
    }
    private void ConnectIndicator(Unit unit)
    {
        _toggleIndicatorConnectionUI.RaiseEvent(true, unit);
    }
}
