using Editor.Infrastructure.Player;

namespace Editor.Infrastructure
{
    public static partial class A
    {
        public static MovementRangeBuilder MovementRange => new MovementRangeBuilder();
        public static NavMeshPathPositionBuilder PathPosition => new NavMeshPathPositionBuilder();
        public static RangeControllerBuilder RangeController => new RangeControllerBuilder();
        public static UnitBuilder Unit => new UnitBuilder();
        public static PlayerBuilder Player => new PlayerBuilder();
        public static PathCalculatorBuilder PathCalculator => new PathCalculatorBuilder();
        public static UnitMoverBuilder UnitMover => new UnitMoverBuilder();
        public static UnitMovementControllerBuilder UnitMovementController => new UnitMovementControllerBuilder();
        public static UnitSelectorBuilder UnitSelector => new UnitSelectorBuilder();
    }
}
