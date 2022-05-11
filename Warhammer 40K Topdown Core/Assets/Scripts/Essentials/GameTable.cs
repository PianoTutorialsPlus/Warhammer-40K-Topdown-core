using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace WH40K.Essentials
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(NavMeshSurface))]
    public class GameTable : MonoBehaviour, IPointerClickHandler, IGameTable
    {
        //public BattleroundEventChannelSO SetMovementPhaseEvent;
        public NavMeshSurface Surface;

        public UnityAction<Vector3> onTapDownAction;

        public UnityAction<Vector3> OnTapDownAction { get => onTapDownAction; set => onTapDownAction = value; }

        public void OnPointerClick(PointerEventData pointerEvent)
        {
            Debug.Log("Table");
            if (pointerEvent.button == PointerEventData.InputButton.Right)
            {
                Debug.Log("Table OnTap " + (onTapDownAction != null));

                if (onTapDownAction != null)
                {
                    Debug.Log("Table");
                    onTapDownAction.Invoke(pointerEvent.pointerCurrentRaycast.worldPosition);
                }
            }
        }
    }
}