using UnityEngine;

namespace WH40K.Essentials
{
    public interface IUnitMover
    {
        //float MoveDistance { get; set; }
        IPathCalculator PathCalculator { get; }
        IUnit Unit { get; }
        float MaxDistance { get; }
        Vector3 CurrentPosition { get; }
        float MoveDistance { get; }
        float MovedDistance { get; }
        MovementRange MovementRange { get; set; }

        void Initialize(IPathCalculator pathCalculator, IStats unit);
        void SetDestination(Vector3 position);
    }
}