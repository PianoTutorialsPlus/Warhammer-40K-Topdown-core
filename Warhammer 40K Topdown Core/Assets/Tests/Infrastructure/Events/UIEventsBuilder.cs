using Editor.Infrastructure;
using System;
using WH40K.Essentials;
using WH40K.UI;

namespace Editor.Infrastructure.Events
{
    public class UIEventsBuilder<T> : TestDataBuilder<T> where T : class
    {
        private IManageUIEvents _uIMovementRange;
        private GameStatsSO _gameStats;
        public UIEventsBuilder()
        {
        }
        public UIEventsBuilder<T> WithUIEvents(IManageUIEvents uiEvents)
        {
            _uIMovementRange = uiEvents;
            return this;
        }
        public UIEventsBuilder<T> WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }
        public override T Build()
        {
            return Activator.CreateInstance(typeof(T), _uIMovementRange, _gameStats) as T;
        }
    }
}
