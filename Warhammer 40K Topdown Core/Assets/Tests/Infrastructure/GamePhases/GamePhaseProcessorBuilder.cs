using System;
using WH40K.Events;

namespace Editor.Infrastructure.GamePhases
{
    public class GamePhaseProcessorBuilder<T> : TestDataBuilder<T> where T : class
    {
        private IGamePhase _gamePhase;

        public GamePhaseProcessorBuilder()
        {
        }
        public GamePhaseProcessorBuilder<T> WithGamePhase(IGamePhase gamePhase)
        {
            _gamePhase = gamePhase;
            return this;
        }
        public override T Build()
        {
            return Activator.CreateInstance(typeof(T), _gamePhase ??= A.GamePhase.Build()) as T;
        }
    }
}
