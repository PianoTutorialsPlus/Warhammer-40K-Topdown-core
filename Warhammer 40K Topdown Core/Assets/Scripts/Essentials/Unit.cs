using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace WH40K.Essentials
{
    public enum Fraction { None = 0, SpaceMarines, Necrons }

    [RequireComponent(typeof(NavMeshAgent))]

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
        public int weaponRange;

        public Fraction Fraction => _unitSO.Fraction;

        public BattleroundEventChannelSO SetMovementPhaseEvent;

        [SerializeField] public WeaponSO _weaponSO;
        [SerializeField] public UnitSO _unitSO;
        //[SerializeField] public IUnitStats _unitSOTest;
        [SerializeField] private GameStatsSO _gameStats;

        public InputReader _inputReader;

        [SerializeField] public UnityAction<IUnit> onTapDownAction;
        [SerializeField] public UnityAction onPointerEnter;
        [SerializeField] public UnityAction<IUnit> onPointerEnterInfo;
        [SerializeField] public UnityAction<IUnit> onPointerExit;

        public bool IsDone => done;
        public bool IsActivated { get => activated; set => activated = value; }
        public Transform Transform => gameObject.transform;
        public Vector3 CurrentPosition => gameObject.transform.position;
        // public IUnit unit => (IUnit)gameObject.GetComponent<Unit>();
        public Fraction ActivePlayerFraction => _gameStats.ActivePlayer.Fraction;
        public Fraction EnemyFraction => _gameStats.EnemyPlayer.Fraction;
        public IUnit EnemyUnit { get => _gameStats.EnemyUnit; set => _gameStats.EnemyUnit = value; }
        public IUnit ActiveUnit { get => _gameStats.ActiveUnit; set => _gameStats.ActiveUnit = value; }

        public UnitMovementPhase unitMovementPhase;
        public UnitShootingPhase unitShootingPhase;
        public UnitSelector UnitSelector { get; protected set; }
        public IUnitMover UnitMover { get; protected set; }
        internal float restDistance;
        public Unit unit => this;//gameObject.GetComponent<Unit>();
        public IPathCalculator PathCalculator { get; protected set; }
        public UnityAction<IUnit> OnTapDownAction { get => onTapDownAction; set => onTapDownAction = value; }
        public UnityAction OnPointerEnter { get => onPointerEnter; set => onPointerEnter = value; }
        public UnityAction<IUnit> OnPointerEnterInfo { get => onPointerEnterInfo; set => onPointerEnterInfo = value; }
        public UnityAction<IUnit> OnPointerExit { get => onPointerExit; set => onPointerExit = value; }
        public GameStatsSO GameStats { get => _gameStats; set => _gameStats = value; }

        public int WeaponRange => _weaponSO.WeaponRange;
        public int WeaponShots => _weaponSO.WeaponShots;
        public int WeaponStrength => _weaponSO.WeaponStrength;
        public int WeaponArmourPen => _weaponSO.WeaponArmourPen;
        public int WeaponDamage => _weaponSO.WeaponDamage;
        public string WeaponName => _weaponSO.WeaponName;
        public int Movement => _unitSO.Movement;
        public int BallisticSkill => _unitSO.BallisticSkill;
        public int Toughness => _unitSO.Toughness;
        public int ArmourSave => _unitSO.ArmourSave;
        public int Wounds { get; set; }

        protected void Awake()
        {
            Wounds = _unitSO.Wounds;
            //_unitSelector = new UnitSelector(_gameStats, gameObject.GetComponent<Unit>());
            //UnitMover = new UnitMover();
            m_Agent = GetComponent<NavMeshAgent>();
            PathCalculator = new PathCalculator(m_Agent);
            UnitMover = GetComponent<UnitMover>();
            UnitSelector = new UnitSelector(_gameStats, this);
            //UnitMover.Initialize(PathCalculator, _unitSO);
            //unitMovementPhase.Initialize(this);
            //gameObject.AddComponent<UnitMovementPhase>();
            //unitMovementPhase = GetComponent<UnitMovementPhase>();
            //UnitMover.Initialize(PathCalculator, this);
            canMove = true;
        }

        // Start is called before the first frame update
        void Start()
        {
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
        public void Activate()
        {
            IsActivated = true;
        }
        public void PrepareMovementPhase()
        {
            //Debug.Log("ResetMovement");
            //m_Agent.isStopped = false;
            //restDistance = MoveDistance;
        }
        public void SetDestination(Vector3 position)
        {
            UnitMover.SetDestination(position);
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
        public void TakeDamage(int damage)
        {
            Wounds -= damage;
            if (Wounds <= 0) Destroy();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}