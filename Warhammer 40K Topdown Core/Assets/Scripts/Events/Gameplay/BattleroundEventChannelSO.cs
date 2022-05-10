using UnityEngine;
using UnityEngine.Events;
using WH40K.Essentials;

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
