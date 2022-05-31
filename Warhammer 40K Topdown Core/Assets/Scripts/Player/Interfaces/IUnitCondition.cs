namespace WH40K.PlayerEvents
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