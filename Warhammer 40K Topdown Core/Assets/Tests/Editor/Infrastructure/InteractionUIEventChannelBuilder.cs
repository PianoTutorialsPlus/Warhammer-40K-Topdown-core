using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WH40K.UI;

namespace Editor.Infrastructure
{
    public class InteractionUIEventChannelBuilder : TestDataBuilder<InteractionUIEventChannelSO>
    {
        public InteractionUIEventChannelBuilder()
        {
        }

        public override InteractionUIEventChannelSO Build()
        {
            return ScriptableObject.CreateInstance<InteractionUIEventChannelSO>();
        }
    }
}
