using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace WH40K.Essentials

{
    public class UnitMovementPhase : UnitPhasesBase, IUnitActionPhase
    {
        private void Awake()
        {
            _unit = GetComponent<IUnit>();
        }

        private void OnEnable()
        {
            //  Debug.Log("enable");
        }

        private void OnDisable()
        {
            //  Debug.Log("disable");
        }

        public void OnPointerEnter(PointerEventData pointerEvent)
        {
            if (onPointerEnter != null) onPointerEnter();
            if (onPointerEnterInfo != null) onPointerEnterInfo(Unit);
        }

        public void OnPointerExit(PointerEventData pointerEvent)
        {
            if (onPointerExit != null)
            {
                onPointerExit(Unit);
            }
        }
        public void OnPointerClick(PointerEventData pointerEvent)
        {
            if (onTapDownAction == null) return;
            if (pointerEvent.button == PointerEventData.InputButton.Left)
            {
                UnitSelector.SelectUnit();
                onTapDownAction(Unit);
            }
        }
    }
}