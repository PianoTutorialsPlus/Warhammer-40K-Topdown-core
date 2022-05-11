namespace WH40K.Events
{
    public interface IPhase
    {
        public void ClearPhase();
        public void HandlePhase();
    }
}