using NSubstitute;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace Editor.Infrastructure
{
    public class GamePhaseBuilder : TestDataBuilder<IGamePhase>
    {
        private GameStatsSO _gameStats;
        private IPhase _battleRoundEvents;

        public GamePhaseBuilder()
        {
        }
        public GamePhaseBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }
        public GamePhaseBuilder WithBattleroundEvent(IPhase battleroundEvent)
        {
            _battleRoundEvents = battleroundEvent;
            return this;
        }

        public override IGamePhase Build()
        {
            var gamePhase = Substitute.For<IGamePhase>();
            gamePhase.GameStats = _gameStats ??= A.GameStats;
            gamePhase.BattleroundEvents.Returns(_battleRoundEvents ??= An.IPhaseEvent.Build());
            return gamePhase;
        }
    }
}
