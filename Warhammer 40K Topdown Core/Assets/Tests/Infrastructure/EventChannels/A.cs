using Infrastructure.EventChannels;
using UnityEngine;

namespace Editor.Infrastructure
{
    public static partial class A
    {
        public static EventChannelBuilder<T> EventChannel<T>() where T : ScriptableObject => new EventChannelBuilder<T>();
    }
}
