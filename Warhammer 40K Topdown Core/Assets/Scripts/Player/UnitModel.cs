using UnityEngine;
using UnityEngine.AI;

namespace WH40K.PlayerEvents
{
    public class UnitModel
    {
        readonly MeshRenderer _renderer;
        readonly NavMeshAgent _agent;

        public UnitModel(
            MeshRenderer renderer, 
            NavMeshAgent agent)
        {
            _renderer = renderer;
            _agent = agent;
        }
        public MeshRenderer Renderer
        {
            get { return _renderer; }
        }
        public NavMeshAgent Agent
        {
            get { return _agent; }
        }
    }
}