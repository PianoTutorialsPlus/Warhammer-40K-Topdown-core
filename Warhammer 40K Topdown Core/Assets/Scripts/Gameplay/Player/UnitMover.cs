using System.Collections;
using UnityEngine;
using WH40K.NavMesh;
using Zenject;

namespace WH40K.Gameplay.PlayerEvents
{
    //[RequireComponent(typeof(IPathCalculator))]
    public class UnitMover : MonoBehaviour//, IUnitMover
    {
        private PathCalculator _pathCalculator;
        private UnitMovementController _moveController;
        private UnitModel _model;
        private MovementRange _movementRange;

        private Vector3 _currentPosition => _model.Position;
        private bool _isAgentStopped => _pathCalculator.AgentIsStopped;

        [Inject]
        public void Construct(
            MovementRange movementRange,
            PathCalculator pathCalculator,
            UnitMovementController moveController,
            UnitModel model)
        {
            _pathCalculator = pathCalculator;
            _movementRange = movementRange;
            _moveController = moveController;
            _model = model;
        }

        public void SetDestination(Vector3 position)
        {
            if (!_isAgentStopped)
            {
                _moveController.SetDestination(position);
                StartCoroutine(GoToCoroutine());
            }
        }
        private IEnumerator GoToCoroutine()
        {
            while (true)
            {
                _movementRange.UpdatePosition(_currentPosition);
                _moveController.FreezeUnitsWithZeroMoveDistance();

                yield return null;
            }
        }
    }
}