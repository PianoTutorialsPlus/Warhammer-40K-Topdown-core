using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace WH40K.Essentials

{
    public class UnitMovementPhase : MonoBehaviour, IUnitActionPhase
    {
        private IUnit _unit;
        private Unit unit;
        private UnitSelector UnitSelector => _unit.UnitSelector;
        private UnityAction<Unit> onTapDownAction => _unit.OnTapDownAction;
        private UnityAction onPointerEnter => _unit.OnPointerEnter;
        private UnityAction<IUnit> onPointerEnterInfo => _unit.OnPointerEnterInfo;
        private UnityAction<IUnit> onPointerExit => _unit.OnPointerExit;
        private GameStatsSO _gameStats => _unit.GameStats;
        public bool IsSelected { get; set; }
        public IStats ActiveUnit { get => _gameStats.activeUnitTest; set => _gameStats.activeUnitTest = value; }

        private void Awake()
        {
            _unit = GetComponent<IUnit>();
            unit = GetComponent<Unit>();
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
            if (onPointerEnterInfo != null) onPointerEnterInfo(_unit);
            //if (onPointerEnterInfo != null) onPointerEnterInfo(gameObject.GetComponent<Unit>());
        }

        public void OnPointerExit(PointerEventData pointerEvent)
        {
            if (onPointerExit != null)
            {
                onPointerExit(_unit);
                //onPointerExit(gameObject.GetComponent<Unit>());
            }
        }

        public void OnPointerClick(PointerEventData pointerEvent)
        {
            //if (onPointerEnter != null) onPointerEnter();
            if (onTapDownAction == null) return;
            if (pointerEvent.button == PointerEventData.InputButton.Left)
            {
                SelectUnit();
                onTapDownAction(unit);
                //onTapDownAction(gameObject.GetComponent<Unit>());
            }
        }

        public void SelectUnit()
        {
            //IUnitStats test = unitSelector.GetUnit();
            //_gameStats.activeUnitTest = test;
            ActiveUnit = UnitSelector.GetUnit();
            SetIsSelected();
        }

        public void SetIsSelected()
        {
            IsSelected = UnitSelector.IsSelected;
        }
    }
}