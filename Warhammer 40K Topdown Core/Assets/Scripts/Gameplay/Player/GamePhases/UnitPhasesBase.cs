using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace WH40K.Gameplay.PlayerEvents
{
    public abstract class UnitPhasesBase : MonoBehaviour
    {
        protected IUnit _unit;

        [Inject]
        public void Construct(
            IUnit unit,
            UnitSelector unitSelector,
            UnitPointer unitPointer)
        {
            _unit = unit;
            _unitPointer = unitPointer;
            UnitSelector = unitSelector;
        }

        private UnitPointer _unitPointer;
        protected UnitSelector UnitSelector;/* => Unit.UnitSelector;*/
        protected UnityAction<IUnit> onTapDownAction => _unit.OnTapDownAction;
        protected UnityAction onPointerEnter => _unit.OnPointerEnter;
        protected UnityAction<IUnit> onPointerEnterInfo => _unit.OnPointerEnterInfo;
        protected UnityAction<IUnit> onPointerExit => _unit.OnPointerExit;
        //protected UnityAction<IUnit> onTapDownAction => _unitPointer.OnTapDownAction;
        //protected UnityAction onPointerEnter => _unitPointer.OnPointerEnter;
        //protected UnityAction<IUnit> onPointerEnterInfo => _unitPointer.OnPointerEnterInfo;
        //protected UnityAction<IUnit> onPointerExit => _unitPointer.OnPointerExit;
        //public IUnit ActiveUnit { get => GameStats.ActiveUnit; set => GameStats.ActiveUnit = value; }
        public IUnit Unit { get => _unit; protected set => _unit = value; }
    }
}
