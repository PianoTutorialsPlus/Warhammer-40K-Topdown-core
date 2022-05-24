using UnityEngine;
using WH40K.NavMesh;

namespace WH40K.PlayerEvents
{
    public class UnitMovementController
    {

        private MovementRange _movementRange;/* => _unitMover.MovementRange;*/
        public float MoveDistance => _movementRange.MoveRange;
        public float MovedDistance => _movementRange.MovedDistance;

        private IPathCalculator _pathCalculator;/* => _unitMover.PathCalculator;*/
        public bool IsAgentStopped => _pathCalculator.AgentIsStopped;
        public IUnit _unit;
        public bool IsMoveDistanceZero => _movementRange.IsMoveRangeZero;
        public Vector3 CurrentPosition => _unit.CurrentPosition;

        public Vector3 EndPosition { get; private set; }

        public UnitMovementController(
            IUnit unit,
            MovementRange movementRange,
            PathCalculator pathCalculator)
        {
            _unit = unit;
            _movementRange = movementRange;
            _pathCalculator = pathCalculator;
        }
        public void SetStartPosition(Vector3 position)
        {
            _movementRange.SetStartPosition(position);
        }
        public void SetDestination(Vector3 position)
        {
            if (!IsAgentStopped)
            {
                _movementRange.UpdateRange();
                SetStartPosition(CurrentPosition);
                SetEndPosition(position);

                SetNavMeshDestination();
            }
        }
        private void SetEndPosition(Vector3 position)
        {
            //PathCalculator.SetMoveDistance(MoveDistance);
            //PathCalculator.SetEndPosition(position);

            EndPosition = _pathCalculator.GetEndPosition(position, MoveDistance);
        }

        // Can Be Deleted
        public void UpdateMoveDistance()
        {
            FreezeUnitsWithZeroMoveDistance();
        }
        private void SetNavMeshDestination()
        {
            _pathCalculator.SetDestination(EndPosition);
        }

        public void FreezeUnitsWithZeroMoveDistance()
        {
            if (IsMoveDistanceZero)
            {
                _pathCalculator.FreezeAgent();
                _unit.Freeze();
            }
        }
    }
}