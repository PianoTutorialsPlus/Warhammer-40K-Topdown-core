using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

namespace Editor.Infrastructure.Events
{
    public class PointerEventDataBuilder : TestDataBuilder<PointerEventData>
    {
        private InputButton _buttonPressed;

        public PointerEventDataBuilder()
        {
        }
        public PointerEventDataBuilder WithButtonPressed(InputButton buttonPressed)
        {
            _buttonPressed = buttonPressed;
            return this;
        }

        public override PointerEventData Build()
        {
            var eventSystem = Container.InstantiateComponentOnNewGameObject<EventSystem>();
            Container.Bind<PointerEventData>().AsSingle().WithArguments(eventSystem);
            var pointerEvent = Container.Resolve<PointerEventData>();

            pointerEvent.button = _buttonPressed;
            return pointerEvent;
        }
    }
}
