using WH40K.GameMechanics.Combat;

namespace Editor.Infrastructure.Combat
{
    public class WoundTableBuilder : TestDataBuilder<WoundTable>
    {
        public WoundTableBuilder()
        {
        }
        public override WoundTable Build()
        {
            return new WoundTable();
        }
    }
}
