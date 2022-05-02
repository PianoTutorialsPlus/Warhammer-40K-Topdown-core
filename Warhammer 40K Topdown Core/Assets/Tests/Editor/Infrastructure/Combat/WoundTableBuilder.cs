using System;
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
            var woundTable = new WoundTable();
            return woundTable;
        }
    }
}
