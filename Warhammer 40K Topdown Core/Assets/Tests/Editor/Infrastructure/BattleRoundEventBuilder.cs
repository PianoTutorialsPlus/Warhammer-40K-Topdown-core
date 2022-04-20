using WH40K.Essentials;
using WH40K.GameMechanics;
using WH40K.UI;

namespace Editor.Infrastructure
{
    public class BattleRoundEventBuilder : TestDataBuilder<BattleRoundEvents>
    {
        private IUIMovementRange _uIMovementRange;
        private GameStatsSO _gameStats;

        public BattleRoundEventBuilder()
        {
        }
        public BattleRoundEventBuilder WithUIEvents(IUIMovementRange uiMovementRange)
        {
            _uIMovementRange = uiMovementRange;
            return this;
        }
        public BattleRoundEventBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }

        public override BattleRoundEvents Build()
        {
            return new BattleRoundEvents(_uIMovementRange, _gameStats);
        }
    }
}
