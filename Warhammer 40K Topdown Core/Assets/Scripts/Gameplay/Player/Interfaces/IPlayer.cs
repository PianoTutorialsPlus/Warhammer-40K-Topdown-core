using System.Collections.Generic;
using WH40K.Stats.Player;

namespace WH40K.Gameplay.PlayerEvents
{
    public interface IPlayer
    {
        Fraction Fraction { get; set; }
        List<UnitFacade> PlayerUnits { get; set; }
    }
}