using UnityEngine.Events;

namespace WH40K.PlayerEvents
{
    public class UnitPointer
    {
        public UnityAction<IUnit> OnTapDownAction;
        public UnityAction OnPointerEnter;
        public UnityAction<IUnit> OnPointerEnterInfo;
        public UnityAction<IUnit> OnPointerExit;
    }
}
