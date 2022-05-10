﻿using Editor.Infrastructure.Events;
using WH40K.GameMechanics;
using WH40K.UI;

namespace Editor.Infrastructure
{
    public static partial class A
    {
        public static UIEventsBuilder<UIDisplayInteractionEvents> UIDisplayInteractionEvent => new UIEventsBuilder<UIDisplayInteractionEvents>();
        public static UIEventsBuilder<UIMovementRangeEvents> UIMovementRangeEvent => new UIEventsBuilder<UIMovementRangeEvents>();
        public static UIEventsBuilder<BattleRoundEvents> BattleRoundEvent => new UIEventsBuilder<BattleRoundEvents>();
        public static PointerEventDataBuilder PointerEventData => new PointerEventDataBuilder();
        public static UIEventsBuilder<UIDisplayInfoEvents> UIDisplayInfoEvent => new UIEventsBuilder<UIDisplayInfoEvents>();
    }
    public static partial class An
    {
        public static IManageUIEventBuilder UIEvent => new IManageUIEventBuilder();
    }
}