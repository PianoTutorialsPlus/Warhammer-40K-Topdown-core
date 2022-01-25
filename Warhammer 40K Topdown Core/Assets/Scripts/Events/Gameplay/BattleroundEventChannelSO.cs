using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Toggle Battleround Phases")]
public class BattleroundEventChannelSO : ScriptableObject
{
    public UnityAction<GameStatsSO> OnEventRaised;

    public void RaiseEvent(GameStatsSO gameStats)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(gameStats);
    }
}
