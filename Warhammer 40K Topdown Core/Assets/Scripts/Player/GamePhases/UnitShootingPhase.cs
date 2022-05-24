using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace WH40K.PlayerEvents
{
    public class UnitShootingPhase : UnitPhasesBase, IUnitActionPhase
    {
        private void Awake()
        {
            //_unit = GetComponent<IUnit>();
            enabled = false;
        }

        private void OnEnable()
        {
            Debug.Log("enable");
        }
        private void OnDisable()
        {
            Debug.Log("disable");
        }

        [Inject]
        public void Construct(IUnit unit)
        {
            _unit = unit;
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
            else if (pointerEvent.button == PointerEventData.InputButton.Right/* && _gameStats.ActiveUnit != null*/)
            {
                UnitSelector.SelectEnemyUnit();
            }
        }
    }
}