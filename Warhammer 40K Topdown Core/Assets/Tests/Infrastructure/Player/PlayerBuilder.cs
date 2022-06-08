using System;
using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats.Player;

namespace Editor.Infrastructure.Player
{
    public class PlayerBuilder : TestDataBuilder<PlayerSO>
    {
        private Fraction _fraction;

        public PlayerBuilder()
        {
        }
        public PlayerBuilder WithFraction(Fraction fraction)
        {
            _fraction = fraction;
            return this;
        }

        public override PlayerSO Build()
        {
            var player = ScriptableObject.CreateInstance<PlayerSO>();
            player.Fraction = _fraction;
            return player;
        }

        internal PlayerSO WithFraction(object enemyFraction)
        {
            throw new NotImplementedException();
        }
    }
}
