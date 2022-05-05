using WH40K.GameMechanics;

namespace Editor.Infrastructure.GamePhases
{
    public class MovementPhaseProcessorBuilder : TestDataBuilder<MovementPhaseProcessor>
    {
        private IGamePhase _gamePhase;

        public MovementPhaseProcessorBuilder()
        {
        }
        public MovementPhaseProcessorBuilder WithGamePhase(IGamePhase gamePhase)
        {
            _gamePhase = gamePhase;
            return this;
        }

        public override MovementPhaseProcessor Build()
        {
            var movementPhaseProcessor = new MovementPhaseProcessor(_gamePhase ??= A.GamePhase.Build());
            //movementPhaseProcessor.SetPrivate(mPP => mPP.Initialized, false);
            return movementPhaseProcessor;
        }
    }
}
