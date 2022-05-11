using WH40K.PlayerEvents;

namespace Editor.Infrastructure.Player
{
    public class UnitSelectorBuilder : TestDataBuilder<UnitSelector>
    {
        private IUnit _unit;

        public UnitSelectorBuilder()
        {
        }

        public UnitSelectorBuilder WithUnit(IUnit unit)
        {
            _unit = unit;
            return this;
        }
        public override UnitSelector Build()
        {
            var unitSelector = new UnitSelector(_unit ??= A.Unit.Build());
            return unitSelector;
        }
    }

}
