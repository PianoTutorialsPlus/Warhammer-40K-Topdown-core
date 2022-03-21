using UnityEngine;

[CreateAssetMenu(menuName = "Game/Battleround Events")]
public class BattleRoundsSO : ScriptableObject, IPhase
{
    public GameStatsSO _gameStats;
    public InputReader _inputReader;

    [SerializeField] private BattleroundEventChannelSO SetPhaseEvent;

    //UI event
    [SerializeField] private InteractionUIEventChannelSO _toggleInteractionUI = default;
    [SerializeField] private InfoUIEventChannelSO _toggleInfoUI = default;
    [SerializeField] private InfoUIEventChannelSO _toggleEnemyInfoUI = default;
    [SerializeField] private IndicatorUIEventChannelSO _toggleIndicatorConnectionUI = default;

    public void HandlePhase(GameStatsSO gameStats)
    {
        foreach (Unit child in gameStats.activePlayer._playerUnits)
        {
            CheckIfUnitIsDone(child);
            CheckIfUnitIsActive(child);
        }
        foreach (Unit child in gameStats.enemyPlayer._playerUnits) FillMethods(child, false, true, true, false);
    }
    private void CheckIfUnitIsDone(Unit child)
    {
        if (isUnitDone(child))
        {
            FillMethods(child, false, true, false, false);
        }
    }
    private bool isUnitDone(Unit child)
    {
        return child.done;
    }

    private void CheckIfUnitIsActive(Unit child)
    {
        if (isUnitDone(child)) return;

        if (isUnitActive(child))
        {
            FillMethods(child, true, true, true, true);
        }
        else
        {
            FillMethods(child, false, true, true, true);
        }
    }

    private bool isUnitActive(Unit child)
    {
        return child == _gameStats.activeUnit;
    }

    public void HandleMove(GameStatsSO gameStats)
    {
        FillMethods(gameStats.activeUnit, true, true, true, false);
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
        if (!unit.IsSelected) _toggleInteractionUI.RaiseEvent(false, InteractionType.None);
        if (!unit.activated) _toggleInfoUI.RaiseEvent(false, unit);
        _toggleEnemyInfoUI.RaiseEvent(false, unit);
    }
    private void ConnectIndicator(Unit unit)
    {
        SetPhaseEvent.RaiseEvent(_gameStats);
        _toggleIndicatorConnectionUI.RaiseEvent(true, unit);
    }


}
