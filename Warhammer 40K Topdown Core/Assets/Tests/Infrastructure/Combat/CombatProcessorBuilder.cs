using WH40K.DiceEvents;
using WH40K.Gameplay.GamePhaseEvents;

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
            var combatProcessor = new CombatProcessor(A.GameStats.Build(),_result ??= An.IResultEvent.Build());
            return combatProcessor;
        }
    }
}
