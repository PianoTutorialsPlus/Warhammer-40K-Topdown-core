using UnityEngine.Events;
using WH40K.Stats.Player;

namespace WH40K.Gameplay.PlayerEvents
{
    public class UnitPointer
    {
        public UnityAction<IUnit> OnTapDownAction;
        public UnityAction OnPointerEnter;
        public UnityAction<IUnit> OnPointerEnterInfo;
        public UnityAction<IUnit> OnPointerExit;
    }
}
