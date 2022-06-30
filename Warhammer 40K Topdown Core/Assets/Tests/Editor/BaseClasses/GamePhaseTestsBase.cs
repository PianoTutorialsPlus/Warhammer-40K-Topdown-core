using Editor.Infrastructure;
using NSubstitute;
using WH40K.Gameplay.Events;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Stats;

namespace Editor.Base
{
    public class GamePhaseTestsBase : CoreElementsBase
    {
        public int counter;

        public IPhase GetIPhase()
        {

            return An.IPhaseEvent.Build();
        }
        public void SetHandlePhase(IPhase gamePhase)
        {
            gamePhase
                .When(x => x.HandlePhase())
                .Do(x => counter++);
        }
        public void SetClearPhase(IPhase gamePhase)
        {
            gamePhase
                .When(x => x.ClearPhase())
                .Do(x => counter++);
        }
        public void SetMovementPhaseProcessor(IPhase gamePhase, GameStatsSO gameStats = null)
        {
            MovementPhaseProcessor processor = A.MovementPhaseProcessor
                .WithGameStats(gameStats)
                .WithGamePhase(gamePhase);
            processor.SetPrivate(x => x.Initialized, false);
        }
        public void SetShootingPhaseProcessor(IPhase gamePhase, GameStatsSO gameStats = null)
        {
            ShootingPhaseProcessor processor = A.ShootingPhaseProcessor
                .WithGameStats(gameStats)
                .WithGamePhase(gamePhase);
            processor.SetPrivate(x => x.Initialized, false);
        }
    }
}
