using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameTable : MonoBehaviour, IPointerClickHandler
{
    public BattleroundEventChannelSO SetMovementPhaseEvent;
    public GameStatsSO _gameStats;

    public UnityAction<Vector3> onTapDownAction;
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        if (pointerEvent.button == PointerEventData.InputButton.Right)
        {
            if (onTapDownAction != null)
            {
                onTapDownAction.Invoke(pointerEvent.pointerCurrentRaycast.worldPosition);
            }
        }
    }
}
