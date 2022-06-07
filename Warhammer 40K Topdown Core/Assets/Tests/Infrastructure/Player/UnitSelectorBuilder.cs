using WH40K.Core;
using WH40K.PlayerEvents;

namespace Editor.Infrastructure.Player
{
    public class UnitSelectorBuilder : TestDataBuilder<UnitSelector>
    {
        private IUnit _unit;
        private Fraction _playerFraction;
        private Fraction _enemyFraction;

        public UnitSelectorBuilder()
        {
        }

        public UnitSelectorBuilder WithUnit(IUnit unit)
        {
            _unit = unit;
            return this;
        }
        public UnitSelectorBuilder WithPlayerFraction(Fraction playerFraction)
        {
            _playerFraction = playerFraction;
            return this;
        }
        public UnitSelectorBuilder WithEnemyFraction(Fraction enemyFraction)
        {
            _enemyFraction = enemyFraction;
            return this;
        }
        public override UnitSelector Build()
        {
            A.GameStats
                .WithActivePlayer(A.Player.WithFraction(_playerFraction))
                .WithEnemyPlayer(A.Player.WithFraction(_enemyFraction)).Build();
            
            Container.BindInstance(_unit ??= A.Unit.Build());
            Container.Bind<UnitSelector>().AsSingle();

            return Container.Resolve<UnitSelector>();
        }
    }

}
