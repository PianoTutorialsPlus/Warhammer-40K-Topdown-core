using WH40K.GameMechanics;
using WH40K.GameMechanics.Combat;

namespace Editor.Infrastructure.Combat
{
    public class DealDamageBuilder : TestDataBuilder<DealDamage>
    {
        private IResult _results;

        public DealDamageBuilder()
        {
        }
        public DealDamageBuilder WithIResult(IResult results)
        {
            _results = results;
            return this;
        }
        public override DealDamage Build()
        {
            return new DealDamage(_results ??= An.IResultEvent.Build());
        }
    }
}
