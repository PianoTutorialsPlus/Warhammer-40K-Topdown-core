using WH40K.EventChannels;
using WH40K.GamePhaseEvents;

namespace Editor.Infrastructure.GamePhases
{
    public class ShootingSubPhaseProcessorBuilder : TestDataBuilder<ShootingSubPhaseProcessor>
    {
        private IResult _result;

        public ShootingSubPhaseProcessorBuilder()
        {
        }
        public ShootingSubPhaseProcessorBuilder WithIResult(IResult result)
        {
            _result = result;
            return this;
        }

        public override ShootingSubPhaseProcessor Build()
        {
            Container.Bind<ShootingSubPhaseProcessor>().AsSingle();
            //var shootingSubPhaseProcessor = new ShootingSubPhaseProcessor();
            return Container.Resolve<ShootingSubPhaseProcessor>();
        }
    }
}
