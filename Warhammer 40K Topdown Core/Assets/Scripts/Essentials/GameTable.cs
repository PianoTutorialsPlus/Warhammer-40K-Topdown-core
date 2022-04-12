using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace WH40K.Essentials
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(NavMeshSurface))]
    public class GameTable : MonoBehaviour, IPointerClickHandler
    {
        //public BattleroundEventChannelSO SetMovementPhaseEvent;
        //public GameStatsSO _gameStats;
        public NavMeshSurface Surface;

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
}