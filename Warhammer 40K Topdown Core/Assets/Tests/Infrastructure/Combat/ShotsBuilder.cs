using WH40K.Stats.Combat;

namespace Editor.Infrastructure.Combat
{
    public class ShotsBuilder : TestDataBuilder<Shots>
    {
        private int _maxShots = 0;

        public ShotsBuilder()
        {
        }
        public ShotsBuilder WithMaxShots(int maxShots)
        {
            _maxShots = maxShots;
            return this;
        }
        public override Shots Build()
        {
            return new Shots(_maxShots);
        }
    }
}
