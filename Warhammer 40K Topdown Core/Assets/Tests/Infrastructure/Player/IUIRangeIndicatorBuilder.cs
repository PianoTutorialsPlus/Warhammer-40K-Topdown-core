using NSubstitute;
using WH40K.UI;

namespace Editor.Infrastructure
{
    public class IUIRangeIndicatorBuilder : TestDataBuilder<IUIRangeIndicator>
    {
        private int _baseSize;

        public IUIRangeIndicatorBuilder()
        {
        }

        public IUIRangeIndicatorBuilder WithBaseSize(int baseSize)
        {
            _baseSize = baseSize;
            return this;
        }

        public override IUIRangeIndicator Build()
        {
            var uiRangeIndicator = Substitute.For<IUIRangeIndicator>();
            uiRangeIndicator.BaseSize.Returns(_baseSize);
            
            return uiRangeIndicator;
        }
    }
}