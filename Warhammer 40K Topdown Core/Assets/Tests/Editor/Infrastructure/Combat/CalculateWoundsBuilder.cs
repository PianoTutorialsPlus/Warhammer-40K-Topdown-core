using WH40K.GameMechanics;
using WH40K.GameMechanics.Combat;

namespace Editor.Infrastructure.Combat
{
    public class CalculateWoundsBuilder : TestDataBuilder<CalculateWounds>
    {
        private IResult _results;

        public CalculateWoundsBuilder()
        {
        }
        public CalculateWoundsBuilder WithIResult(IResult results)
        {
            _results = results;
            return this;
        }
        public override CalculateWounds Build()
        {
            return new CalculateWounds(_results ??= An.IResultEvent.Build());
        }
    }
}
