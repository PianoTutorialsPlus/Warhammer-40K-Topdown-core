using UnityEngine.Events;

namespace WH40K.UnitHandler
{
    public interface IUnit
    {
        UnityAction<Unit> OnTapDownAction { get; set; }
        UnityAction OnPointerEnter { get; set; }
        UnityAction<Unit> OnPointerEnterInfo { get; set; }
        UnityAction<Unit> OnPointerExit { get; set; }
        Unit unit { get; }
        UnitSelector UnitSelector { get; }
        GameStatsSO GameStats { get; set; }
    }
}