﻿using WH40K.PlayerEvents;

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
            return null;//new UnitMovementController(_unitMover ??= A.UnitMover.Build());
        }
    }
}
