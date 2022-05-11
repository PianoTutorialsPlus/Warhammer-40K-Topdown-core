using NSubstitute;
using WH40K.Events;

namespace Editor.Infrastructure.GamePhases
{
    public class IGamePhaseBuilder : TestDataBuilder<IGamePhase>
    {
        private IPhase _battleRoundEvents;

        public IGamePhaseBuilder()
        {
        }

        public IGamePhaseBuilder WithBattleroundEvent(IPhase battleroundEvent)
        {
            _battleRoundEvents = battleroundEvent;
            return this;
        }

        public override IGamePhase Build()
        {
            var gamePhase = Substitute.For<IGamePhase>();
            gamePhase.BattleroundEvents.Returns(_battleRoundEvents ??= An.IPhaseEvent.Build());
            return gamePhase;
        }
    }
}
