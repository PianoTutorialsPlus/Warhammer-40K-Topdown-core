using WH40K.Gameplay.PlayerEvents;
using WH40K.NavMesh;

namespace Editor.Infrastructure.Player
{
    public class UnitMovementControllerBuilder : TestDataBuilder<UnitMovementController>
    {
        private IUnitMover _unitMover;
        private IPathCalculator _pathCalculator;
        private IUnit _unit;
        private MovementRange _movementRange;

        public UnitMovementControllerBuilder()
        {
        }
        public UnitMovementControllerBuilder WithUnitMover(IUnitMover unitMover)
        {
            _unitMover = unitMover;
            return this;
        }
        public UnitMovementControllerBuilder WithPathCalculator(IPathCalculator pathCalculator)
        {
            _pathCalculator = pathCalculator;
            return this;
        }
        public UnitMovementControllerBuilder WithMovementRange(MovementRange movementRange)
        {
            _movementRange = movementRange;
            return this;
        }
        public UnitMovementControllerBuilder WithUnit(IUnit unit)
        {
            _unit = unit;
            return this;
        }

        public override UnitMovementController Build()
        {
            Container.BindInstance(_unit ??= A.Unit.Build()).AsSingle();
            Container.BindInstance(_movementRange ??= A.MovementRange).AsSingle().IfNotBound();
            Container.BindInstance(_pathCalculator ??= A.PathCalculator.Build()).AsSingle().IfNotBound();
            
            Container.Bind<UnitMovementController>().AsSingle();
            return Container.Resolve<UnitMovementController>();
        }
    }
}
