using UnityEngine;

namespace WH40K.Stats.Player
{
    public interface IUnit : IStats, IUnitPointer
    {
        Transform Transform { get; }
        Vector3 CurrentPosition { get; }
    }
}