using System;
using UnityEngine;
using UnityEngine.AI;

namespace WH40K.Essentials
{
    public class PathCalculator : IPathCalculator
    //: MonoBehaviour
    {
        public NavMeshAgent m_Agent;
        public NavMeshPath path;
        public NavMeshPathPosition endPosition;

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

        public void SetEndPosition(Vector3 position)
        {
            m_Agent.CalculatePath(position, path);
        }
        public void SetDestination(Vector3 position)
        {
            //position = GetEndPosition(position);
            m_Agent.SetDestination(position);
        }

        public Vector3 GetEndPosition(Vector3 position, float range)
        {
            SetEndPosition(position);

            if (path.status == NavMeshPathStatus.PathInvalid) return position;

            endPosition = new NavMeshPathPosition(path.corners, range)
            {
                EndPosition = position
            };
            return endPosition.EndPosition;
        }

        public bool IsPathCalculated => m_Agent.hasPath && !m_Agent.pathPending;
        public bool AgentIsStopped => m_Agent.isStopped;

        public void ResetPath()
        {
            m_Agent.ResetPath();
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

        public Vector3 GetEndPosition(Vector3 position)
        {
            throw new NotImplementedException();
        }
    }
}