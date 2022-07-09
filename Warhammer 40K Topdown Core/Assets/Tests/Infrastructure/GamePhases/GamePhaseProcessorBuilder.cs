using System;
using WH40K.Gameplay.Events;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;

namespace Editor.Infrastructure.GamePhases
{
    public class GamePhaseProcessorBuilder<T> : TestDataBuilder<T> where T : class
    {
        private IPhase _gamePhase;
        private GameStatsSO _gameStats;
        private GamePhaseFactory _factory;

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
        public GamePhaseProcessorBuilder<T> WithGamePhaseFactory(GamePhaseFactory factory)
        {
            _factory = factory;
            return this;
        }

        public override T Build()
        {
            Container.Bind<T>().AsSingle();

            Container.BindInstance(_factory ??= A.GamePhaseFactory
                .WithGamePhase(_gamePhase ??= An.IPhaseEvent.Build())
                .WithGameStats(_gameStats ??= A.GameStats));

            return Container.Resolve<T>();
        }
    }
}
