using UnityEngine;
using UnityEngine.Events;
using WH40K.Gameplay.EventChannels;

namespace WH40K.Gameplay.Events
{
    /// <summary>
    /// A flexible handler for void events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
    /// </summary>
    public class VoidEventListener : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO _channel = default;

        public UnityEvent OnEventRaised;

        private void OnEnable()
        {
            if (_channel != null)
                _channel.OnEventRaised += Respond;
        }

        private void OnDisable()
        {
            if (_channel != null)
                _channel.OnEventRaised -= Respond;
        }

        private void Respond()
        {
            if (_channel != null)
                OnEventRaised.Invoke();
        }
    }
}