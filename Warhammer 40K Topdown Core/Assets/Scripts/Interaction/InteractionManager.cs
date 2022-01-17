using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InteractionType { None = 0, Activate,ShowStats}
public class InteractionManager : MonoBehaviour
{
    public PlayerSO _activePlayer;
    public PlayerSO _enemyPlayer;
    public PlayerSO _player1;
    public PlayerSO _player2;

    [HideInInspector] public InteractionType currentInteraction;
    //To store the object we are currently interacting with
    private LinkedList<Interaction> _ongoingInteractions = new LinkedList<Interaction>();
    //UI event
    [SerializeField] private InteractionUIEventChannelSO _toggleInteractionUI = default;
    [SerializeField] private InfoUIEventChannelSO _toggleInfoUI = default;
    [SerializeField] private InfoUIEventChannelSO _toggleEnemyInfoUI = default;
    [Header("Listening to")]
    //Check if the interaction ended 
    [SerializeField] private VoidEventChannelSO _onInteractionEnded = default;

    private void OnEnable()
    {
        // StartCoroutine(Sets());
        _activePlayer = _player1;
        _enemyPlayer = _player2;

        foreach (GameObject child in _player1._playerUnits)
        {
            //_ongoingInteractions.AddFirst(new Interaction(ongoingInteractionType, child));
            child.GetComponent<Unit>().onPointerEnter += DisplayInteractionUI;
            child.GetComponent<Unit>().onPointerExit += ResetInteraction;

            child.GetComponent<Unit>().onPointerEnterInfo += DisplayInfoUI;
        }
        foreach (GameObject child in _player2._playerUnits)
        {
            //_ongoingInteractions.AddFirst(new Interaction(ongoingInteractionType, child));
            child.GetComponent<Unit>().onPointerEnter += DisplayInteractionUI;
            child.GetComponent<Unit>().onPointerExit += ResetInteraction;

            child.GetComponent<Unit>().onPointerEnterInfo += DisplayInfoUI;
        }
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


    private void DisplayInfoUI(Unit unit)
    {
        if(_activePlayer._playerUnits[0].tag == unit.tag)
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
}
