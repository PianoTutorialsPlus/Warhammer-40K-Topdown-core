using Editor.Infrastructure;
using UnityEngine.EventSystems;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats.Player;
using static UnityEngine.EventSystems.PointerEventData;

namespace Editor.Base
{
    public class UnitPhasesTestsBase : UnitElementsBase
    {
        public T SetUnitPhase<T>(IUnit unit, UnitSelector unitSelector = null) where T : UnitPhasesBase
        {
            T target = A.UnitPhase<T>()
                .WithUnitSelector(unitSelector)
                .WithUnit(unit);
            return target;
        }

        public PointerEventData GetPointerEvent(InputButton button = default)
        {
            return A.PointerEventData
                .WithButtonPressed(button);
        }

        public void UnityActionFiller(IUnit unit)
        {
        }

        public void UnityActionFiller()
        {
        }
    }
}
