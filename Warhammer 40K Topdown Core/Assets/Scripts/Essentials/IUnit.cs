using UnityEngine;
using UnityEngine.Events;

namespace WH40K.Essentials
{
    public interface IUnit: IStats, IUnitPointer
    {
        Unit unit { get; }
        UnitSelector UnitSelector { get; }
        GameStatsSO GameStats { get; set; }
        Transform Transform { get; }
        Vector3 CurrentPosition { get; }
        IUnitMover UnitMover { get; }
        IPathCalculator PathCalculator { get; }

        void Freeze();
        void SetDestination(Vector3 position);
    }
}