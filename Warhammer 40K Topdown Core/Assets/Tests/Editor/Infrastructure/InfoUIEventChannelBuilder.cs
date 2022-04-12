using UnityEngine;
using WH40K.UI;

namespace Editor.Infrastructure
{
    public class InfoUIEventChannelBuilder : TestDataBuilder<InfoUIEventChannelSO>
    {
        public InfoUIEventChannelBuilder()
        {

        }

        public override InfoUIEventChannelSO Build()
        {
            return ScriptableObject.CreateInstance<InfoUIEventChannelSO>();
        }
    }
}