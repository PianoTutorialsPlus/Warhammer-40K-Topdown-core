using NSubstitute;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace Editor.Infrastructure.GamePhases
{
    public class IGamePhaseBuilder : TestDataBuilder<IGamePhase>
    {
        private GameStatsSO _gameStats;
        private IPhase _battleRoundEvents;

        public IGamePhaseBuilder()
        {
        }
        public IGamePhaseBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }
        public IGamePhaseBuilder WithBattleroundEvent(IPhase battleroundEvent)
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
