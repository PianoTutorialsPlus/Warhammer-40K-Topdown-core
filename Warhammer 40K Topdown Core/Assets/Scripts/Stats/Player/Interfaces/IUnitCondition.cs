namespace WH40K.Stats.Player
{
    public interface IUnitCondition
    {
        public bool IsDone { get; }
        public bool IsActivated { get; }
        public bool IsSelected { get; }

        void Activate();
        void Destroy();
        void ResetData();
        void TakeDamage(int damage);
    }
}