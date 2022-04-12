namespace Editor.Infrastructure
{
    public static class A
    {
        public static MovementRangeBuilder MovementRange => new MovementRangeBuilder();
        public static NavMeshPathPositionBuilder PathPosition => new NavMeshPathPositionBuilder();
        public static UnitPropertiesBuilder UnitProperties => new UnitPropertiesBuilder();
        public static CombatResultsBuilder DiceResult => new CombatResultsBuilder();
        public static RangeControllerBuilder RangeController => new RangeControllerBuilder();
        public static UIDisplayInfoEventBuilder UIDisplayInfoEvent => new UIDisplayInfoEventBuilder();
        public static UnitBuilder Unit => new UnitBuilder();
    }
    public static class An
    {
        public static InfoUIEventChannelBuilder InfoUIEventChannel => new InfoUIEventChannelBuilder();
        public static UIEventBuilder UIEvent => new UIEventBuilder();
    }
}