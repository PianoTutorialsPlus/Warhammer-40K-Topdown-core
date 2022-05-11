using UnityEngine;
using WH40K.PlayerEvents;

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
    }
}
