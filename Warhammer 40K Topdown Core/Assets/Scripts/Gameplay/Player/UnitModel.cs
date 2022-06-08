using UnityEngine;
using UnityEngine.AI;

namespace WH40K.Gameplay.PlayerEvents
{
    public class UnitModel
    {
        readonly MeshRenderer _renderer;
        readonly NavMeshAgent _agent;
        readonly Rigidbody _rigidBody;
        readonly Transform _transform;

        public UnitModel(
            MeshRenderer renderer,
            Rigidbody rigidbody,
            Transform transform,
            NavMeshAgent agent)
        {
            _renderer = renderer;
            _rigidBody = rigidbody;
            _transform = transform;
            _agent = agent;
        }

        public MeshRenderer Renderer => _renderer;
        public Rigidbody Rigidbody => _rigidBody;
        public NavMeshAgent Agent => _agent;
        public Transform Transform => _transform;

        public Vector3 Position
        {
            get { return _rigidBody.position; }
            set { _rigidBody.position = value; }
        }
    }
}