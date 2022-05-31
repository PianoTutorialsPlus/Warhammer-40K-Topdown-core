using UnityEngine;

namespace WH40K.PlayerEvents
{
    public class MovementRange
    {
        protected const float DistanceTolerance = 0.02f;
        private float _maxRange;
        private float _deltaRange;
        private Vector3 _startPosition;
        private Vector3 _currentPosition;

        public Vector3 StartPosition => _startPosition;
        public Vector3 CurrentPosition => _currentPosition;
        public float MoveRange => Mathf.Max(0, _maxRange - MovedDistance);
        public float MovedDistance => _deltaRange + (_startPosition - _currentPosition).magnitude;
        public bool IsMoveRangeZero => MoveRange <= DistanceTolerance;

        public MovementRange(float maxRange)
        {
            _maxRange = maxRange;
        }
        public void SetStartPosition(Vector3 position)
        {
            _startPosition = position;
            _currentPosition = position;
        }

        public void UpdateRange()
        {
            _deltaRange += (_startPosition - _currentPosition).magnitude;
            _startPosition = _currentPosition;
        }

        public void UpdatePosition(Vector3 currentPosition)
        {
            _currentPosition = currentPosition;
        }
        public void ResetRange()
        {
            _deltaRange = 0;
        }
    }
}