using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace WH40K.Core
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(NavMeshSurface))]
    public class GameTable : MonoBehaviour, IPointerClickHandler
    {
        public NavMeshSurface Surface;
        public UnityAction<Vector3> onTapDownAction;

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