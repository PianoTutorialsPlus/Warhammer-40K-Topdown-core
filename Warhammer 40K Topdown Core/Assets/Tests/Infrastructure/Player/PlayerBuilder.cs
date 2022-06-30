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
            BindSettings();
            Container.Bind<PlayerSO>().AsSingle();
            
            return Container.Resolve<PlayerSO>(); ;
        }

        private void BindSettings()
        {
            Container.Bind<PlayerSO.Settings>().AsSingle();
            var playerSettings = Container.Resolve<PlayerSO.Settings>();
            playerSettings.Fraction = _fraction;
        }
    }
}
