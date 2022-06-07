using Editor.Infrastructure.GamePhases;
using WH40K.GamePhaseEvents;

namespace Editor.Infrastructure
{
    public static partial class A
    {
        public static GamePhaseProcessorBuilder<MovementPhaseProcessor> MovementPhaseProcessor => new GamePhaseProcessorBuilder<MovementPhaseProcessor>();
        public static GamePhaseProcessorBuilder<ShootingPhaseProcessor> ShootingPhaseProcessor => new GamePhaseProcessorBuilder<ShootingPhaseProcessor>();
        public static ShootingSubPhaseProcessorBuilder ShootingSubPhaseProcessor => new ShootingSubPhaseProcessorBuilder();
    }
    public static partial class An
    {
        public static IPhaseBuilder IPhaseEvent => new IPhaseBuilder();
    }
}
