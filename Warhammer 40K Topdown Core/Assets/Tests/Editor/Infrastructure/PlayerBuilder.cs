using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WH40K.Essentials;

namespace Editor.Infrastructure
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
