using Editor.Infrastructure.Combat;

namespace Editor.Infrastructure
{
    public static partial class A
    {
        public static CombatResultsBuilder CombatResult => new CombatResultsBuilder();
        public static ShotsBuilder Shot => new ShotsBuilder();
        public static WoundsBuilder Wound => new WoundsBuilder();
        public static WoundTableBuilder WoundTable => new WoundTableBuilder();
        public static CombatProcessorBuilder CombatProcessor => new CombatProcessorBuilder();
    }
    public static partial class An
    {
        public static IResultsBuilder IResultEvent => new IResultsBuilder();
    }
}
