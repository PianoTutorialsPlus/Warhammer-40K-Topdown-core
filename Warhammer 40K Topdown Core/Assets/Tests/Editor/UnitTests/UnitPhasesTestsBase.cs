using Editor.Infrastructure;
using UnityEngine;
using UnityEngine.Events;
using WH40K.Essentials;

namespace Editor.UnitTests
{
    public class UnitPhasesTestsBase
    {
        public UnityAction _pointerAction;
        public UnityAction<IUnit> _action;
        public IUnit GetUnit(Fraction playerFraction = Fraction.Necrons, bool isActivated = false, bool isDone = false)
        {
            return A.Unit
                    .WithOnPointerEnterInfo(_action)
                    .WithOnPointerEnter(_pointerAction)
                    .WithOnTapdownAction(_action)
                    .WithOnPointerExit(_action)
                    .WithUnitSelector(A.UnitSelector)
                    .WithFraction(playerFraction)
                    .WithIsActivatedState(isActivated)
                    .WithIsDoneState(isDone)
                    .Build();
        }
        public T SetUnitPhase<T>(IUnit unit) where T: UnitPhasesBase
        {
            T target = new GameObject().AddComponent<T>();
            target.SetPrivate(x => x.Unit, unit);
            return target;
        }

        public void UnityActionFiller(IUnit unit)
        {
        }

        public void UnityActionFiller()
        {
        }
    }
}
