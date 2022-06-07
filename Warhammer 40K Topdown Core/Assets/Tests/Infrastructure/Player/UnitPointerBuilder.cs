﻿using WH40K.PlayerEvents;

namespace Editor.Infrastructure
{
    public class UnitPointerBuilder : TestDataBuilder<UnitPointer>
    {
        public UnitPointerBuilder()
        {
        }

        public override UnitPointer Build()
        {
            Container.Bind<UnitPointer>().AsSingle().IfNotBound();
            return Container.Resolve<UnitPointer>();
        }
    }
}