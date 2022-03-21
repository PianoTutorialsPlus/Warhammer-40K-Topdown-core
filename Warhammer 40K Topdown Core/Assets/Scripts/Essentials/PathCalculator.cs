using UnityEngine;
using UnityEngine.AI;

public class PathCalculator : IPathCalculator
//: MonoBehaviour
{
    public NavMeshAgent m_Agent;
    public NavMeshPath path;

    public float speed = 20;

    public PathCalculator(NavMeshAgent agent)
    {
        m_Agent = agent;

        path = new NavMeshPath();

        m_Agent.speed = speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;
        m_Agent.isStopped = false;
    }

    public virtual float GetDistance(Vector3 position)
    {
        m_Agent.CalculatePath(position, path);
        return GetPathLength(path);
    }
    public void SetDestination(Vector3 position)
    {
        m_Agent.SetDestination(position);
    }
    public bool IsPathCalculated => m_Agent.hasPath && !m_Agent.pathPending;
    public float RemainingDistance => m_Agent.remainingDistance;
    public bool AgentIsStopped => m_Agent.isStopped;
    public void ResetPath()
    {
        m_Agent.ResetPath();
    }
    public static float GetPathLength(NavMeshPath path)
    {
        float lng = 0.0f;

        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            lng = AddPathCorners(path, lng);
        }

        return lng;
    }
    public static float AddPathCorners(NavMeshPath path, float lng)
    {
        for (int i = 1; i < path.corners.Length; ++i)
        {
            lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }

        return lng;
    }
    public void ResetAgent()
    {
        m_Agent.isStopped = false;
    }
    public void FreezeAgent()
    {
        Debug.Log("Freeze");

        m_Agent.isStopped = true;
    }
}
