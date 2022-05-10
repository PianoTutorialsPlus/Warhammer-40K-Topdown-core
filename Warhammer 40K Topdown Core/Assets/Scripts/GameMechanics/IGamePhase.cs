using WH40K.Essentials;

namespace WH40K.GameMechanics
{
    public interface IGamePhase
    {
        IPhase BattleroundEvents { get; }
    }
}