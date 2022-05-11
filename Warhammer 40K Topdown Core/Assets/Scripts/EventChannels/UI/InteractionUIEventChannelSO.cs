using UnityEngine;
using UnityEngine.Events;
using WH40K.GamePhaseEvents;

/// <summary>
/// This class is used for Events to toggle the interaction UI.
/// Example: Dispaly or hide the interaction UI via a bool and the interaction type from the enum via int
/// </summary>
namespace WH40K.EventChannels
{
    [CreateAssetMenu(menuName = "Events/Toggle Interaction UI Events Channel")]
    public class InteractionUIEventChannelSO : ScriptableObject
    {
        public UnityAction<bool, InteractionType> OnEventRaised;
        public void RaiseEvent(bool state, InteractionType interactionType)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(state, interactionType);
        }
    }
}
