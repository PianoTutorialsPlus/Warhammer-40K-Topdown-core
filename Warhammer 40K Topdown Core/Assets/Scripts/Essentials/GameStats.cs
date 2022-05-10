namespace WH40K.Essentials
{
    public static class GameStats
    {
        public static int Turn { get; set; }
        public static PlayerSO ActivePlayer { get ; set ; }
        public static PlayerSO EnemyPlayer { get; set; }
        public static IUnit ActiveUnit { get; set; }
        public static IUnit EnemyUnit { get; set; }
        public static GameTableSO GameTable { get; set; }
        public static GamePhase Phase { get; set; }
    }
}
