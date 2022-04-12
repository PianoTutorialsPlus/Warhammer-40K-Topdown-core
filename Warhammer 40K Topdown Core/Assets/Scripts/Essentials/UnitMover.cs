using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace WH40K.Essentials
{
    public class UnitMover : MonoBehaviour, IUnitMover
    {
        public IPathCalculator PathCalculator { get; private set; }

        private IStats _unit;
        public Unit Unit { get; set; }
        public UnitMovementController MoveController;
        public MovementRange MovementRange { get; set; }

        public float MaxDistance => _unit.Movement;

        public float MoveDistance => MovementRange.MoveRange;
        public float MovedDistance => MovementRange.MovedDistance;
        public bool IsAgentStopped => PathCalculator.AgentIsStopped;
        private bool LoopBreakConditions => MovementRange.IsMoveRangeZero;
        public Vector3 CurrentPosition => transform.position;

        public void Awake()
        {
            PathCalculator = GetComponent<IPathCalculator>();
            Unit = GetComponent<Unit>();
            _unit = Unit;
            MoveController = new UnitMovementController(this);
            MovementRange = new MovementRange(MaxDistance);
            //Debug.Log(Unit.name);
        }
        public void Initialize(IPathCalculator pathCalculator, IStats unit)
        {
           // PathCalculator = pathCalculator;
            //_unit = unit;
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

                if (LoopBreakConditions)
                {
                    MovementRange.UpdateRange();
                    break;
                }
                yield return null;
            }
        }
        //private void SetStaticDistance(Vector3 position)
        //{
        //    staticDistance = PathCalculator.GetDistance(position) > MoveDistance
        //        ? MoveDistance
        //        : PathCalculator.GetDistance(position);

        //    startPosition = transform.position;
        //}
        //private void SetDistanceToMove()
        //{
        //    movedDistance = (startPosition - currentPosition).magnitude;
        //    distanceToMove = staticDistance - movedDistance;

        //}
        //private void SetNavMeshDestination(Vector3 position)
        //{
        //    PathCalculator.SetDestination(position);
        //}
        //private void SetMoveDistance()
        //{
        //    MoveDistance -= movedDistance;
        //}

        //public void FreezeUnitsWithZeroMoveDistance()
        //{
        //    if (IsMoveDistanceZero)
        //    {
        //        PathCalculator.FreezeAgent();
        //        Unit.Freeze();
        //    }
        //}

        //public float MovedInstance
        //{
        //    get
        //    {
        //        return distanceToMove - GetRemainingDistance();
        //    }
        //    protected set {; }
        //}

        //public float DistanceToMove => CalculateDistanceToMove(position);


        //public void SetMovedDistance()
        //{
        //    //if (IsRemainingDistanceZero() && IsPathCalculated())
        //    //{

        //    MovedDistance += distanceToMove;
        //    //return movedDistance;//
        //        //movedInstance = 0;
        //        //isMoving = false;
        //        //break;
        //    //}
        //}

        //private float CalculateDistanceInCoroutine()
        //{
        //    var rest = RemainingDistance > movedDistance
        //        ? restDistance
        //        : 0;
        //    return RemainingDistance + rest;
        //}
        //private float CalculateDistanceStatic()
        //{
        //    return staticDistance > movedDistance
        //                ? movedDistance
        //                : staticDistance;
        //}
        //private void CalculateDeltaDistance()
        //{
        //    restDistance = movedDistance - staticDistance;
        //}
        //private void AddMoveDistance()
        //{
        //    movedDistance = PathCalculator.IsPathCalculated
        //        ? CalculateMoveDistanceInCoroutine()
        //        : movedDistance;

        //    FreezeUnitsWithZeroMoveDistance();
        //}
        //private float CalculateMoveDistanceInCoroutine()
        //{
        //    var rest = RemainingDistance < movedDistance
        //       ? restDistance
        //       : 0;
        //    return distanceToMove + rest;
        //}




        //public virtual void SetDestination(Vector3 position)
        //{
        //    if (!IsAgentStopped)
        //    {
        //        AddDistance(MovedInstance);
        //        SetNavMeshDestination(position);
        //        StartCoroutine(GoToCoroutine(position));
        //    }
        //}
        //private void AddDistance(float distanceToAdd)
        //{
        //    MovedDistance += distanceToAdd;
        //    MovedInstance = 0;
        //}
        //public void SetNavMeshDestination(Vector3 position)
        //{
        //    CalculateDistanceToMove(position);
        //    //distanceToMove = PathCalculator.GetDistance(position);
        //    PathCalculator.SetDestination(position);
        //}
        //public IEnumerator GoToCoroutine(Vector3 position)
        //{
        //    //if (AgentIsStopped()) yield return null;
        //    //AddMovedDistance();
        //    //SetNavMeshDestination(position);

        //    while (true)
        //    {
        //        yield return new WaitForEndOfFrame();

        //        ObserveMovingDistance();
        //        CalculateMovedDistance();

        //        if (LoopBreakConditions()) break;
        //    }
        //}

        ////public virtual void AddMovedInstance()
        ////{
        ////    //MovedDistance += MovedInstance;// (distanceToMove - m_Agent.remainingDistance);
        ////    AddDistance(MovedInstance);
        ////    //MovedInstance = 0

        ////}

        //public void ObserveMovingDistance()
        //{
        //    if (IsPathCalculated())
        //    {
        //        //Debug.Log(m_Agent.pathPending);
        //        isMoving = true;
        //        CalculateMovingDistance();
        //        FreezeUnitsWithZeroMoveDistance();
        //    }
        //}
        //public void CalculateMovedDistance()
        //{
        //    if (IsRemainingDistanceZero() && IsPathCalculated())
        //    {
        //        AddDistance(distanceToMove);
        //        //MovedInstance = 0;
        //        isMoving = false;
        //        //break;
        //    }
        //}

        //public void CalculateDistanceToMove(Vector3 position)
        //{
        //    distanceToMove = PathCalculator.GetDistance(position);
        //}

        //public bool LoopBreakConditions()
        //{
        //    return IsMoveDistanceZero|| (IsRemainingDistanceZero() && IsPathCalculated());
        //}
        //public bool IsRemainingDistanceZero()
        //{
        //    return GetRemainingDistance() <= 0;
        //}
        //public bool IsPathCalculated()
        //{
        //    return PathCalculator.IsPathCalculated;
        //}
        //public void CalculateMovingDistance()
        //{
        //    CalculateRestDistance();
        //    MovedInstance = (distanceToMove - GetRemainingDistance());
        //}
        //private void CalculateRestDistance()
        //{
        //    RestDistance = (MoveDistance - MovedDistance - MovedInstance);
        //}
        //public float GetRemainingDistance()
        //{
        //    return PathCalculator.RemainingDistance;
        //}
        ////public void FreezeUnitsWithoutRestDistance()
        ////{
        ////    if (IsRestDistanceZero())
        ////    {
        ////        Unit.Freeze();
        ////        PathCalculator.ResetPath();
        ////        SetMovementPhaseEvent.RaiseEvent(_gameStats);
        ////        //break;
        ////    }
        ////}
        ////public bool IsRestDistanceZero()
        ////{
        ////    return RestDistance <= 0;
        ////}


    }
}