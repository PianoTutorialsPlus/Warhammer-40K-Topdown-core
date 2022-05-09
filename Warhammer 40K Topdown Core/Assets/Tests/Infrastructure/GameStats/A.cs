using Editor.Infrastructure.GameStats;

namespace Editor.Infrastructure
{
    public static partial class A
    {
        public static GameStatsBuilder GameStats => new GameStatsBuilder();
        public static GameTableBuilder GameTable => new GameTableBuilder();
    }
}