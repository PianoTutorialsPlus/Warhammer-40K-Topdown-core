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
        //public GameStatsSO _gameStats;
        public NavMeshSurface Surface;

        private UnityAction<Vector3> onTapDownAction;

        public UnityAction<Vector3> OnTapDownAction { get => onTapDownAction; set => onTapDownAction = value; }

        public void OnPointerClick(PointerEventData pointerEvent)
        {
            if (pointerEvent.button == PointerEventData.InputButton.Right)
            {
                if (OnTapDownAction != null)
                {
                    OnTapDownAction.Invoke(pointerEvent.pointerCurrentRaycast.worldPosition);
                }
            }
        }
    }
}