using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WH40K.GameMechanics;
using WH40K.GameMechanics.Combat;

namespace Editor.Infrastructure.Combat
{
    public class SelectEnemyBuilder : TestDataBuilder<SelectEnemies>
    {
        private IResult _results;

        public SelectEnemyBuilder()
        {
        }
        public SelectEnemyBuilder WithIResult(IResult results)
        {
            _results = results;
            return this;
        }
        public override SelectEnemies Build()
        {
            return new SelectEnemies(_results ??= An.IResultEvent.Build());
        }
    }
}
