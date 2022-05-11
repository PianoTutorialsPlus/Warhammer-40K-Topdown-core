using UnityEngine;
using WH40K.NavMesh;

namespace WH40K.PlayerEvents
{
    public interface IUnit : IStats, IUnitPointer
    {
        Unit unit { get; }
        UnitSelector UnitSelector { get; }
        Transform Transform { get; }
        Vector3 CurrentPosition { get; }
        IUnitMover UnitMover { get; }
        IPathCalculator PathCalculator { get; }

        void Freeze();
        void SetDestination(Vector3 position);
    }
}