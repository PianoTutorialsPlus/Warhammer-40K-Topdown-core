using System;
using UnityEngine;
using UnityEngine.AI;

public class PathCalculator : IPathCalculator
//: MonoBehaviour
{
    public NavMeshAgent m_Agent;
    public NavMeshPath path;

    public float speed = 20;
    private static float moveDistance;
    private static Vector3 EndPosition;

    public PathCalculator(NavMeshAgent agent)
    {
        m_Agent = agent;

        path = new NavMeshPath();

        m_Agent.speed = speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;
        m_Agent.isStopped = false;
    }

    public void SetMoveDistance(float distance)
    {
        moveDistance = distance;
    }

    public void SetEndPosition(Vector3 position)
    {
        m_Agent.CalculatePath(position, path);
        SetPathLength(path);
    }
    public void SetDestination(Vector3 position)
    {
        position = GetEndPosition(position);
        m_Agent.SetDestination(position);
    }

    public Vector3 GetEndPosition(Vector3 position)
    {
        return (EndPosition != Vector3.zero)
            ? EndPosition
            : position;
    }

    public bool IsPathCalculated => m_Agent.hasPath && !m_Agent.pathPending;
    public bool AgentIsStopped => m_Agent.isStopped;
    public void ResetPath()
    {
        m_Agent.ResetPath();
    }
    public static void SetPathLength(NavMeshPath path)
    {
        
        float lng = 0.0f;

        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            AddPathCorners(path, lng);
        }
    }
    public static void AddPathCorners(NavMeshPath path, float lng)
    {
        for (int i = 1; i < path.corners.Length; ++i)
        {
            SetEndPosition(path, lng, i);

            if(EndPosition!= Vector3.zero) break; 

            lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }
    }

    private static void SetEndPosition(NavMeshPath path,float lng, int i)
    {
        EndPosition = Vector3.zero;

        if (lng + Vector3.Distance(path.corners[i - 1], path.corners[i]) >= moveDistance)
        {
            Vector3 normalizedVector = (path.corners[i] - path.corners[i - 1]).normalized;
            float delta = moveDistance - lng;

            EndPosition = normalizedVector * delta;
        }
    }

    public void ResetAgent()
    {
        m_Agent.isStopped = false;
    }
    public void FreezeAgent()
    {
        ResetPath();
        m_Agent.isStopped = true;
    }
}
