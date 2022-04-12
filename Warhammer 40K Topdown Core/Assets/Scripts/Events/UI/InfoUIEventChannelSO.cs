using UnityEngine;
using UnityEngine.Events;
using WH40K.Essentials;

/// <summary>
/// This class is used for Events to toggle the info UI.
/// Example: Dispaly or hide the interaction UI via a bool and the interaction type from the enum via int
/// </summary>
namespace WH40K.UI
{
    [CreateAssetMenu(menuName = "Events/Toggle Info UI Events Channel")]
    public class InfoUIEventChannelSO : ScriptableObject
    {
        public UnityAction<bool, IStats> OnEventRaised;
        public void RaiseEvent(bool state, IStats unit)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(state, unit);
        }
    }
}
