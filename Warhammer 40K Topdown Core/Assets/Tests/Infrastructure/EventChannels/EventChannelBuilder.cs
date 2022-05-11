using Editor.Infrastructure;
using UnityEngine;

namespace Infrastructure.EventChannels
{
    public class EventChannelBuilder<T> : TestDataBuilder<T> where T : ScriptableObject
    {
        public EventChannelBuilder() { }

        public override T Build()
        {
            return ScriptableObject.CreateInstance<T>();
        }
    }
}
