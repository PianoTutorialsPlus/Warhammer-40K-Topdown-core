using Editor.Infrastructure.Combat;
using Editor.Infrastructure.EventChannels;
using Editor.Infrastructure.Events;
using Editor.Infrastructure.GamePhases;
using Editor.Infrastructure.GameStats;
using Editor.Infrastructure.Player;

namespace Editor.Infrastructure
{
    public static class A
    {
        public static MovementRangeBuilder MovementRange => new MovementRangeBuilder();
        public static NavMeshPathPositionBuilder PathPosition => new NavMeshPathPositionBuilder();
        public static UnitPropertiesBuilder UnitProperties => new UnitPropertiesBuilder();
        public static CombatResultsBuilder CombatResult => new CombatResultsBuilder();
        public static ShotsBuilder Shot => new ShotsBuilder();
        public static WoundTableBuilder WoundTable => new WoundTableBuilder();
        public static CalculateHitsBuilder CalculateHits => new CalculateHitsBuilder();
        public static CalculateWoundsBuilder CalculateWounds => new CalculateWoundsBuilder();
        public static RangeControllerBuilder RangeController => new RangeControllerBuilder();
        public static UIDisplayInfoEventBuilder UIDisplayInfoEvent => new UIDisplayInfoEventBuilder();
        public static InfoUIEventChannelBuilder InfoUIEventChannel => new InfoUIEventChannelBuilder();
        public static UIDisplayInteractionEventBuilder UIDisplayInteractionEvent => new UIDisplayInteractionEventBuilder();
        public static InteractionUIEventChannelBuilder InteractionUIEventChannel => new InteractionUIEventChannelBuilder();
        public static RollTheDiceEventChannelBuilder RollTheDiceEventChannel => new RollTheDiceEventChannelBuilder();
        public static UIMovementRangeEventBuilder UIMovementRangeEvent => new UIMovementRangeEventBuilder();
        public static IndicatorUIEventChannel IndicatorUIEventChannel => new IndicatorUIEventChannel();
        public static BattleRoundEventChannelBuilder BattleRoundEventChannel => new BattleRoundEventChannelBuilder();
        public static UnitBuilder Unit => new UnitBuilder();
        public static PlayerBuilder Player => new PlayerBuilder();
        public static GameStatsBuilder GameStats => new GameStatsBuilder();
        public static GamePhaseBuilder GamePhase => new GamePhaseBuilder();
        public static MovementPhaseProcessorBuilder MovementPhaseProcessor => new MovementPhaseProcessorBuilder();
        public static ShootingPhaseProcessorBuilder ShootingPhaseProcessor => new ShootingPhaseProcessorBuilder();
        public static PathCalculatorBuilder PathCalculator => new PathCalculatorBuilder();
        public static UnitMoverBuilder UnitMover => new UnitMoverBuilder();
        public static UnitMovementControllerBuilder UnitMovementController => new UnitMovementControllerBuilder();
        public static PointerEventDataBuilder PointerEventData => new PointerEventDataBuilder();
        public static UnitSelectorBuilder UnitSelector => new UnitSelectorBuilder();
        public static GameTableBuilder GameTable => new GameTableBuilder();
        
    }
    public static class An
    {
        public static UIEventBuilder UIEvent => new UIEventBuilder();
        public static BattleRoundEventBuilder BattleRoundEvent => new BattleRoundEventBuilder();
        public static IPhaseBuilder IPhaseEvent => new IPhaseBuilder();
        public static IResultsBuilder IResultEvent => new IResultsBuilder();
    }
}