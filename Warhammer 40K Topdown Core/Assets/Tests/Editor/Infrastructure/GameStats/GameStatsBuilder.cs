﻿using UnityEngine;
using WH40K.Essentials;

namespace Editor.Infrastructure.GameStats
{
    public class GameStatsBuilder : TestDataBuilder<GameStatsSO>
    {
        private PlayerSO _activePlayer;
        private IUnit _activeUnit;
        private GameTableSO _gameTable;

        public GameStatsBuilder()
        {
        }

        public GameStatsBuilder WithActivePlayer(PlayerSO activePlayer)
        {
            _activePlayer = activePlayer;
            return this;
        }
        public GameStatsBuilder WithActiveUnit(IUnit unit)
        {
            _activeUnit = unit;
            return this;
        }
        public GameStatsBuilder WithGameTable(GameTableSO gameTable)
        {
            _gameTable = gameTable;
            return this;
        }

        public override GameStatsSO Build()
        {
            var gameStats = ScriptableObject.CreateInstance<GameStatsSO>();
            gameStats.ActivePlayer = _activePlayer??= A.Player;
            gameStats.ActiveUnit = _activeUnit;
            //gameStats.GameTable = _gameTable ??= A.GameTable;
            return gameStats;
        }
    }
}
