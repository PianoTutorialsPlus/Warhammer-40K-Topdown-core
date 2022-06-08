using UnityEngine;
using UnityEngine.Events;

namespace WH40K.Gameplay.EventChannels
{
    /// <summary>
    /// This class is used for Events to toggle the gameinfo UI.
    /// Example: Dispaly or hide the gameinfo UI via a bool.
    /// </summary>

    [CreateAssetMenu(menuName = "Events/Toggle Gameinfo UI Events Channel")]
    public class GameInfoUIEventChannelSO : ScriptableObject
    {
        public UnityAction<bool> OnEventRaised;

        public void RaiseEvent(bool state)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(state);
        }
    }
}