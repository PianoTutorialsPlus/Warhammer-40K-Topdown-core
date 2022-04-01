using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace WH40K.UnitHandler
{
    [RequireComponent(typeof(NavMeshAgent))]
    //[RequireComponent(typeof(NecronWarriorSO))]

    public class Unit : MonoBehaviour, IUnit// INHARITANCE
    {
        public float speed = 3;
        public bool canMove = true;
        public bool activated = false;
        public bool canShoot = true;
        public bool IsSelected { get; set; }
        public bool done = false;

        public NavMeshAgent m_Agent;     // Moving behaviour
                                         //private NavMeshPath path;
        public float MoveDistance => _unitSO.Movement;
        public int weaponRange;

        public Fraction Fraction => _unitSO.Fraction;

        public BattleroundEventChannelSO SetMovementPhaseEvent;

        [SerializeField] public WeaponSO _weaponSO;
        [SerializeField] public UnitSO _unitSO;
        //[SerializeField] public IUnitStats _unitSOTest;
        [SerializeField] private GameStatsSO _gameStats;

        public InputReader _inputReader;

        [SerializeField] public UnityAction<Unit> onTapDownAction;
        [SerializeField] public UnityAction onPointerEnter;
        [SerializeField] public UnityAction<Unit> onPointerEnterInfo;
        [SerializeField] public UnityAction<Unit> onPointerExit;

        public bool IsDone => done;
        // public IUnit unit => (IUnit)gameObject.GetComponent<Unit>();
        public Fraction ActivePlayerFraction => _gameStats.activePlayer.fraction;
        public Fraction EnemyFraction => _gameStats.enemyPlayer.fraction;
        public IUnitStats EnemyUnit { get => _gameStats.enemyUnitTest; set => _gameStats.enemyUnitTest = value; }
        public IUnitStats ActiveUnit { get => _gameStats.activeUnitTest; set => _gameStats.activeUnitTest = value; }


        public UnitMovementPhase unitMovementPhase;
        public UnitSelector UnitSelector { get; protected set; }
        public UnitMover UnitMover;
        internal float restDistance;
        public Unit unit => gameObject.GetComponent<Unit>();
        public IPathCalculator PathCalculator { get; protected set; }
        public UnityAction<Unit> OnTapDownAction { get => onTapDownAction; set => onTapDownAction = value; }
        public UnityAction OnPointerEnter { get => onPointerEnter; set => onPointerEnter = value; }
        public UnityAction<Unit> OnPointerEnterInfo { get => onPointerEnterInfo; set => onPointerEnterInfo = value; }
        public UnityAction<Unit> OnPointerExit { get => onPointerExit; set => onPointerExit = value; }
        public GameStatsSO GameStats { get => _gameStats; set => _gameStats = value; }

        //public ActiveUnitSO activeUnit;
        //public  GameObject distanceIndicator;

        protected void Awake()
        {

            //_unitSelector = new UnitSelector(_gameStats, gameObject.GetComponent<Unit>());
            //UnitMover = new UnitMover();

            m_Agent = GetComponent<NavMeshAgent>();
            PathCalculator = new PathCalculator(m_Agent);

            UnitSelector = new UnitSelector(ActivePlayerFraction, gameObject.GetComponent<Unit>()._unitSO);
            UnitMover.Initialize(PathCalculator, _unitSO);
            //unitMovementPhase.Initialize(this);
            gameObject.AddComponent<UnitMovementPhase>();
            unitMovementPhase = GetComponent<UnitMovementPhase>();

            canMove = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            _unitSO.takenWounds = 0;
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
            //Debug.Log("Reset:" + name);
            //////canMove = true;
            //canShoot = true;
            //movedDistance = 0;
            //activated = false;
            //IsSelected = false;
            //PathCalculator.ResetAgent();
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
            //restDistance = weaponRange;
        }
        public virtual void Freeze()
        {
            ////canMove = false;
            //PathCalculator.FreezeAgent();
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
}