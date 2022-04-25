using NSubstitute;
using WH40K.GameMechanics;

namespace Editor.Infrastructure
{
    public class IPhaseBuilder : TestDataBuilder<IPhase>
    {
        public IPhaseBuilder()
        {
        }

        public override IPhase Build()
        {
            return Substitute.For<IPhase>();
        }
    }
}
