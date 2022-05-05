using System;
using WH40K.GameMechanics;

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
            var shootingSubPhaseProcessor = new ShootingSubPhaseProcessor(_result ??= An.IResultEvent.Build());
            return shootingSubPhaseProcessor;
        }
    }
}
