﻿using UnityEngine;
using UnityEngine.AI;

namespace WH40K.PlayerEvents
{
    public class UnitModel
    {
        readonly MeshRenderer _renderer;
        readonly NavMeshAgent _agent;
        readonly Rigidbody _rigidBody;

        public UnitModel(
            MeshRenderer renderer, 
            Rigidbody rigidbody,
            NavMeshAgent agent)
        {
            _renderer = renderer;
            _rigidBody = rigidbody;
            _agent = agent;
        }

        public MeshRenderer Renderer => _renderer;
        public Rigidbody Rigidbody => _rigidBody;
        public NavMeshAgent Agent => _agent;

        public Vector3 Position
        {
            get { return _rigidBody.position; }
            set { _rigidBody.position = value; }
        }
    }
}