namespace Editor.Infrastructure
{
    public static class A
    {
        public static MovementRangeBuilder MovementRange => new MovementRangeBuilder();
        public static NavMeshPathPositionBuilder PathPosition => new NavMeshPathPositionBuilder();
        public static UnitPropertiesBuilder UnitProperties => new UnitPropertiesBuilder();
    }
}