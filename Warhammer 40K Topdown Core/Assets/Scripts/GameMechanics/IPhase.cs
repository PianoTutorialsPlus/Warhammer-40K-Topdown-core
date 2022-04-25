using WH40K.Essentials;

namespace WH40K.GameMechanics
{
    public interface IPhase
    {
        public void ClearPhase(GameStatsSO gameStats);

        //public MovementPhase SubEvents { get; }
        public void HandlePhase(GameStatsSO gameStats);
    }
}