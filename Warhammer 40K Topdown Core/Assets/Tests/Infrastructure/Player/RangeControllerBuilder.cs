using UnityEngine;
using WH40K.UI;

namespace Editor.Infrastructure.Player
{
    public class RangeControllerBuilder : TestDataBuilder<UIRangeController>
    {
        private IUIRangeIndicator _rangeIndicator;
        private Vector3 _position;

        public RangeControllerBuilder()
        {
        }

        public RangeControllerBuilder WithRangeIndicator(IUIRangeIndicator range)
        {
            _rangeIndicator = range;
            return this;
        }
        public RangeControllerBuilder WithPosition(Vector3 position)
        {
            _position = position;
            return this;
        }

        public override UIRangeController Build()
        {
            var rangeController = new UIRangeController(_rangeIndicator);
            rangeController.SetPosition(_position);
            return rangeController;
        }
    }
}
