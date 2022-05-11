using UnityEngine;
using UnityEngine.Events;

namespace WH40K.EventChannels
{

    /// <summary>
    /// This class is used for Events to switch the gamestats.
    /// Example: Switch from movement phase to shooting phase
    /// </summary>

    [CreateAssetMenu(menuName = "Events/Switch gamestats")]
    public class GameStatsEventChannelSO : ScriptableObject
    {
        public UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke();
        }
    }
}