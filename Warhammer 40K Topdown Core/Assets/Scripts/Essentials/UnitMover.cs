﻿using System.Collections;
using UnityEngine;


namespace WH40K.Essentials
{
    [RequireComponent(typeof(IPathCalculator))]
    public class UnitMover : MonoBehaviour, IUnitMover
    {
        private IUnit _unit;
        private UnitMovementController _moveController;
        private MovementRange _movementRange;

        public Vector3 CurrentPosition => transform.position;
        public IUnit Unit => _unit;
        public float MaxDistance => _unit.Movement;
        public IPathCalculator PathCalculator => _unit.PathCalculator;
        public bool IsAgentStopped => PathCalculator.AgentIsStopped;
        public MovementRange MovementRange => _movementRange;
        public float Range => MovementRange.MoveRange;
        public UnitMovementController MoveController => _moveController;

        public void Awake()
        {
            _unit = GetComponent<IUnit>();
            _moveController = new UnitMovementController(this);
            _movementRange = new MovementRange(MaxDistance);
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