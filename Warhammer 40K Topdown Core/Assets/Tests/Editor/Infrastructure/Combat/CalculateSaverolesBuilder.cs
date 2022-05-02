using WH40K.GameMechanics;
using WH40K.GameMechanics.Combat;

namespace Editor.Infrastructure.Combat
{
    public class CalculateSaverolesBuilder : TestDataBuilder<CalculateSaveroles>
    {
        private IResult _results;

        public CalculateSaverolesBuilder()
        {
        }
        public CalculateSaverolesBuilder WithIResult(IResult results)
        {
            _results = results;
            return this;
        }
        public override CalculateSaveroles Build()
        {
            return new CalculateSaveroles(_results ??= An.IResultEvent.Build());
        }
    }
}
