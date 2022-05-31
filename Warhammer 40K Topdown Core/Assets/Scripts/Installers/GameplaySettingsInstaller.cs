using System;
using UnityEngine;
using WH40K.GamePhaseEvents;
using Zenject;

// Uncomment if you want to add alternative game settings
//[CreateAssetMenu(menuName = "Game Settings")]
public class GameplaySettingsInstaller : ScriptableObjectInstaller<GameplaySettingsInstaller>
{
    public GamePhaseSettings GamePhase;

    [Serializable]
    public class GamePhaseSettings
    {
        public MovementPhaseManager.Settings MovementPhaseHandler;
        public ShootingPhaseManager.Settings ShootingPhaseHandler;
    }
    public override void InstallBindings()
    {
        Container.BindInstance(GamePhase.MovementPhaseHandler).IfNotBound();
        Container.BindInstance(GamePhase.ShootingPhaseHandler).IfNotBound();
    }
}