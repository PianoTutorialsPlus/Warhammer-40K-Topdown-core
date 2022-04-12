namespace WH40K.Essentials
{
    public interface IGameStats
    {
        PlayerSO ActivePlayer { get ; set; }
        PlayerSO EnemyPlayer { get; set; }
    }
}