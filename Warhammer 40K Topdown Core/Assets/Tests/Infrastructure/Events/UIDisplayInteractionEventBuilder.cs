using WH40K.Essentials;
using WH40K.UI;

namespace Editor.Infrastructure.Events
{
    public class UIDisplayInteractionEventBuilder : TestDataBuilder<UIDisplayInteractionEvents>
    {
        private IManageUIEvents _uIEvents;
        //private Fraction _playerFraction;
        //private IStats _activeUnit;
        private GameStatsSO _gameStats;

        public UIDisplayInteractionEventBuilder()
        {
        }

        public UIDisplayInteractionEventBuilder WithUIEvents(IManageUIEvents uIEvents)
        {
            _uIEvents = uIEvents;
            return this;
        }
        public UIDisplayInteractionEventBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }

        //public UIDisplayInteractionEventBuilder WithPlayerFraction(Fraction playerFraction)
        //{
        //    _playerFraction = playerFraction;
        //    return this;
        //}
        //public UIDisplayInteractionEventBuilder WithActiveUnit(IStats activeUnit)
        //{
        //    _activeUnit = activeUnit;
        //    return this;
        //}
        public override UIDisplayInteractionEvents Build()
        {
            return new UIDisplayInteractionEvents(_uIEvents, _gameStats);
        }
    }
}
