using WH40K.Essentials;
using WH40K.UI;

namespace WH40K.GameMechanics
{
    public interface IGamePhase
    {
        IPhase BattleroundEvents { get; }
        GameStatsSO GameStats { get; set; }
    }
}