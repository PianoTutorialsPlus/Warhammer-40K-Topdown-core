using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WH40K.Essentials;

namespace Editor.Infrastructure.Player
{
    public class UnitMovementControllerBuilder : TestDataBuilder<UnitMovementController>
    {
        private IUnitMover _unitMover;

        public UnitMovementControllerBuilder()
        {
        }
        public UnitMovementControllerBuilder WithUnitMover(IUnitMover unitMover)
        {
            _unitMover = unitMover;
            return this;
        }
        public override UnitMovementController Build()
        {
            return new UnitMovementController(_unitMover ??= A.UnitMover.Build());
        }
    }
}
