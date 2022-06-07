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
            Container.Bind<UIRangeController>().AsSingle().WithArguments(_rangeIndicator);
            var rangeController = Container.Resolve<UIRangeController>();
            rangeController.SetPosition(_position);

            return rangeController;
        }
    }
}
