using UnityEngine;
using UnityEngine.Events;
using WH40K.EventChannels;

namespace WH40K.Events
{

    /// <summary>
    /// A flexible handler for int events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
    /// </summary>
    public class IntEventListener : MonoBehaviour
    {
        [SerializeField] private IntEventChannelSO _channel = default;

        public UnityEvent<int> OnEventRaised;

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

        private void Respond(int integer)
        {
            if (_channel != null)
                OnEventRaised.Invoke(integer);
        }
    }
}