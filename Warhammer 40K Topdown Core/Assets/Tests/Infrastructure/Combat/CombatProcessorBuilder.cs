using GameMechanics.Combat;
using WH40K.GameMechanics;

namespace Editor.Infrastructure.Combat
{
    public class CombatProcessorBuilder : TestDataBuilder<CombatProcessor>
    {
        private IResult _result;

        public CombatProcessorBuilder()
        {
        }
        public CombatProcessorBuilder WithIResult(IResult result)
        {
            _result = result;
            return this;
        }

        public override CombatProcessor Build()
        {
            var combatProcessor = new CombatProcessor(_result ??= An.IResultEvent.Build());
            return combatProcessor;
        }
    }
}
