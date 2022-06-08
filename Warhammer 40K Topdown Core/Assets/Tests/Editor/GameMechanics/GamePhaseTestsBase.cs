using Editor.Infrastructure;
using NSubstitute;
using WH40K.Gameplay.Events;
using WH40K.Gameplay.GamePhaseEvents;

namespace Editor.GameMechanics
{
    public class GamePhaseTestsBase
    {
        public int counter;

        public IPhase GetIPhase()
        {
            GetGameStats();
            return An.IPhaseEvent.Build();
        }
        public void GetGameStats()
        {
            A.GameStats
                    .WithActiveUnit(A.Unit.Build())
                    .Build();
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
        public void SetMovementPhaseProcessor(IPhase gamePhase)
        {
            MovementPhaseProcessor processor = A.MovementPhaseProcessor.WithGamePhase(gamePhase);
            processor.SetPrivate(x => x.Initialized, false);
        }
        public void SetShootingPhaseProcessor(IPhase gamePhase)
        {
            ShootingPhaseProcessor processor = A.ShootingPhaseProcessor.WithGamePhase(gamePhase);
            processor.SetPrivate(x => x.Initialized, false);
        }
    }
}
