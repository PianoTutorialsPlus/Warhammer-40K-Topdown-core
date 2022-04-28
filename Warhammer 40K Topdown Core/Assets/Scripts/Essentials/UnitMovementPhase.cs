using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace WH40K.Essentials

{
    public class UnitMovementPhase : MonoBehaviour, IUnitActionPhase
    {
        private IUnit _unit;

        private UnitSelector UnitSelector => Unit.UnitSelector;
        private UnityAction<IUnit> onTapDownAction => Unit.OnTapDownAction;
        private UnityAction onPointerEnter => Unit.OnPointerEnter;
        private UnityAction<IUnit> onPointerEnterInfo => Unit.OnPointerEnterInfo;
        private UnityAction<IUnit> onPointerExit => Unit.OnPointerExit;
        private GameStatsSO _gameStats => Unit.GameStats;
        public bool IsSelected { get; set; }
        public IUnit ActiveUnit { get => _gameStats.ActiveUnit; set => _gameStats.ActiveUnit = value; }
        public IUnit Unit { get => _unit; private set => _unit = value; }

        private void Awake()
        {
            _unit = GetComponent<IUnit>();
            //unit = GetComponent<Unit>();
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