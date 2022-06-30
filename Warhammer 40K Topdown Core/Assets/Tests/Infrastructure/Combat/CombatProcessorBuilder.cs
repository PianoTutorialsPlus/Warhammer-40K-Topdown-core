using WH40K.DiceEvents;
using WH40K.Gameplay.Combat;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;
using WH40K.Stats.Combat;

namespace Editor.Infrastructure.Combat
{
    public class CombatProcessorBuilder : TestDataBuilder<CombatProcessor>
    {
        private IResult _result;
        private GameStatsSO _gameStats;

        public CombatProcessorBuilder()
        {
        }
        public CombatProcessorBuilder WithIResult(IResult result)
        {
            _result = result;
            return this;
        }
        public CombatProcessorBuilder WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }

        public override CombatProcessor Build()
        {
            Container.Bind<CombatProcessor>().AsSingle()
                .WithArguments(_gameStats ??= A.GameStats.Build(), _result ??= An.IResultEvent.Build());

            return Container.Resolve<CombatProcessor>();
        }
    }
}
