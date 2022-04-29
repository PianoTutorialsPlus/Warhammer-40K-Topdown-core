using UnityEngine;

namespace WH40K.Essentials
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