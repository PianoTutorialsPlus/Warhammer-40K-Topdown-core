using System.Collections;
using UnityEngine;
using WH40K.NavMesh;
using Zenject;

namespace WH40K.PlayerEvents
{
    //[RequireComponent(typeof(IPathCalculator))]
    public class UnitMover : MonoBehaviour//, IUnitMover
    {
        private PathCalculator _pathCalculator;
        private UnitMovementController _moveController;
        private MovementRange _movementRange;

        public Vector3 CurrentPosition => transform.position;
        public bool IsAgentStopped => _pathCalculator.AgentIsStopped;
        public MovementRange MovementRange => _movementRange;
        public float Range => MovementRange.MoveRange;
        public UnitMovementController MoveController => _moveController;

        [Inject]
        public void Construct(
            MovementRange movementRange,
            PathCalculator pathCalculator,
            UnitMovementController moveController)
        {
            _pathCalculator = pathCalculator;
            _movementRange = movementRange;
            _moveController = moveController;
        }

        public void SetDestination(Vector3 position)
        {
            if (!IsAgentStopped)
            {
                MoveController.SetDestination(position);
                StartCoroutine(GoToCoroutine());
            }
        }
        private IEnumerator GoToCoroutine()
        {
            while (true)
            {
                MovementRange.UpdatePosition(CurrentPosition);
                MoveController.FreezeUnitsWithZeroMoveDistance();

                yield return null;
            }
        }
    }
}