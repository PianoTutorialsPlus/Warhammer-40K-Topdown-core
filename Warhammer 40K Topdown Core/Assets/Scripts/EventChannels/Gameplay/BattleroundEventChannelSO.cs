using UnityEngine;
using UnityEngine.Events;

namespace WH40K.EventChannels
{
    [CreateAssetMenu(menuName = "Events/Toggle Battleround Phases")]
    public class BattleroundEventChannelSO : ScriptableObject
    {
        public UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke();
        }
    }
}