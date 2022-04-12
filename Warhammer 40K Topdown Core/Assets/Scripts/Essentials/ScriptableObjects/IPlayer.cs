using System.Collections.Generic;

namespace WH40K.Essentials
{
    public interface IPlayer
    {
        Fraction Fraction { get; set; }
        List<Unit> PlayerUnits { get; set; }
    }
}