using UnityEngine;
using UnityEngine.Events;
using WH40K.PlayerEvents;

/// <summary>
/// This class is used for Events to set the distance Indicator UI.
/// Example: Dispaly or hide the distance Indicator via a bool.
/// </summary>
namespace WH40K.EventChannels
{
    [CreateAssetMenu(menuName = "Events/Toggle Distance Indicator Connection UI Events Channel")]
    public class IndicatorUIEventChannelSO : ScriptableObject
    {
        public UnityAction<bool, IUnit> OnEventRaised;
        public void RaiseEvent(bool state, IUnit unit)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(state, unit);
        }
    }
}
