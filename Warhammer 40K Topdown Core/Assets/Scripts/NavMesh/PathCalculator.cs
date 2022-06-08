using System;
using UnityEngine;
using UnityEngine.AI;

namespace WH40K.NavMesh
{
    public class PathCalculator : IPathCalculator
    {
        private readonly NavMeshAgent _agent;
        private readonly Settings _settings;
        private readonly NavMeshPath _path;
        private NavMeshPathPosition endPosition;

        public PathCalculator(
            NavMeshAgent agent,
            Settings settings,
            NavMeshPath path)
        {
            _agent = agent;
            _settings = settings;
            _path = path;

            SetAgentSettings();
        }

        private void SetAgentSettings()
        {
            _agent.speed = _settings.Speed;
            _agent.acceleration = _settings.Acceleration;
            _agent.angularSpeed = _settings.AngularSpeed;
            _agent.isStopped = _settings.IsStopped;
        }

        public void SetEndPosition(Vector3 position)
        {
            _agent.CalculatePath(position, _path);
        }
        public void SetDestination(Vector3 position)
        {
            //position = GetEndPosition(position);
            _agent.SetDestination(position);
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

        public bool IsPathCalculated => _agent.hasPath && !_agent.pathPending;
        public bool AgentIsStopped => _agent.isStopped;

        public void ResetPath()
        {
            _agent.ResetPath();
        }
        public void ResetAgent()
        {
            _agent.isStopped = false;
        }
        public void FreezeAgent()
        {
            ResetPath();
            _agent.isStopped = true;
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