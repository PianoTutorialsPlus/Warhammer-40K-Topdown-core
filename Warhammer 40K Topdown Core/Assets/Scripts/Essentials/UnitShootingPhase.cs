using UnityEngine;
using UnityEngine.EventSystems;

namespace WH40K.Essentials
{
    public class UnitShootingPhase : MonoBehaviour//, IUnitActionPhase
    {
        private IUnit _unit;
        private Unit unit;

        private void Awake()
        {
            _unit = GetComponent<IUnit>();
            unit = GetComponent<Unit>();
        }

        private void OnEnable()
        {
            Debug.Log("enable");
        }

        private void OnDisable()
        {
            Debug.Log("disable");
        }
        //public void OnPointerEnter(PointerEventData pointerEvent)
        //{
        //    if (onPointerEnter != null) onPointerEnter();
        //    if (onPointerEnterInfo != null) onPointerEnterInfo(this);
        //    //if (onPointerEnterInfo != null) onPointerEnterInfo(gameObject.GetComponent<Unit>());
        //}

        //public void OnPointerExit(PointerEventData pointerEvent)
        //{
        //    if (onPointerExit != null)
        //    {
        //        onPointerExit(this);
        //        //onPointerExit(gameObject.GetComponent<Unit>());
        //    }
        //}

        //public void OnPointerClick(PointerEventData pointerEvent)
        //{
        //    //if (onPointerEnter != null) onPointerEnter();
        //    if (onTapDownAction == null) return;
        //    if (pointerEvent.button == PointerEventData.InputButton.Left)
        //    {
        //        SelectUnit();
        //        onTapDownAction(this);
        //        //onTapDownAction(gameObject.GetComponent<Unit>());
        //    }
        //    // Shooting Phase
        //    //else if (pointerEvent.button == PointerEventData.InputButton.Right && _gameStats.activePlayer._playerUnits[0].tag == gameObject.tag)
        //    //{
        //    //    SetUnitAsEnemy();
        //    //}
        //}

        //// Shooting Phase
        //public void SetUnitAsEnemy()
        //{
        //    EnemyUnit = UnitSelector.GetUnit(EnemyFraction);
        //    //_gameStats.enemyUnit = gameObject.GetComponent<Unit>();
        //}

        //public void SelectUnit()
        //{
        //    //IUnitStats test = unitSelector.GetUnit();
        //    //_gameStats.activeUnitTest = test;
        //    ActiveUnit = UnitSelector.GetUnit();
        //    SetIsSelected();
        //}

        //public void SetIsSelected()
        //{
        //    IsSelected = UnitSelector.UnitIsFromFraction();
        //}
    }
}