using UnityEngine;
using WH40K.Stats.Player;

namespace WH40K.Gameplay.PlayerEvents
{
    public interface IUnit : IStats, IUnitPointer
    {
        UnitFacade unit { get; }
        //UnitSelector UnitSelector { get; }
        Transform Transform { get; }
        Vector3 CurrentPosition { get; }
        UnitMover UnitMover { get; }
        //IPathCalculator PathCalculator { get; }

        void Freeze();
        void SetDestination(Vector3 position);
    }
}