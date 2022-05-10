using Editor.Infrastructure.GameStatss;

namespace Editor.Infrastructure
{
    public static partial class A
    {
        public static GameStatsBuilder GameStats => new GameStatsBuilder();
        public static GameTableBuilder GameTable => new GameTableBuilder();
    }
}