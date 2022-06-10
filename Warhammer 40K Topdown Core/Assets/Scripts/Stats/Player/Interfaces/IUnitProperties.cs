namespace WH40K.Stats.Player
{
    public interface IUnitProperties
    {
        public bool IsDone { get; }
        public bool IsActivated { get; }
        public bool IsSelected { get; }
        float Range { get; }
    }
}