using UnityEngine.Events;

namespace WH40K.Essentials
{
    public interface IUnit: IStats, IUnitPointer
    {
        Unit unit { get; }
        UnitSelector UnitSelector { get; }
        GameStatsSO GameStats { get; set; }
    }
}