using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public float speed = 3;
    public bool canMove = true;
    public bool singleton;

    protected NavMeshAgent m_Agent;     // Moving behaviour
    private NavMeshPath path;
    [SerializeField] float moveDistance;
    public float movedDistance = 0;
    public float distanceToMove;
    public float restDistance = 1;


    [Header("List")]
    [SerializeField]
    List<string> test2 = new List<string> { "Hallo" };

    public Dictionary<string, int> stats = new Dictionary<string, int>()
    {
        {"Movement", 0},
        {"Weapon Skill", 0},
        {"Ballistic Skill", 0},
        {"Strength", 0},
        {"Toughness", 0},
        {"Wounds", 0},
        {"Attacks", 0},
        {"Leadership", 0},
        {"Armour Save", 0},
    };


    //public  GameObject distanceIndicator;

    protected void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;
        SetStats();
        moveDistance = stats["Movement"];
        
    }


    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
    }


    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            restDistance = moveDistance - movedDistance - (distanceToMove - m_Agent.remainingDistance);
        }

        if (restDistance <= 0)
        {
            canMove = false;
            m_Agent.isStopped = true;
            restDistance = 0;
        }

        if (canMove && m_Agent.remainingDistance <= 0 && singleton)
        {
            movedDistance += distanceToMove;
            distanceToMove = 0;
            singleton = false;
        }

        if (m_Agent.remainingDistance > 0 && m_Agent.hasPath)
        {
            singleton = true;
        }

    }

    public virtual void AddMovedDistance()
    {
        
        movedDistance += (distanceToMove - m_Agent.remainingDistance);
    }

    public virtual void GoTo(Vector3 position)
    {

        m_Agent.SetDestination(position);
        if (canMove)
        {
            m_Agent.isStopped = false;
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
        canMove = true;
        movedDistance = 0;
    }

    public virtual void SetStats()
    {

    }
}
