using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(NecronWarriorSO))]
public class Unit : MonoBehaviour // INHARITANCE
{
    public float speed = 3;
    public bool canMove = true;
    public bool activated = false;
    public bool canShoot = true;
    public bool singleton = false;
    [SerializeField] bool Stop => PathCalculator.AgentIsStopped;
    public bool isMoving = false;
    public bool IsSelected { get; set; }
    public bool done = false;

    public NavMeshAgent m_Agent;     // Moving behaviour
    //private NavMeshPath path;
    public float MoveDistance => _unitSO.Movement;
    public float movedDistance = 0;
    public int weaponRange;

    public Fraction Fraction => _unitSO.Fraction;

    public float distanceToMove;
    public float restDistance = 1;
    public string phase;
    float movedInstance = 0;

    public BattleroundEventChannelSO SetMovementPhaseEvent;

    [SerializeField] public WeaponSO _weaponSO;
    [SerializeField] public UnitSO _unitSO;
    //[SerializeField] public IUnitStats _unitSOTest;
    public GameStatsSO _gameStats;

    public InputReader _inputReader;

    public UnityAction<Unit> onTapDownAction;
    public UnityAction onPointerEnter;
    public UnityAction<Unit> onPointerEnterInfo;
    public UnityAction<Unit> onPointerExit;



    public bool IsDone => done;
    public IUnit unit => (IUnit)gameObject.GetComponent<Unit>();
    public Fraction ActivePlayerFraction => _gameStats.activePlayer.fraction;
    public Fraction EnemyFraction => _gameStats.enemyPlayer.fraction;

    public IUnitStats EnemyUnit { get => _gameStats.enemyUnitTest; set => _gameStats.enemyUnitTest = value; }
    public IUnitStats ActiveUnit { get => _gameStats.activeUnitTest; set => _gameStats.activeUnitTest = value; }


    public UnitSelector UnitSelector { get; protected set; }
    public UnitMover UnitMover;
    public IPathCalculator PathCalculator { get; protected set; }


    //public ActiveUnitSO activeUnit;

    public int test = 0;

    //public  GameObject distanceIndicator;

    protected void Awake()
    {

        //_unitSelector = new UnitSelector(_gameStats, gameObject.GetComponent<Unit>());
        //UnitMover = new UnitMover();
        m_Agent = GetComponent<NavMeshAgent>();
        PathCalculator = new PathCalculator(m_Agent);
        UnitSelector = new UnitSelector(ActivePlayerFraction, gameObject.GetComponent<IUnitStats>());
        UnitMover.Initialize(PathCalculator, (IUnitStats)_unitSO);

        //m_Agent = GetComponent<NavMeshAgent>();
        //m_Agent.speed = speed;
        //m_Agent.acceleration = 999;
        //m_Agent.angularSpeed = 999;
        //m_Agent.isStopped = false;
        canMove = true;
        isMoving = false;
        singleton = false;
        //moveDistance = _unitSO.Movement; //stats["Movement"];
        //restDistance = MoveDistance;
        //weaponRange = _weaponSO.Range; //weaponStats["Range"];
    }

    // Start is called before the first frame update
    void Start()
    {
        _unitSO.takenWounds = 0;
        //path = new NavMeshPath();

        //phase = "Movement Phase";
    }

    //public void OnEnable()
    //{
    //    Debug.Log("Enable");
    //}

    //public void OnDisable()
    //{
    //    Debug.Log("Disable");
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    //switch (phase)
    //    //{
    //    //    case "Movement Phase":
    //    //        if (canMove)
    //    //        {
    //    //            restDistance = moveDistance - movedDistance - (distanceToMove - m_Agent.remainingDistance);
    //    //        }

    //    //        if (restDistance <= 0)
    //    //        {
    //    //            Freeze();
    //    //        }

    //    //        if (canMove && m_Agent.remainingDistance <= 0 && singleton)
    //    //        {
    //    //            movedDistance += distanceToMove;
    //    //            distanceToMove = 0;
    //    //            singleton = false;
    //    //        }

    //    //        if (m_Agent.remainingDistance > 0 && m_Agent.hasPath)
    //    //        {
    //    //            singleton = true;
    //    //        }
    //    //        break;
    //    //    case "Shooting Phase":
    //    //        restDistance = weaponRange;

    //    //        break;
    //    //}


    //}

    //public virtual void AddMovedDistance()
    //{
    //    movedDistance += movedInstance;// (distanceToMove - m_Agent.remainingDistance);
    //    movedInstance = 0;
    //}

    //public virtual void SetDestination(Vector3 position)
    //{
    //    if (!m_Agent.isStopped)
    //    {
    //        StartCoroutine(GoTo(position));
    //    }
    //}

    //public IEnumerator GoTo(Vector3 position)
    //{
    //    if (!m_Agent.isStopped)
    //    {
    //        AddMovedDistance();
    //        GetDistance(position);
    //        m_Agent.SetDestination(position);

    //        while (true)
    //        {
    //            yield return new WaitForEndOfFrame();

    //            if (m_Agent.hasPath && !m_Agent.pathPending)
    //            {
    //                //Debug.Log(m_Agent.pathPending);
    //                isMoving = true;
    //                restDistance = moveDistance - movedDistance - (distanceToMove - m_Agent.remainingDistance);
    //                movedInstance = (distanceToMove - m_Agent.remainingDistance);

    //                if (restDistance <= 0)
    //                {
    //                    Freeze();
    //                    Debug.Log("restDistance");
    //                    m_Agent.ResetPath();
    //                    SetMovementPhaseEvent.RaiseEvent(_gameStats);
    //                    break;
    //                }
    //            }
    //            if (m_Agent.remainingDistance <= 0 && isMoving == true)
    //            {
    //                movedDistance += distanceToMove;
    //                movedInstance = 0;
    //                isMoving = false;
    //                break;
    //            }
    //        }
    //    }
    //}

    //public virtual void GetDistance(Vector3 position)
    //{
    //    m_Agent.CalculatePath(position, path);
    //    distanceToMove = GetPathLength(path);
    //}
    //public static float GetPathLength(NavMeshPath path)
    //{
    //    float lng = 0.0f;

    //    if ((path.status != NavMeshPathStatus.PathInvalid))
    //    {
    //        for (int i = 1; i < path.corners.Length; ++i)
    //        {
    //            lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
    //        }
    //    }

    //    return lng;
    //}
    public void ResetData()
    {
        //Debug.Log("Reset");
        //Debug.Log("Reset:" + name);
        //////canMove = true;
        //canShoot = true;
        //movedDistance = 0;
        //activated = false;
        //IsSelected = false;
        PathCalculator.ResetAgent();
        done = false;
    }
    public void PrepareMovementPhase()
    {
        //Debug.Log("ResetMovement");
        //m_Agent.isStopped = false;
        //restDistance = MoveDistance;
    }
    public void PrepareShootingPhase()
    {
        //Debug.Log("ResetShooting");
        m_Agent.isStopped = true;
        restDistance = weaponRange;
    }
    public virtual void Freeze()
    {
        ////canMove = false;
        //PathCalculator.FreezeAgent();
        Debug.Log("Freeze");
        //restDistance = 0;
        //distanceToMove = 0;
        ////movedDistance = moveDistance;
        //activated = false;
        //IsSelected = false;
        done = true;
        //_gameStats.movementSubPhase = MovementPhase.Selection;
        //SetMovementPhaseEvent.RaiseEvent(_gameStats);
    }



    public void Destroy()
    {
        Destroy(gameObject);
    }
}
public class UnitMovementPhase : Unit, IUnitActionPhase
{
    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        if (onPointerEnter != null) onPointerEnter();
        if (onPointerEnterInfo != null) onPointerEnterInfo(this);
        //if (onPointerEnterInfo != null) onPointerEnterInfo(gameObject.GetComponent<Unit>());
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        if (onPointerExit != null)
        {
            onPointerExit(this);
            //onPointerExit(gameObject.GetComponent<Unit>());
        }
    }

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        //if (onPointerEnter != null) onPointerEnter();
        if (onTapDownAction == null) return;
        if (pointerEvent.button == PointerEventData.InputButton.Left)
        {
            SelectUnit();
            onTapDownAction(this);
            //onTapDownAction(gameObject.GetComponent<Unit>());
        }
        // Shooting Phase
        //else if (pointerEvent.button == PointerEventData.InputButton.Right && _gameStats.activePlayer._playerUnits[0].tag == gameObject.tag)
        //{
        //    SetUnitAsEnemy();
        //}
    }

    // Shooting Phase
    public void SetUnitAsEnemy()
    {
        EnemyUnit = UnitSelector.GetUnit(EnemyFraction);
        //_gameStats.enemyUnit = gameObject.GetComponent<Unit>();
    }

    public void SelectUnit()
    {
        //IUnitStats test = unitSelector.GetUnit();
        //_gameStats.activeUnitTest = test;
        ActiveUnit = UnitSelector.GetUnit();
        SetIsSelected();
    }

    public void SetIsSelected()
    {
        IsSelected = UnitSelector.UnitIsFromFraction();
    }
}
public class UnitSelector
{
    private Fraction _playerFraction;
    private IUnitStats _unit;

    public UnitSelector(Fraction fraction, IUnitStats unit)
    {
        _playerFraction = fraction;
        _unit = unit;
    }

    public IUnitStats GetUnit(Fraction enemyFraction = Fraction.None)
    {
        return (UnitIsFromFraction(enemyFraction))
            ? _unit
            : null;
    }

    public bool UnitIsFromFraction(Fraction enemyFraction = Fraction.None)
    {
        return (enemyFraction == Fraction.None)
            ? _unit.Fraction == _playerFraction
            : _unit.Fraction == enemyFraction;

    }
}
