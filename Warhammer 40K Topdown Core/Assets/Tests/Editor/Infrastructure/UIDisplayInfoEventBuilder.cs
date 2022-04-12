using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WH40K.Essentials;
using WH40K.UI;

namespace Editor.Infrastructure
{
    public class UIDisplayInfoEventBuilder : TestDataBuilder<UIDisplayInfoEvents>
    {
        private IManageUIEvents _uIEvents;
        private Fraction _playerFraction = Fraction.Necrons;

        public UIDisplayInfoEventBuilder()
        {
        }

        public UIDisplayInfoEventBuilder WithUIEvents(IManageUIEvents uIEvents)
        {
            _uIEvents = uIEvents;
            return this;
        }
        public UIDisplayInfoEventBuilder WithPlayerFraction(Fraction playerFraction)
        {
            _playerFraction = playerFraction;
            return this;
        }

        public override UIDisplayInfoEvents Build()
        {
            return new UIDisplayInfoEvents(_uIEvents, _playerFraction);
        }
    }
}
