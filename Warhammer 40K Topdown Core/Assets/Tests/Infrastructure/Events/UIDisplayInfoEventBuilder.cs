using WH40K.Essentials;
using WH40K.UI;

namespace Editor.Infrastructure.Events
{
    public class UIDisplayInfoEventBuilder : TestDataBuilder<UIDisplayInfoEvents>
    {
        private IManageUIEvents _uIEvents;
        private GameStatsSO _gameStats;

        public UIDisplayInfoEventBuilder()
        {
        }

        public UIDisplayInfoEventBuilder WithUIEvents(IManageUIEvents uIEvents)
        {
            _uIEvents = uIEvents;
            return this;
        }
        public UIDisplayInfoEventBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }

        public override UIDisplayInfoEvents Build()
        {
            //return null;
            return new UIDisplayInfoEvents(_uIEvents, _gameStats);
        }
    }
}
