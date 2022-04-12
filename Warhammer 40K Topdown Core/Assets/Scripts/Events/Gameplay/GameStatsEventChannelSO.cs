using UnityEngine;
using UnityEngine.Events;
using WH40K.Essentials;

/// <summary>
/// This class is used for Events to switch the gamestats.
/// Example: Switch from movement phase to shooting phase
/// </summary>

[CreateAssetMenu(menuName = "Events/Switch gamestats")]
public class GameStatsEventChannelSO : ScriptableObject
{
    public UnityAction<GameStatsSO> OnEventRaised;

    public void RaiseEvent(GameStatsSO gameStats)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(gameStats);
    }
}
