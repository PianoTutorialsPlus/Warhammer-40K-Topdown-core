using NSubstitute;
using UnityEngine;
using WH40K.Essentials;

namespace Editor.Infrastructure.Player
{
    public class UnitMoverBuilder : TestDataBuilder<IUnitMover>
    {
        private float _maxDistance = 0;
        private Vector3 _currentPosition = Vector3.zero;
        private MovementRange _movementRange;
        private IPathCalculator _pathCalculator;

        public UnitMoverBuilder()
        {
        }
        public UnitMoverBuilder WithMaxDistance(float distance)
        {
            _maxDistance = distance;
            return this;
        }
        public UnitMoverBuilder WithCurrentPosition(Vector3 position)
        {
            _currentPosition = position;
            return this;
        }
        public UnitMoverBuilder WithMovementRange(MovementRange range)
        {
            _movementRange = range;
            return this;
        }
        public UnitMoverBuilder WithPathCalculator(IPathCalculator pathCalculator)
        {
            _pathCalculator = pathCalculator;
            return this;
        }

        public override IUnitMover Build()
        {
            var unitMover = Substitute.For<IUnitMover>();
            unitMover.MaxDistance.Returns(_maxDistance);
            unitMover.CurrentPosition.Returns(_currentPosition);
            unitMover.PathCalculator.Returns(_pathCalculator ??= A.PathCalculator.Build());
            unitMover.MovementRange = _movementRange ??= A.MovementRange.WithMaxRange(_maxDistance);
            return unitMover;
        }
    }
}
