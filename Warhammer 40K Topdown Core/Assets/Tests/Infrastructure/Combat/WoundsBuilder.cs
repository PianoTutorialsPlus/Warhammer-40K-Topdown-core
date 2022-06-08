using System.Collections.Generic;
using WH40K.Gameplay.Combat;

namespace Editor.Infrastructure.Combat
{
    public class WoundsBuilder : TestDataBuilder<Wounds>
    {
        private List<int> _unsavedWounds;

        public WoundsBuilder()
        {
        }
        public WoundsBuilder WithUnsavedWoundList(List<int> wounds)
        {
            _unsavedWounds = wounds;
            return this;
        }
        public override Wounds Build()
        {
            return new Wounds(_unsavedWounds);
        }
    }
}
