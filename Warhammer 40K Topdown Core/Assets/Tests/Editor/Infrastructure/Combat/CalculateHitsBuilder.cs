using WH40K.GameMechanics;
using WH40K.GameMechanics.Combat;

namespace Editor.Infrastructure.Combat
{
    public class CalculateHitsBuilder : TestDataBuilder<CalculateHits>
    {
        private IResult _results;

        public CalculateHitsBuilder()
        {
        }
        public CalculateHitsBuilder WithIResult(IResult results)
        {
            _results = results;
            return this;
        }
        public override CalculateHits Build()
        {
            return new CalculateHits(_results ??= An.IResultEvent.Build());
        }
    }
}
