using WH40K.Essentials;
using WH40K.UI;

namespace WH40K.GameMechanics
{
    public interface IGamePhase
    {
        BattleRoundsSO BattleroundEvents { get; set; }
        InputReader InputReader { get; set; }
        GameStatsSO GameStats { get; set; }
    }
}