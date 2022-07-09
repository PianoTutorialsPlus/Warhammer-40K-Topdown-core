using WH40K.DiceEvents;
using WH40K.Gameplay.GamePhaseEvents;

namespace Editor.Infrastructure.GamePhases
{
    public class ShootingSubPhaseProcessorBuilder : TestDataBuilder<ShootingSubPhaseProcessor>
    {
        private IResult _result;
        private GamePhaseFactory _factory;

        public ShootingSubPhaseProcessorBuilder()
        {
        }
        public ShootingSubPhaseProcessorBuilder WithIResult(IResult result)
        {
            _result = result;
            return this;
        }
        public ShootingSubPhaseProcessorBuilder WithGamePhaseFactory(GamePhaseFactory factory)
        {
            _factory = factory;
            return this;
        }

        public override ShootingSubPhaseProcessor Build()
        {
            Container.Bind<ShootingSubPhaseProcessor>().AsSingle();
            Container.BindInstance(_factory ??= A.GamePhaseFactory);

            return Container.Resolve<ShootingSubPhaseProcessor>();
        }
    }
}
