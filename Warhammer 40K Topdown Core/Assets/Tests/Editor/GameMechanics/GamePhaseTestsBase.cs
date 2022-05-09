using Editor.Infrastructure;
using NSubstitute;
using WH40K.Essentials;
using WH40K.GameMechanics;

namespace Editor.GameMechanics
{
    public class GamePhaseTestsBase
    {
        public int counter;

        public IGamePhase GetGamePhase()
        {
            return A.GamePhase
                .WithGameStats(A.GameStats
                    .WithActiveUnit(A.Unit.Build()))
                .WithBattleroundEvent(An.IPhaseEvent.Build())
                .Build();
        }
        public void SetHandlePhase(IGamePhase gamePhase)
        {
            gamePhase.BattleroundEvents
                .When(x => x.HandlePhase(Arg.Any<GameStatsSO>()))
                .Do(x => counter++);
        }
        public void SetClearPhase(IGamePhase gamePhase)
        {
            gamePhase.BattleroundEvents
                .When(x => x.ClearPhase(Arg.Any<GameStatsSO>()))
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
