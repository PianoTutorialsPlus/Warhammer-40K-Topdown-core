using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class Unit : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler // INHARITANCE
{
    public float speed = 3;
    public bool canMove = true;
    public bool canShoot = true;
    public bool activated = false;
    public bool singleton = false;
    public bool isMoving = false;
    public bool selected = false;
    public bool done = false;

    protected NavMeshAgent m_Agent;     // Moving behaviour
    private NavMeshPath path;
    [SerializeField] float moveDistance;
    public float movedDistance = 0;
    public int weaponRange;
    public float distanceToMove;
    public float restDistance = 1;
    public string phase;
    float movedInstance = 0;

    public BattleroundEventChannelSO SetMovementPhaseEvent;

    public WeaponSO _weaponSO;
    public UnitSO _unitSO;
    public GameStatsSO _gameStats;

    public InputReader _inputReader;

    public UnityAction<Unit> onTapDownAction;
    public UnityAction onPointerEnter;
    public UnityAction<Unit> onPointerEnterInfo;
    public UnityAction<Unit> onPointerExit;

    //public ActiveUnitSO activeUnit;

    public int test = 0;

    //public Dictionary<string, int> stats = new Dictionary<string, int>()
    //{
    //    {"Movement", 0},
    //    {"Weapon Skill", 0},
    //    {"Ballistic Skill", 0},
    //    {"Strength", 0},
    //    {"Toughness", 0},
    //    {"Wounds", 0},
    //    {"Attacks", 0},
    //    {"Leadership", 0},
    //    {"Armour Save", 0},
    //};

    //public Dictionary<string, int> weaponStats = new Dictionary<string, int>()
    //{
    //    {"Range", 0},
    //    {"Type", 0},
    //    {"Strength",0},
    //    {"Armor Pen",0},
    //    {"Damage",0 },
    //};


    //public  GameObject distanceIndicator;

    protected void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;
        m_Agent.isStopped = false;
        canMove = true;
        isMoving = false;
        singleton = false;


        moveDistance = _unitSO.Movement; //stats["Movement"];
        restDistance = moveDistance;
        weaponRange = _weaponSO.Range; //weaponStats["Range"];

    }

    // Start is called before the first frame update
    void Start()
    {
        _unitSO.takenWounds = 0;
        path = new NavMeshPath();
        phase = "Movement Phase";
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


    public void OnPointerEnter(PointerEventData pointerEvent)
    {

        if (onPointerEnter != null) onPointerEnter();
        if (onPointerEnterInfo != null) onPointerEnterInfo(gameObject.GetComponent<Unit>());

    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        if (onPointerExit != null)
        {
            onPointerExit(gameObject.GetComponent<Unit>());
        }
    }


    public void OnPointerClick(PointerEventData pointerEvent)
    {

        if (onTapDownAction != null)
        {
            if (pointerEvent.button == PointerEventData.InputButton.Left)
            {
                Debug.Log(gameObject.name);
                _gameStats.activeUnit = gameObject.GetComponent<Unit>();
                onTapDownAction(gameObject.GetComponent<Unit>());
                selected = true;
            }
            else if (pointerEvent.button == PointerEventData.InputButton.Right && _gameStats.activePlayer._playerUnits[0].tag == gameObject.tag)
            {
                _gameStats.enemyUnit = gameObject.GetComponent<Unit>();
            }
        }
        if (onPointerEnter != null) onPointerEnter();
    }

    public virtual void AddMovedDistance()
    {
        movedDistance += movedInstance;// (distanceToMove - m_Agent.remainingDistance);
        movedInstance = 0;
    }

    public virtual void SetDestination(Vector3 position)
    {
        if (!m_Agent.isStopped)
        {
            StartCoroutine(GoTo(position));
        }
    }

    public IEnumerator GoTo(Vector3 position)
    {
        if (!m_Agent.isStopped)
        {
            AddMovedDistance();
            GetDistance(position);
            m_Agent.SetDestination(position);

            while (true)
            {
                yield return new WaitForEndOfFrame();

                if (m_Agent.hasPath && !m_Agent.pathPending)
                {
                    //Debug.Log(m_Agent.pathPending);
                    isMoving = true;
                    restDistance = moveDistance - movedDistance - (distanceToMove - m_Agent.remainingDistance);
                    movedInstance = (distanceToMove - m_Agent.remainingDistance);

                    if (restDistance <= 0)
                    {

                        Freeze();
                        Debug.Log("restDistance");
                        m_Agent.ResetPath();
                        break;
                    }
                }
                if (m_Agent.remainingDistance <= 0 && isMoving == true)
                {
                    movedDistance += distanceToMove;
                    movedInstance = 0;
                    isMoving = false;
                    break;
                }
            }
        }
        
    }

    public virtual void GetDistance(Vector3 position)
    {
        m_Agent.CalculatePath(position, path);
        distanceToMove = GetPathLength(path);
    }

    public static float GetPathLength(NavMeshPath path)
    {
        float lng = 0.0f;

        if ((path.status != NavMeshPathStatus.PathInvalid))
        {
            for (int i = 1; i < path.corners.Length; ++i)
            {
                lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }

        return lng;
    }

    public virtual void ResetData()
    {
        Debug.Log("Reset");
        if (_gameStats.phase == GamePhase.MovementPhase)
        {
            m_Agent.isStopped = false;
            restDistance = moveDistance;
        }
        else
        {
            m_Agent.isStopped = true;
            restDistance = weaponRange;
        }
        //Debug.Log(name);
        //canMove = true;
        canShoot = true;
        movedDistance = 0;
        activated = false;
        selected = false;
        done = false;

    }

    public virtual void Freeze()
    {
        //canMove = false;
        m_Agent.isStopped = true;
        restDistance = 0;
        distanceToMove = 0;
        //movedDistance = moveDistance;
        activated = false;
        selected = false;
        done = true;
        _gameStats.movementSubPhase = MovementPhase.Selection;
        SetMovementPhaseEvent.RaiseEvent(_gameStats);
    }


    public void Destroy()
    {
        Destroy(gameObject);
    }
}
