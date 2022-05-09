using Editor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.EventChannels
{
    public class EventChannelBuilder<T> : TestDataBuilder<T> where T: ScriptableObject
    {
        public EventChannelBuilder() { }
 
        public override T Build()
        {
            return ScriptableObject.CreateInstance<T>();
        }
    }
}
