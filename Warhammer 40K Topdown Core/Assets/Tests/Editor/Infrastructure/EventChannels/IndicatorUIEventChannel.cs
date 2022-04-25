using UnityEngine;
using WH40K.UI;

namespace Editor.Infrastructure.EventChannels
{
    public class IndicatorUIEventChannel : TestDataBuilder<IndicatorUIEventChannelSO>
    {
        public IndicatorUIEventChannel()
        {
        }

        public override IndicatorUIEventChannelSO Build()
        {
            return ScriptableObject.CreateInstance<IndicatorUIEventChannelSO>();
        }
    }
}
