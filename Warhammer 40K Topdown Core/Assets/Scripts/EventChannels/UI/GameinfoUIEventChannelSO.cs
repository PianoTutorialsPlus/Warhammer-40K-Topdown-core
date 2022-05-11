using UnityEngine;
using UnityEngine.Events;

namespace WH40K.EventChannels
{
    /// <summary>
    /// This class is used for Events to toggle the gameinfo UI.
    /// Example: Dispaly or hide the gameinfo UI via a bool.
    /// </summary>

    [CreateAssetMenu(menuName = "Events/Toggle Gameinfo UI Events Channel")]
    public class GameinfoUIEventChannelSO : ScriptableObject
    {
        public UnityAction<bool> OnEventRaised;   //public UnityAction<bool, Unit,PhaseSO,TurnSO> OnEventRaised;
                                                  //public void RaiseEvent(bool state, Unit unit, PhaseSO phase, TurnSO turn)
                                                  //{
                                                  //    if (OnEventRaised != null)
                                                  //        OnEventRaised.Invoke(state, unit,phase,turn);
                                                  //}

        public void RaiseEvent(bool state)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(state);
        }
    }
}