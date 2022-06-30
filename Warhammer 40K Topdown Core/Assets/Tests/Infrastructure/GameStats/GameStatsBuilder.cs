using System;
using UnityEngine;
using WH40K.Stats;
using WH40K.Stats.Player;

namespace Editor.Infrastructure.GameStatss
{
    public class GameStatsBuilder : TestDataBuilder<GameStatsSO>
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

        public override GameStatsSO Build()
        {
            BindSettings();
            Container.Bind<GameStatsSO>().AsSingle();
            
            return Container.Resolve<GameStatsSO>();
        }

        private void BindSettings()
        {
            Container.Bind<GameStatsSO.Settings>().AsSingle();
            var gameStatsSettings = Container.Resolve<GameStatsSO.Settings>();

            gameStatsSettings.Turn = 1;
            gameStatsSettings.ActivePlayer = _activePlayer ??= A.Player;
            gameStatsSettings.EnemyPlayer = _enemyPlayer ??= A.Player;
            gameStatsSettings.ActiveUnit = _activeUnit;
            gameStatsSettings.EnemyUnit = _enemyUnit;
            gameStatsSettings.GameTable = _gameTable ??= A.GameTable;
        }
    }
}
