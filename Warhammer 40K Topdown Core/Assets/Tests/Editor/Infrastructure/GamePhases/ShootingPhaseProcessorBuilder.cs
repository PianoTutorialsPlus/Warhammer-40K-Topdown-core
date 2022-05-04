using WH40K.GameMechanics;

namespace Editor.Infrastructure.GamePhases
{
    public class ShootingPhaseProcessorBuilder : TestDataBuilder<ShootingPhaseProcessor>
    {
        private IGamePhase _gamePhase;

        public ShootingPhaseProcessorBuilder()
        {
        }
        public ShootingPhaseProcessorBuilder WithGamePhase(IGamePhase gamePhase)
        {
            _gamePhase = gamePhase;
            return this;
        }
        public override ShootingPhaseProcessor Build()
        {
            var shootingPhaseProcessor = new ShootingPhaseProcessor(_gamePhase ??= A.GamePhase.Build());
            //movementPhaseProcessor.SetPrivate(mPP => mPP.Initialized, false);
            return shootingPhaseProcessor;
        }
    }
}
