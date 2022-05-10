using UnityEngine;
using WH40K.Essentials;

namespace Editor.Infrastructure.GameStatss
{
    public class GameStatsBuilder
    {
        private PlayerSO _activePlayer;
        private IUnit _activeUnit;
        private GameTableSO _gameTable;
        private PlayerSO _enemyPlayer;
        private IUnit _enemyUnit;

        public GameStatsBuilder()
        {
        }

        public GameStatsBuilder WithActivePlayer(PlayerSO activePlayer)
        {
            _activePlayer = activePlayer;
            return this;
        }
        public GameStatsBuilder WithEnemyPlayer(PlayerSO enemyPlayer)
        {
            _enemyPlayer = enemyPlayer;
            return this;
        }
        public GameStatsBuilder WithActiveUnit(IUnit unit)
        {
            _activeUnit = unit;
            return this;
        }
        public GameStatsBuilder WithEnemyUnit(IUnit unit)
        {
            _enemyUnit = unit;
            return this;
        }
        public GameStatsBuilder WithGameTable(GameTableSO gameTable)
        {
            _gameTable = gameTable;
            return this;
        }

        public void Build()
        {
            GameStats.ActivePlayer = _activePlayer ??= A.Player;
            GameStats.EnemyPlayer = _enemyPlayer ??= A.Player;
            GameStats.ActiveUnit = _activeUnit;
            GameStats.EnemyUnit = _enemyUnit;
            GameStats.GameTable = _gameTable ??= A.GameTable;
        }
    }
}
