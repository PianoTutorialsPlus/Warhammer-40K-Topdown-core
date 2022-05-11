namespace WH40K.PlayerEvents
{
    public interface IUnitCondition
    {
        public bool IsDone { get; }
        public bool IsActivated { get; set; }
        public bool IsSelected { get; }

        void Activate();
        void Destroy();
        void TakeDamage(int damage);
    }
}