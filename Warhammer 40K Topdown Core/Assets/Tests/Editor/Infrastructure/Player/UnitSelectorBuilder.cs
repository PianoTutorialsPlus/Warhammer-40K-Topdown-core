using WH40K.Essentials;

namespace Editor.Infrastructure.Player
{
    public class UnitSelectorBuilder : TestDataBuilder<UnitSelector>
    {
        private GameStatsSO _gameStats;
        private IUnit _unit;

        public UnitSelectorBuilder()
        {
        }
        public UnitSelectorBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }
        public UnitSelectorBuilder WithUnit(IUnit unit)
        {
            _unit = unit;
            return this;
        }
        public override UnitSelector Build()
        {
            var unitSelector = new UnitSelector(_gameStats ??= A.GameStats, _unit??= A.Unit.Build());
            return unitSelector;
        }
    }

}
