using System;
using UnityEngine;
using UnityEngine.AI;
using WH40K.PlayerEvents;

namespace WH40K.NavMesh
{
    public class PathCalculator : IPathCalculator
    {
        private readonly NavMeshAgent m_Agent;
        private readonly Settings _settings;
        private readonly NavMeshPath _path;
        private NavMeshPathPosition endPosition;

        public float speed = 20;

        public PathCalculator(
            UnitModel model,
            Settings settings,
            NavMeshPath path)
        {
            m_Agent = model.Agent;
            _settings = settings;
            _path = path;

            SetAgentSettings();
        }

        private void SetAgentSettings()
        {
            m_Agent.speed = _settings.Speed;
            m_Agent.acceleration = _settings.Acceleration;
            m_Agent.angularSpeed = _settings.AngularSpeed;
            m_Agent.isStopped = _settings.IsStopped;
        }

        public void SetEndPosition(Vector3 position)
        {
            m_Agent.CalculatePath(position, _path);
        }
        public void SetDestination(Vector3 position)
        {
            //position = GetEndPosition(position);
            m_Agent.SetDestination(position);
        }

        public Vector3 GetEndPosition(Vector3 position, float range)
        {
            SetEndPosition(position);

            if (_path.status == NavMeshPathStatus.PathInvalid) return position;

            endPosition = new NavMeshPathPosition(_path.corners, range)
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

        [Serializable]
        public class Settings
        {
            public float Speed;
            public float Acceleration;
            public float AngularSpeed;
            public bool IsStopped;
        }
    }
}