using System.Collections;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    protected const float DistanceTolerance = 0.02f;
    public IPathCalculator PathCalculator;
    public DistanceCalculator DistanceCalculator;
    public Unit Unit;

    //private bool isMoving;
    //float restDistance = 1;
    //float movedInstance = 0;
    //float movedDistance = 0;

    float maxDistance;
    [SerializeField] float distanceToMove = 0;

    //public float RestDistance { get; protected set; }
    //public float MovedDistance { get; protected set; }
    //public float MoveDistance => maxDistance;
    public float DistanceToMove => distanceToMove;
    public bool IsMoveDistanceZero => DistanceToMove <= DistanceTolerance;
    //public bool IsMoving { get; protected set; }
    public float MoveDistance { get; protected set; }
    public bool IsAgentStopped => PathCalculator.AgentIsStopped;
    private bool LoopBreakConditions => IsMoveDistanceZero;
    private float RemainingDistance => PathCalculator.RemainingDistance;

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

    //public GameStatsSO _gameStats;
    private float staticDistance;
    private float restDistance;

    public void Awake()
    {
        Unit = GetComponent<Unit>();
        Debug.Log(Unit.name);
    }
    public void Initialize(IPathCalculator pathCalculator, IUnitStats unit)
    {
        PathCalculator = pathCalculator;
        maxDistance = unit.Movement;
        MoveDistance = maxDistance;
    }
    public void SetDestination(Vector3 position)
    {
        if (!IsAgentStopped)
        {
            SetStaticDistance(position);

            SetDistanceToMove();
            SetNavMeshDestination(position);
            StartCoroutine(GoToCoroutine());
        }
    }

    private void SetStaticDistance(Vector3 position)
    {
        staticDistance = PathCalculator.GetDistance(position);
        CalculateDeltaDistance();
    }
    private void SetDistanceToMove()
    {
        distanceToMove = PathCalculator.IsPathCalculated
            ? CalculateDistanceInCoroutine()
            : CalculateDistanceStatic();

        AddMoveDistance();
    }
    private void SetNavMeshDestination(Vector3 position)
    {
        PathCalculator.SetDestination(position);
    }
    private IEnumerator GoToCoroutine()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            SetDistanceToMove();
            if (LoopBreakConditions) break;
        }
    }
    private float CalculateDistanceInCoroutine()
    {
        var rest = RemainingDistance > MoveDistance
            ? restDistance
            : 0;
        return RemainingDistance + rest;
    }
    private float CalculateDistanceStatic()
    {
        return staticDistance > MoveDistance
                    ? MoveDistance
                    : staticDistance;
    }
    private void CalculateDeltaDistance()
    {
        restDistance = MoveDistance - staticDistance;
    }
    private void AddMoveDistance()
    {
        MoveDistance = PathCalculator.IsPathCalculated
            ? CalculateMoveDistanceInCoroutine()
            : MoveDistance;

        FreezeUnitsWithZeroMoveDistance();
    }
    private float CalculateMoveDistanceInCoroutine()
    {
        var rest = RemainingDistance < MoveDistance
           ? restDistance
           : 0;
        return distanceToMove + rest;
    }
    public void FreezeUnitsWithZeroMoveDistance()
    {
        if (IsMoveDistanceZero)
        {
            PathCalculator.FreezeAgent();
            Unit.Freeze();

            //PathCalculator.ResetPath();
        }
    }



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
