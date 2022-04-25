using WH40K.Essentials;
using WH40K.UI;

namespace Editor.Infrastructure.Events
{
    public class UIMovementRangeEventBuilder : TestDataBuilder<UIMovementRangeEvents>
    {
        private IUIMovementRange _uIMovementRange;
        private GameStatsSO _gameStats;

        public UIMovementRangeEventBuilder()
        {
        }
        public UIMovementRangeEventBuilder WithUIEvents(IUIMovementRange uiMovementRange)
        {
            _uIMovementRange = uiMovementRange;
            return this;
        }
        public UIMovementRangeEventBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }

        public override UIMovementRangeEvents Build()
        {
            return new UIMovementRangeEvents(_uIMovementRange, _gameStats);
        }
    }
}
