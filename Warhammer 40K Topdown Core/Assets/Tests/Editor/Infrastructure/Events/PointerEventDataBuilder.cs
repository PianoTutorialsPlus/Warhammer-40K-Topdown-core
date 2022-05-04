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
            var eventSystem = new GameObject().AddComponent<EventSystem>();
            var pointerEvent = new PointerEventData(eventSystem);
            pointerEvent.button = _buttonPressed;
            return pointerEvent;
        }
    }
}
