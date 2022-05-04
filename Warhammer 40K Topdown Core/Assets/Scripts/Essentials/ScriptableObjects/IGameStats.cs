﻿namespace WH40K.Essentials
{
    public interface IGameStats
    {
        PlayerSO ActivePlayer { get; set; }
        PlayerSO EnemyPlayer { get; set; }
        IUnit ActiveUnit { get; set; }
        GameTableSO GameTable { get; set; }
    }
}