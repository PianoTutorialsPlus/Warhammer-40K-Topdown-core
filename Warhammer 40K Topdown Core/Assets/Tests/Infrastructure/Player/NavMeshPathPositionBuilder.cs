using UnityEngine;
using WH40K.NavMesh;

namespace Editor.Infrastructure.Player
{
    public class NavMeshPathPositionBuilder : TestDataBuilder<NavMeshPathPosition>
    {
        private float _moveRange;
        private Vector3[] _pathCorners;

        public NavMeshPathPositionBuilder()
        {
        }

        public NavMeshPathPositionBuilder WithMoveRange(float range)
        {
            _moveRange = range;
            return this;
        }

        public NavMeshPathPositionBuilder WithPathCorners(Vector3 startPos)
        {
            _pathCorners = new Vector3[1];
            _pathCorners[0] = startPos;
            return this;
        }
        public NavMeshPathPositionBuilder WithPathCorners(Vector3 startPos, Vector3 endPos)
        {
            _pathCorners = new Vector3[2];
            _pathCorners[0] = startPos;
            _pathCorners[1] = endPos;
            return this;
        }
        public NavMeshPathPositionBuilder WithPathCorners(Vector3 startPos, Vector3 midPos, Vector3 endPos)
        {
            _pathCorners = new Vector3[3];
            _pathCorners[0] = startPos;
            _pathCorners[1] = midPos;
            _pathCorners[2] = endPos;
            return this;
        }

        public override NavMeshPathPosition Build()
        {
            var navMeshPosition = new NavMeshPathPosition(_pathCorners, _moveRange);

            return navMeshPosition;
        }
    }
}