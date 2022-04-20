using UnityEngine;
using WH40K.Essentials;

namespace Editor.Infrastructure
{
    public class GameStatsBuilder : TestDataBuilder<GameStatsSO>
    {
        private PlayerSO _activePlayer;
        private IUnit _activeUnit;

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

        public override GameStatsSO Build()
        {
            var gameStats = ScriptableObject.CreateInstance<GameStatsSO>();
            gameStats.ActivePlayer = _activePlayer;
            gameStats.ActiveUnit = _activeUnit;
            return gameStats;
        }
    }
}
