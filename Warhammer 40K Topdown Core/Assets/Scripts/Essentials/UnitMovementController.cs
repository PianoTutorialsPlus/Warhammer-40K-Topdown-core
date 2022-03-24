using System;
using UnityEngine;

public class UnitMovementController
{
    
    private IUnitMover _unitMover;
    private MovementRange _movementRange => _unitMover.MovementRange;
    public float MoveDistance => _movementRange.MoveRange;
    public float MovedDistance => _movementRange.MovedDistance;

    private IPathCalculator PathCalculator => _unitMover.PathCalculator;
    public bool IsAgentStopped => PathCalculator.AgentIsStopped;
    public Unit Unit => _unitMover.Unit;
    public bool IsMoveDistanceZero => _movementRange.IsMoveRangeZero;
    public Vector3 CurrentPosition => _unitMover.CurrentPosition;

    public Vector3 EndPosition { get; private set; }

    public UnitMovementController(IUnitMover unitMover)
    {
        _unitMover = unitMover;
    }
    public void SetStartPosition(Vector3 position)
    {
        _movementRange.SetStartPosition(position);
    }
    public void SetDestination(Vector3 position)
    {
        if (!IsAgentStopped)
        {
            SetStartPosition(CurrentPosition);
            SetEndPosition(position);
            
            SetNavMeshDestination();
        }
    }
    private void SetEndPosition(Vector3 position)
    {
        Debug.Log(MoveDistance);
        PathCalculator.SetMoveDistance(MoveDistance);
        PathCalculator.SetEndPosition(position);

        EndPosition = PathCalculator.GetEndPosition(position);
    }

    public void UpdateMoveDistance()
    {
        FreezeUnitsWithZeroMoveDistance();
    }
    private void SetNavMeshDestination()
    {
        PathCalculator.SetDestination(EndPosition);
    }

    public void FreezeUnitsWithZeroMoveDistance()
    {
        if (IsMoveDistanceZero)
        {
            Debug.Log("FReee");
            PathCalculator.FreezeAgent();
            Unit.Freeze();
        }
    }
}