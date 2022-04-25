using UnityEngine;

namespace WH40K.Essentials
{
    public interface IUnitMover
    {
        //float MoveDistance { get; set; }
        IPathCalculator PathCalculator { get; }
        Unit Unit { get; }
        float MaxDistance { get; }
        Vector3 CurrentPosition { get; }
        float MoveDistance { get; }
        float MovedDistance { get; }
        MovementRange MovementRange { get; set; }
        void SetDestination(Vector3 position);
    }
}