using Editor.Infrastructure.Events;

namespace Editor.Infrastructure
{
    public static partial class A
    {
        public static PointerEventDataBuilder PointerEventData => new PointerEventDataBuilder();
        public static UIEventsBuilder<T> UIEvent<T>() where T : class => new UIEventsBuilder<T>();
    }
    public static partial class An
    {
    }
}
