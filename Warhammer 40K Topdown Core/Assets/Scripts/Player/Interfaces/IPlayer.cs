using System.Collections.Generic;

namespace WH40K.PlayerEvents
{
    public interface IPlayer
    {
        Fraction Fraction { get; set; }
        List<UnitFacade> PlayerUnits { get; set; }
    }
}