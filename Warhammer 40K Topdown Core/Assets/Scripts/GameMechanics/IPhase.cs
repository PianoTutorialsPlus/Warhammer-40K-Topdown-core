public interface IPhase
{
    //public MovementPhase SubEvents { get; }
    public void HandlePhase(GameStatsSO gameStats);
    void HandleMove(GameStatsSO gameStats);
}