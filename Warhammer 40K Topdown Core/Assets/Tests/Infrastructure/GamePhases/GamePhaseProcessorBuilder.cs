using System;
using WH40K.Gameplay.Events;
using WH40K.Stats;

namespace Editor.Infrastructure.GamePhases
{
    public class GamePhaseProcessorBuilder<T> : TestDataBuilder<T> where T : class
    {
        private IPhase _gamePhase;
        private GameStatsSO _gameStats;

        public GamePhaseProcessorBuilder()
        {
        }
        public GamePhaseProcessorBuilder<T> WithGamePhase(IPhase gamePhase)
        {
            _gamePhase = gamePhase;
            return this;
        }
        public GamePhaseProcessorBuilder<T> WithGameStats(GameStatsSO gameStats)
        {
            _gameStats = gameStats;
            return this;
        }

        public override T Build()
        {
            Container.Bind<T>().AsSingle()
                .WithArguments(_gameStats ??= A.GameStats.Build(), _gamePhase ??= An.IPhaseEvent.Build()); ;
            return Container.Resolve<T>();
        }
    }
}
