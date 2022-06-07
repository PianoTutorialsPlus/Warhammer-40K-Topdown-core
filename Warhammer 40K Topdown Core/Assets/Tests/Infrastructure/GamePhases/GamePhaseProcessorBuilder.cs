using System;
using WH40K.Events;

namespace Editor.Infrastructure.GamePhases
{
    public class GamePhaseProcessorBuilder<T> : TestDataBuilder<T> where T : class
    {
        private IPhase _gamePhase;

        public GamePhaseProcessorBuilder()
        {
        }
        public GamePhaseProcessorBuilder<T> WithGamePhase(IPhase gamePhase)
        {
            _gamePhase = gamePhase;
            return this;
        }
        public override T Build()
        {
            Container.Bind<T>().AsSingle()
                .WithArguments(_gamePhase ??= An.IPhaseEvent.Build());
            return Container.Resolve<T>();
        }
    }
}
