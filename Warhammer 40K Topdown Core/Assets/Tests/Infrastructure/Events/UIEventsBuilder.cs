using System;
using WH40K.EventChannels;

namespace Editor.Infrastructure.Events
{
    public class UIEventsBuilder<T> : TestDataBuilder<T> where T : class
    {
        private IManageUIEvents _uIMovementRange;
        public UIEventsBuilder()
        {
        }
        public UIEventsBuilder<T> WithUIEvents(IManageUIEvents uiEvents)
        {
            _uIMovementRange = uiEvents;
            return this;
        }
        public override T Build()
        {
            return Activator.CreateInstance(typeof(T), _uIMovementRange) as T;
        }
    }
}
