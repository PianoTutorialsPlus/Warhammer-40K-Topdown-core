namespace WH40K.Gameplay.Events
{
    public interface IPhase
    {
        public void ClearPhase();
        public void HandlePhase();
    }
}