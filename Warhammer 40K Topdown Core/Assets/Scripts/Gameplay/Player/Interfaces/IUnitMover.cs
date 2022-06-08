using UnityEngine;
using WH40K.NavMesh;

namespace WH40K.Gameplay.PlayerEvents
{
    public interface IUnitMover
    {
        IPathCalculator PathCalculator { get; }
        IUnit Unit { get; }
        float MaxDistance { get; }
        Vector3 CurrentPosition { get; }
        float Range { get; }
        MovementRange MovementRange { get; }

        void SetDestination(Vector3 position);
    }
}