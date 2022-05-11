using Editor.Infrastructure;
using NSubstitute;
using WH40K.Events;
using WH40K.GamePhaseEvents;

namespace Editor.GameMechanics
{
    public class GamePhaseTestsBase
    {
        public int counter;

        public IGamePhase GetGamePhase()
        {
            GetGameStats();
            return A.GamePhase
                .WithBattleroundEvent(An.IPhaseEvent.Build())
                .Build();
        }
        public void GetGameStats()
        {
            A.GameStats
                    .WithActiveUnit(A.Unit.Build())
                    .Build();
        }
        public void SetHandlePhase(IGamePhase gamePhase)
        {
            gamePhase.BattleroundEvents
                .When(x => x.HandlePhase())
                .Do(x => counter++);
        }
        public void SetClearPhase(IGamePhase gamePhase)
        {
            gamePhase.BattleroundEvents
                .When(x => x.ClearPhase())
                .Do(x => counter++);
        }
        public void SetMovementPhaseProcessor(IGamePhase gamePhase)
        {
            MovementPhaseProcessor processor = A.MovementPhaseProcessor.WithGamePhase(gamePhase);
            processor.SetPrivate(x => x.Initialized, false);
        }
        public void SetShootingPhaseProcessor(IGamePhase gamePhase)
        {
            ShootingPhaseProcessor processor = A.ShootingPhaseProcessor.WithGamePhase(gamePhase);
            processor.SetPrivate(x => x.Initialized, false);
        }
    }
}
