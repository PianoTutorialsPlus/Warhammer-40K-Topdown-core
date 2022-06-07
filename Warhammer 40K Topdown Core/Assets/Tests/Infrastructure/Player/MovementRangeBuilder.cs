using UnityEngine;
using WH40K.PlayerEvents;

namespace Editor.Infrastructure.Player
{
    public class MovementRangeBuilder : TestDataBuilder<MovementRange>
    {
        private float _maxRange;
        private Vector3 _startPosition;
        private Vector3 _currentPosition;

        public MovementRangeBuilder()
        {
        }

        public MovementRangeBuilder WithMaxRange(float maxRange)
        {
            _maxRange = maxRange;
            return this;
        }

        public MovementRangeBuilder WithStartPosition(Vector3 startPosition)
        {
            _startPosition = startPosition;
            return this;
        }
        public MovementRangeBuilder WithCurrentPosition(Vector3 currentPosition)
        {
            _currentPosition = currentPosition;
            return this;
        }

        public override MovementRange Build()
        {
            Container.Bind<MovementRange>().AsSingle().WithArguments(_maxRange);
            var movementRange = Container.Resolve<MovementRange>();
            movementRange.SetStartPosition(_startPosition);
            movementRange.UpdatePosition(_currentPosition != Vector3.zero ? _currentPosition : _startPosition);

            return movementRange;
        }
    }
}