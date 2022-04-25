using NSubstitute;
using WH40K.GameMechanics;

namespace Editor.Infrastructure.GamePhases
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
